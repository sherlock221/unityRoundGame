// **********************************************************************
// 
// 文件名(File Name)：             CreateResini.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/9/26 11:42:31
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class CreateResini  
{
    [MenuItem("youke/CreateResIni")]
    public static void Createini()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        string pathRes = Application.dataPath +"/Resources/";
        string pathIni = pathRes + "/res.txt";
        if (File.Exists(pathIni))
        {
            File.Delete(pathIni);
        }

        CreateResInfo(pathRes, ref dic);
        List<string> list = new List<string>();
        foreach(KeyValuePair<string,string> keyValue in dic)
        {
            list.Add(keyValue.Key +"="+keyValue.Value);
        }
        File.WriteAllLines(pathRes +"/res.txt",list.ToArray());
        Log.Debug("生成完毕 ");
        AssetDatabase.Refresh();
    }

    public static void CreateResInfo(string path,ref Dictionary<string,string>dic)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            return;
        }
        FileInfo[] files = dir.GetFiles();
        for (int i = 0; i < files.Length;i++ )
        {
            
            FileInfo info = files[i];
            if (!(info.Name.IndexOf(".meta",0) > 0))
            {
                
                string pathdir = info.FullName.Replace("\\","/")
                    .Replace((Application.dataPath + "/Resources/"), "")
                    .Replace(info.Name, "").TrimEnd('/');
                string fileName = Path.GetFileNameWithoutExtension(info.Name);
                Debug.Log("fileName =" + fileName);
                if (!dic.ContainsKey(info.Name))
                {
                    dic.Add(fileName, pathdir);
                }
                else
                {
                    Log.Error("存在相同的资源名称 名称为：" + info.Name + "/path1=" + dic[info.Name] + "/ path2 =" + pathdir);
                }
            }
        }
        DirectoryInfo[] dirs = dir.GetDirectories();
        if (dirs.Length > 0)
        {
            for (int i = 0; i < dirs.Length;i++ )
            {
                string tempPath = Path.Combine(path, dirs[i].Name);
                CreateResInfo(tempPath, ref dic);
            }
        }
    }
}
