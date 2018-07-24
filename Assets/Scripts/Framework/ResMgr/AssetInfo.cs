// **********************************************************************
// 
// 文件名(File Name)：             AssetInfo.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/9/11 12:44:53
//
// **********************************************************************

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 资源信息
/// </summary>
public class AssetInfo
{
    /// <summary>
    /// 资源
    /// </summary>
    public object asset;

    /// <summary>
    /// 是否常驻内存
    /// </summary>
    public bool isKeepInMemory;

    /// <summary>
    /// 资源堆栈数量
    /// </summary>
    public int stackCount = 0;
}

/// <summary>
/// 资源加载信息
/// </summary>
public class RequestInfo
{
    /// <summary>
    /// 资源反馈信息
    /// </summary>
    public ResourceRequest request;

    /// <summary>
    /// 是否常驻内存
    /// </summary>
    public bool isKeepInMemory;

    /// <summary>
    /// 加载完成之后的回调
    /// </summary>
    public List<IResLoadListener> linsteners;

    public void AddListener(IResLoadListener listener)
    {
        if (linsteners == null)
        {
            linsteners = new List<IResLoadListener>() { listener };
        }
        else
        {
            if (!linsteners.Contains(listener))
            {
                linsteners.Add(listener);
            }
        }
    }

    /// <summary>
    /// 资源名称
    /// </summary>
    public string assetName;

    public string assetFullName
    {
        get
        {
            return ResMgr.Instance.GetFileFullName(assetName);
        }
    }

    /// <summary>
    /// 资源类型
    /// </summary>
    public Type type;

    /// <summary>
    /// 资源是否加载完成
    /// </summary>
    public bool IsDone
    {
        get
        {
            return (request != null && request.isDone);
        }
    }

    /// <summary>
    /// 加载到的资源
    /// </summary>
    public object Asset
    {
        get
        {
            return request != null ? request.asset : null;
        }
    }

    public void LoadAsync()
    {

        request = type == null ? Resources.LoadAsync(assetFullName) : Resources.LoadAsync(assetFullName,type);
    }
}
