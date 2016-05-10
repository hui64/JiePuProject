using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Linq;

public class LoadAssets : BaseMonoBehaviour {

    public static Dictionary<string, string> LocalVersion;
    public static Dictionary<string, string> SeverVersion;
    public static bool IsCanUpDateVersion = true;
    private List<string> NeedDownLoadFiles;
    private string VersionMD5;

    void Awake() {
        Init();
    }
    void Start() {
        //开始更新资源
        DownLocalVersion();
    }
  
    public void Init()
    {
        LocalVersion = new Dictionary<string, string>();
        SeverVersion = new Dictionary<string, string>();
        NeedDownLoadFiles = new List<string>();
    }
    private void DownLocalVersion() {
        DownLoad(AssetPath.GetWWWPath(AssetPath.VersionBundlePath), delegate (WWW www)
        {
            //保存本地version
            ParseVersionPath(AssetPath.GetAssetText(www), LocalVersion);
            //加载服务端version配置  
            DownLoad(AssetPath.VersionBundleURL, delegate(WWW serverVersion)
            {
                //保存服务端version  
                ParseVersionPath(AssetPath.GetAssetText(serverVersion), SeverVersion);
                //计算出需要重新加载的资源  
                CompareVersion();
                //加载需要更新的资源  
                DownLoadRes();
            });
        });
    }
    //依次加载需要更新的资源  
    private void DownLoadRes()
    {
        if (NeedDownLoadFiles.Count == 0)
        {
            LoadDep();
            return;
        }
        string fileName = "";
        if (NeedDownLoadFiles[0] != AssetPath.DepPath)
            fileName = AssetPath.GetFileName(NeedDownLoadFiles[0]);
        else
            fileName = NeedDownLoadFiles[0];
        NeedDownLoadFiles.RemoveAt(0);
        DownLoad(AssetPath.GetDownLoadURL(fileName), delegate (WWW w)
        {
            //将下载的资源替换本地旧的资源  
            ReplaceLocalRes(AssetPath.GetFullAssetBundlePath(fileName), w.bytes);
            DownLoadRes();
        });
    }
    //更新本地的version配置  
    private void UpdateLocalVersionPath()
    {
        if (IsCanUpDateVersion)
        {
            StringBuilder versions = new StringBuilder();
            foreach (var item in SeverVersion)
            {
                versions.Append(item.Key).Append(",").Append(item.Value).Append("\n");
            }

            FileStream stream = new FileStream(AssetPath.VersionBundlePath, FileMode.Create);
            byte[] data = Encoding.UTF8.GetBytes(versions.ToString());
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Close();
        }
    }
    private void ReplaceLocalRes(string fileName, byte[] data)
    {
        print("保存文件" + fileName);
        print(data.Length);
        //使用www读取 流应该有问题
        AssetPath.CreateFile(fileName);
        FileStream stream = new FileStream(fileName, FileMode.Create);
        BufferedStream buffStream = new BufferedStream(stream);
        buffStream.Write(data, 0, data.Length);
        buffStream.Flush();
        stream.Flush();
        buffStream.Close();
        stream.Close();
    }
    private void CompareVersion()
    {
        //比对版本号
        if (!IsCanUpDateVersion)
        {
            print("已经是最新版本");
            return;
        }
        NeedDownLoadFiles.Add(AssetPath.DepPath);

        foreach (var version in SeverVersion)
        {
            string fileName = version.Key;
            string serverMd5 = version.Value;
            //新增的资源  
            if (!LocalVersion.ContainsKey(fileName))
            {
                print(fileName);
                NeedDownLoadFiles.Add(fileName);
            }
            else
            {
                //需要替换的资源  
                string localMd5;
                LocalVersion.TryGetValue(fileName, out localMd5);
                if (!serverMd5.Equals(localMd5))
                {
                    NeedDownLoadFiles.Add(fileName);
                }
            }
        }
        //本次有更新，同时更新本地的version.txt  
        IsCanUpDateVersion = NeedDownLoadFiles.Count > 0;
    }
    private void ParseVersionPath(string content, Dictionary<string, string> dict)
    {
        print("版本文件:" + content);
        if (content == null || content.Length == 0)
        {
            IsCanUpDateVersion = true;
            return;
        }
        string[] items = content.Split(new char[] { '\n' });
        foreach (string item in items)
        {
            string[] info = item.Split(new char[] { ',' });
            if (info != null && info.Length == 2)
            {
                dict.Add(info[0], info[1]);
                //对比版本号
                if (info[0].Contains("version.txt")) {
                    if (string.IsNullOrEmpty(VersionMD5))
                    {
                        VersionMD5 = info[1];
                    }
                    else {
                        IsCanUpDateVersion = VersionMD5 == info[1];
                        print(IsCanUpDateVersion);
                    }
                }
            }
        }
    }
    //加载依赖关系
    private void LoadDep()
    {
        print("加载依赖关系");
        DownLoad(AssetPath.GetWWWPath(AssetPath.DepBundlePath), delegate (WWW www)
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
            }
            else
            {
                AssetBundle mab = www.assetBundle;
                AssetPath.AssetDep = (AssetBundleManifest)mab.LoadAsset("AssetBundleManifest");
                //AssetPath.AssetDep.GetAllDependencies("");
            }
            //跟新完毕进入游戏
            GameStart();
        });
    }
    private void GameStart (){
        Debug.Log("=================  " + "运行游戏" + "  ====================");
        DownLoadPrefab("UIPrefab/LoginUI", delegate(GameObject go) {
            Instantiate(go);
        });
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DownLoadPrefab("UIPrefab/LoginUI", delegate (GameObject go)
            {
                Instantiate(go);
            });
        }
    }
}
