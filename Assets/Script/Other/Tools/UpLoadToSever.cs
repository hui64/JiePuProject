using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEngine.UI;

public class UpLoadToSever : MonoBehaviour {
    public Text MyText;
    public Text MyText01;
    public Text MyText02;
    public Text MyText03;
    public Text MyText04;
    // Use this for initialization
    void Start () {
        StartCoroutine(ClearSeverData());
        //StartCoroutine(DownLoad());
	}
    /*
       推送文件到资源服务器
   */
    private IEnumerator UpLoad()
    {
        //AssetPath.Init();
        print(AssetPath.AssetBundlePath);
        List<string> paths = new List<string>();
        AssetPath.GetAllPaths(AssetPath.StreamingPath, paths);
        foreach (string info in paths)
        {
            print(info);
            WWW www = new WWW(info);
            //生成版本资源更新
            yield return www;
            WWWForm form = new WWWForm();
            byte[] infos = www.bytes;
            print(www.bytes.Length);
            form.AddBinaryData("aaaa", www.bytes, AssetPath.GetPathOutBegin(info, "assetBundle"));
            WWW upLoad = new WWW(AssetPath.UpURL, form);
            yield return upLoad;
        }
    }
    private IEnumerator ClearSeverData() {
        print("====");
        WWWForm form = new WWWForm();
        form.AddField("clear", "true");
        WWW upLoad = new WWW(AssetPath.UpURL, form);
        yield return upLoad;
        print("startLoad");
        StartCoroutine(UpLoad());
    }
    private IEnumerator DownLoad()
    {
        //AssetPath.Init();
        print(AssetPath.AssetBundlePath);
        //print(AssetPath.VersionBundleURL);
        MyText03.text = AssetPath.VersionBundleURL;
        List<string> paths = new List<string>();
        WWW versionWWW = new WWW(AssetPath.VersionBundleURL);
        yield return versionWWW;
        MyText02.text = versionWWW.error;
        MyText.text = "请求版本文件";
        TextAsset[] texts = versionWWW.assetBundle.LoadAllAssets<TextAsset>();

        versionWWW.assetBundle.Unload(false);
        MyText01.text = texts[0].text;
        string[] items = texts[0].text.Split('\n');
        foreach (string item in items)
        {
            string[] info = item.Split( ',');
            paths.Add(info[0]);
            MyText.text = info[0];
        }
        yield return new WaitForFixedUpdate();
      
        MyText03.text = paths.Count + "==========";
        List<object> Gos = new List<object>();
        List<Sprite> Sprites = new List<Sprite>();
        paths.RemoveAt(paths.Count - 1);
        paths.RemoveAt(paths.Count - 1);
        Sprite[] textures = new Sprite[1];
        foreach (string info in paths)
        {
            MyText02.text = info;
            string url = AssetPath.DownURL + AssetPath.GetPathOutBegin(info, "Assets");
            string end = url.Split('.').Last();
            url = url.Replace(end, "assetbundle");
            WWW www = new WWW(url.ToLower().Trim());
            yield return www;
            MyText.text = url;
            print(info);
            GameObject[] gos01 = new GameObject[1];
            try
            {
                gos01 = www.assetBundle.LoadAllAssets<GameObject>(); }
            catch (Exception e) {
                Debug.LogError(e);
            }
            foreach (GameObject go in gos01)
            {
                Instantiate(go);
                Gos.Add(go);
               // yield return new WaitForFixedUpdate();
            }
            textures = www.assetBundle.LoadAllAssets<Sprite>();
            if(textures.Length > 0)
                Sprites.Add(textures[0]);
        }
        foreach (GameObject go in Gos)
        {
            try
            {
                if (go.name == "LoginUI") {
                //
                    print("======================");
                    //GameObject info = Instantiate(go);
                    //info.transform.parent = MyText04.transform;
                   // print(info.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>());
                    //info.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Sprites[0];
                   // print(Sprites[0].name);
                }
            }
            catch (Exception e) {
                print(e);
            };
            yield return new WaitForFixedUpdate();
        }
    }
}
