using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class AssetBundle : Editor
{
    //将选中目录下以及子目录下的所有的asset文件打包
    [MenuItem("GameObject/Build AssetBundles From Directory of AssetFiles")]
    private static void ExportAssetBundles()
    {
        //选中的目录
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        //选中目录中的子文件夹
        var childPaths = new Stack<string>();
        childPaths.Push(path);
        //待打包链表
        var waitForBundleList = new List<Object>();

        while (childPaths.Count != 0)
        {
            var curPath = childPaths.Pop();
            curPath = curPath.Replace("\\", "/");
            //1.获得所有当前目录下的子目录和子文件
            var fileEntries = Directory.GetFiles(curPath);
            var pathEntries = Directory.GetDirectories(curPath);
            //2.子目录加入到stack中
            foreach (var pathEntry in pathEntries)
            {
                childPaths.Push(pathEntry);
            }
            //3.子文件,是asset的加入到待打包的链表中
            foreach (var fileEntry in fileEntries)
            {
                //先加载
                var t = AssetDatabase.LoadMainAssetAtPath(fileEntry);
                if (t != null)
                {
                    waitForBundleList.Add(t);
                }
            }
        }

        //打包
        if (BuildPipeline.BuildAssetBundle(waitForBundleList[0], waitForBundleList.ToArray(),
            Application.streamingAssetsPath + "/" + "name.assetbundle", BuildAssetBundleOptions.CollectDependencies,
            BuildTarget.Android))
        {
            AssetDatabase.Refresh();
        }
    }
}