using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseMonoBehaviour : MonoBehaviour
{
    Dictionary<string, AssetBundle> AssetDatas = new Dictionary<string, AssetBundle>();
    public virtual void Open() {
  
    }
    public virtual void OpenAnother()
    {
    
    }
    //-----------------------------------------------------------------------------------------------
    //
    //  调用区
    //
    //-----------------------------------------------------------------------------------------------
    //加载预制
    public void DownLoadPrefab(string name, GameTools.Finish<GameObject> finish) {
        string prefabName = AssetPath.PrefabPath + name + AssetPath.EndPath;
        string fullPath = AssetPath.GetWWWPath(AssetPath.AssetBundlePath + prefabName);
        DownLoad(fullPath, delegate (WWW www)
        {
            GetAssetPrefab(prefabName, www, finish);
            ClearAssetDatas();
        });
    }
    //截屏并保存的SD卡中
    public void CaptureScreen(GameTools.Finish<Sprite> Finish)
    {
        CaptureFullScreen(GameTools.UICanvas, delegate (Texture2D texture) {
            Finish(GameTools.GetSprite(texture));
        });
    }
    //-----------------------------------------------------------------------------------------------
    //
    //  封装区
    //
    //------------------------------------------------------------------------------------------------
    //获取本地图片
    public void GetTexture2D(string path, GameTools.Finish<Texture2D> Finish) {
        DownLoad(Application.temporaryCachePath + "/Screenshot.png", delegate (WWW www) {
            Texture2D info = www.assetBundle.mainAsset as Texture2D;
            Finish(info);
        });
    }
    //加载www
    public void DownLoad(string path, GameTools.Finish<WWW> finish)
    {
        StartCoroutine(DownLoadWWW(path, finish));
    }
    //获取文本
    public static string GetAssetText(WWW www)
    {
        if (string.IsNullOrEmpty(www.error))
        {
            return GameTools.GetAsset<TextAsset>(www.assetBundle).text;
        }
        return "";
    }
    //获得图片
    public Sprite GetAssetSprite(WWW www)
    {
        if (string.IsNullOrEmpty(www.error))
        {
           return GameTools.GetAsset<Sprite>(www.assetBundle);
        }
        return null;
    }
    //获得预制
    public void GetAssetPrefab(string prefabName, WWW www, GameTools.Finish<GameObject> finish)
    {
        if (string.IsNullOrEmpty(www.error))
        {
            //先加载依赖资源
            LoadDepAsset(prefabName, delegate ()
            {
                GameTools.GetAsset(www.assetBundle, finish);
            });
        }
    }
    //截全屏
    public void CaptureFullScreen(Canvas canvas, GameTools.Finish<Texture2D> Finish)
    {
        StartCoroutine(CaptureRect(canvas.pixelRect, Finish));
    }
    //-----------------------------------------------------------------------------------------------
    //
    //  原始方法区
    //
    //------------------------------------------------------------------------------------------------
    //加载依赖资源
    public void LoadDepAsset(string name, GameTools.Finish finish)
    {
        string[] paths = AssetPath.AssetDep.GetAllDependencies(name);
        List<string> needDownLoadFiles = new List<string>();
        foreach (string info in paths)
        {
            print("依赖资源: " + info);
            string fileName = AssetPath.GetFullAssetBundlePath(info);
            needDownLoadFiles.Add(AssetPath.GetWWWPath(fileName));
        }
        //依次加载
        DownLoadRes(needDownLoadFiles, finish);
    }
    //依次加载需要更新的资源  
    public void DownLoadRes(List<string> needDownLoadFiles, GameTools.Finish finish)
    {
        if (needDownLoadFiles.Count == 0)
        {
            finish();
            return;
        }
        string fileName = needDownLoadFiles[0];
        needDownLoadFiles.RemoveAt(0);
        if (AssetDatas.ContainsKey(fileName)) {
            DownLoadRes(needDownLoadFiles, finish);
        }
        DownLoad(fileName, delegate (WWW w)
        {
            GameTools.LoadAllAssets(w.assetBundle);
            AssetDatas.Add(fileName, w.assetBundle);
            DownLoadRes(needDownLoadFiles, finish);
        });
    }
    public IEnumerator DownLoadWWW(string url, GameTools.Finish<WWW> finishFun)
    {
        WWW www = new WWW(url);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(url);
            Debug.Log(www.error);
        }
        else {
        }
        if (finishFun != null)
        {
            finishFun(www);
        }
    }
    //Load assetbudle资源
    public GameObject[] LoadAssetBundle(WWW www) {
        GameObject[] prefabs = www.assetBundle.LoadAllAssets<GameObject>();
        GameTools.ClearAssetBundle(www.assetBundle);
        return prefabs;
    }
    //截图
    public IEnumerator CaptureRect(Rect rect, GameTools.Finish<Texture2D> Finish)
    {
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.temporaryCachePath + "/Screenshot.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        Finish(screenShot);
    }
    //清除缓存Assetbundle
    public void ClearAssetDatas() {
        foreach (var info in AssetDatas)
        {
            GameTools.ClearAssetBundle(info.Value);
        }
        AssetDatas.Clear();
    }
    void OnDestroy() {
        StopAllCoroutines();
    }
}
