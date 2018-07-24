// **********************************************************************
// 
// 文件名(File Name)：             IMode
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/12/21 2:31:49
//
// 网址：                          www.youke.pro
//
// **********************************************************************

using UnityEngine;
using System.Collections;

public interface IMode  
{
    /// <summary>
    /// 初始化数据
    /// </summary>
    void InitData();

    /// <summary>
    /// 在登陆的时候要做的事情
    /// </summary>
    void OnLogin();

    /// <summary>
    /// 清除缓存数据
    /// </summary>
    void OnClear();
}
