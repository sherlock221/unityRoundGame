// **********************************************************************
// 
// 文件名(File Name)：             IResLoadListener.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/9/9 15:2:16
//
// **********************************************************************

using UnityEngine;
using System.Collections;

/// <summary>
/// 资源加载回调
/// </summary>
public interface IResLoadListener  
{
    void Finish(object asset);

    void Failure();
}
