using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System;

public class AssetPath
{
    public static AssetBundleManifest AssetDep;                                                      //资源的依赖关系
    public static string DepPath = "/assetBundle";                                                   //依赖关系地址
    public static readonly string EndPath = ".assetbundle";
    private static readonly string VersionName = "/versionBundle/version.txt";                       //版本相对路径文件名
    public static readonly string UpURL = "http://192.168.0.135:8088/assets/upload/";              //资源服务器上传地址
    public static readonly string DownURL = "http://192.168.0.135:8088/assets/download?filename=";       //资源服务器下载地址
    //public static readonly string UpURL = "http://192.168.0.138:8088/assets/upload/";              //资源服务器上传地址
    //public static readonly string DownURL = "http://192.168.0.138:8088/assets/download?filename=";
    public static string AssetBundlePath = Application.temporaryCachePath;                           //assetbundle资源包保存路径
    public static string VersionPath = Application.dataPath + VersionName;
    public static string VersionBundlePath = AssetBundlePath + ChangPathEnd(VersionName, EndPath);
    public static string VersionBundleURL = DownURL + ChangPathEnd(VersionName, EndPath);
    public static string BuildFileEndPath = "Bundle";                                                //需要打包的资源路径                         
    public static string StreamingPath = Application.streamingAssetsPath + "/assetBundle";           //打包资源中转站
    public static string DepBundlePath = AssetBundlePath + DepPath;                                  //依赖关系路径
    public static string DepBundleURL = DownURL + DepPath;                                           //依赖关系URL

    public static string PrefabPath = "/prefabbundle/";
    public static string PrefabBundlePath = GetWWWPath(AssetBundlePath + PrefabPath);
    public static string TexturePath = "/texturebundle/";
    public static string TextureBundlePath = GetWWWPath(AssetBundlePath + TexturePath);
    public static string TextPath = "/textbundle/";
    public static string TextBundlePath = GetWWWPath(AssetBundlePath + TextPath);
    //获取从begin开始的字符串包括begin
    public static string GetPathHaveBegin(string myString, string begin)
    {
        int i = myString.IndexOf(begin);
        return myString.Substring(i);
    }
    //获取从begin开始的字符串不包括begin
    public static string GetPathOutBegin(string myString, string begin)
    {
        if (myString.IndexOf(begin) == -1)
            return myString;
        int i = myString.IndexOf(begin) + begin.Length;
        return myString.Substring(i);
    }
    public static string GetFileName(string fullPath)
    {
        string myString = GetPathOutBegin(fullPath, "Assets");
        return ChangPathEnd(myString, EndPath);
    }
    public static string ChangPathEnd(string path, string end) {
        return path.Split('.')[0] + end;
    }
    //根据文件名获取本地资源总路径
    public static string GetFullAssetBundlePath(string path)
    {
        return AssetBundlePath + ReplaceString(path);
    }
    //根据文件名获取本地资源www总路径
    public static string GetWWWAssetBundlePath(string path) {
        return GetWWWPath(AssetBundlePath + "/" + ReplaceString(path));
    }
    //根据文件名获取服务器资源地址
    public static string GetDownLoadURL(string path)
    {
        return DownURL + ReplaceString(path);
    }
    public static string ReplaceString(string path) {
        return (StringTool.Trim(path)).Replace(@"\", @"/");
    }
    //根据文件路径创建文件夹 到最后一个文件夹
    public static void CreateFile(string path) {
        string[] paths = path.Split('/');
        string info = "";
        for (int i = 0; i < paths.Length - 1; i++)
        {
            if (i != 0)
                info += "/" + paths[i];
            else
                info += paths[i];
        }
        Directory.CreateDirectory(info);
    }
    //获取需要打包的文件夹
    public static List<string> GetNeedBuildPath()
    {
        List<string> assetPaths = new List<string>();
        //获取路径下的所有文件
        DirectoryInfo Dir = new DirectoryInfo(Application.dataPath);
        //获取所有子路径
        foreach (DirectoryInfo info in Dir.GetDirectories())
        {
            if (info.Name.EndsWith(BuildFileEndPath))
                assetPaths.Add(info.FullName);
        }
        return assetPaths;
    }
    //获取该路径下的所有文件路径名
    public static void GetAllPaths(string targetPath, List<string> paths)
    {
        //获取路径下的所有文件
        DirectoryInfo Dir = new DirectoryInfo(targetPath);
        //获取所有子路径
        foreach (DirectoryInfo info in Dir.GetDirectories())
        {
            GetAllPaths(info.FullName, paths);
        }
        //获取所有子文件
        foreach (FileInfo info in Dir.GetFiles())
        {
            if (info.FullName.EndsWith(EndPath) || info.FullName.EndsWith("assetBundle"))
            {
                paths.Add(GetWWWPath(info.FullName));
            }
        }
    }
    public static string GetWWWPath(string path)
    {
        return "file:///" + path.Replace(@"\", @"/");
    }
    public static void GetFilePathsAndMD5(string fullPath, Dictionary<string, string> versions)
    {
        //获取路径下的所有文件
        DirectoryInfo Dir = new DirectoryInfo(fullPath);
        //获取所有子路径
        foreach (DirectoryInfo info in Dir.GetDirectories())
        {
            GetFilePathsAndMD5(info.FullName, versions);
        }
        //获取所有子文件
        foreach (FileInfo info in Dir.GetFiles())
        {
            if (info.Name.EndsWith(".meta"))
                continue;
            Debug.Log(info.FullName);
            versions.Add(info.FullName, GetMD5(info.FullName));
        }
    }
    //获取文件的MD5
    public static string GetMD5(string fileName)
    {
        FileStream file = new FileStream(fileName, FileMode.Open);
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] retVal = md5.ComputeHash(file);
        file.Close();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < retVal.Length; i++)
        {
            sb.Append(retVal[i].ToString("x2"));
        }
        return sb.ToString();
    }
    //新建文本文件
    public static void NewText(string fullpath) {
        StreamWriter sw = File.CreateText(fullpath);
        sw.Close();
    }
    //写入map
    public static void WriteVersion(string fullPath, Dictionary<string, string> pathAndMD5)
    {
        StringBuilder mySB = new StringBuilder();
        foreach (var item in pathAndMD5)
        {
            mySB.Append(item.Key).Append(",").Append(item.Value).Append("\n");
        }
        FileStream stream = new FileStream(fullPath, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(mySB.ToString());
        stream.Write(data, 0, data.Length);
        stream.Flush();
        stream.Close();
        //WriteByUTF8(fullPath, mySB.ToString());
    }
    //写入文本并且变为UTF8格式
    public static void WriteByUTF8(string path, string myString) {
        StreamWriter stream = new StreamWriter(path, false, Encoding.UTF8);
        UTF8Encoding utf8 = new UTF8Encoding(); // Create a UTF-8 encoding.
        byte[] bytes = utf8.GetBytes(myString.ToCharArray());
        string EnUserid = utf8.GetString(bytes);
        stream.WriteLine(EnUserid);
        stream.Flush();
        stream.Close();
    }
    //获取文本
    public static string GetAssetText(WWW www)
    {
        TextAsset[] texts = new TextAsset[1];
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("获取txt");
            texts = www.assetBundle.LoadAllAssets<TextAsset>();
            www.assetBundle.Unload(false);
            return texts[0].text;
        }
        return "";
    }
    //获得图片
    public static Sprite GetAssetSprite(WWW www)
    {
        if (string.IsNullOrEmpty(www.error))
        {
            Sprite[] sprites = www.assetBundle.LoadAllAssets<Sprite>();
            return sprites[0];
        }
        return null;
    }
    //获得预制
    public static GameObject GetAssetPrefab(WWW www)
    {
        //GameObject prefab = new GameObject();
        if (string.IsNullOrEmpty(www.error))
        {
            string[] names = www.assetBundle.GetAllAssetNames();
            //先加载依赖资源
            string[] depPaths = AssetDep.GetAllDependencies(names[0]);
            //prefabs = www.assetBundle.LoadAllAssets<GameObject>();
          
        }
        return null;
    }
    public void LoadDepAsset(string path, GameTools.Finish finish) {
        string[] paths = AssetDep.GetAllDependencies(path);
        foreach (string info in paths) {
         
        }
    }
}

