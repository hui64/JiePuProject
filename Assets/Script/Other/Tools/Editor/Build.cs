using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
public class Build{
    [MenuItem("Build/Create AssetsAndVersion")]
    static void CreateAssets()
    {
        Dictionary<string, string> fileData = new Dictionary<string, string>();
        Directory.Delete(AssetPath.StreamingPath, true);
        Directory.CreateDirectory(AssetPath.StreamingPath);
        AssetPath.NewText(AssetPath.VersionPath);
        List<string> assetFullPaths = AssetPath.GetNeedBuildPath();
        foreach (string fullPath in assetFullPaths)
        {
            Debug.Log(fullPath);
            AssetPath.GetFilePathsAndMD5(fullPath, fileData);
        }
        AssetPath.WriteVersion(AssetPath.VersionPath, fileData);             //写入版本配置文件
        AssetDatabase.Refresh();
        AssetBundleBuild[] buildMap = new AssetBundleBuild[fileData.Count];
        List<string> paths = new List<string>();
        foreach (string path in fileData.Keys)
        {
            paths.Add(path);
        }
        for (int i = 0; i < buildMap.Length; i++)
        {
            //此资源包下文件
            string[] path = new string[1];
            path[0] = AssetPath.GetPathHaveBegin(paths[i], "Assets");
            buildMap[i].assetNames = path;
            buildMap[i].assetBundleName = AssetPath.GetFileName(path[0]);   //打包的资源包名称
            Debug.Log(AssetPath.GetFileName(path[0]));
        }
        BuildPipeline.BuildAssetBundles(AssetPath.StreamingPath, buildMap, BuildAssetBundleOptions.None, BuildTarget.Android);
        AssetDatabase.Refresh();                                             //刷新编辑器
    }
}
