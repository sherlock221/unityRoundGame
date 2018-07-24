// **********************************************************************
// 
// 文件名(File Name)：             IEventListener.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/8/23 23:4:8
//
// **********************************************************************

using UnityEngine;
using System.Collections;

/// <summary>
/// 消息监听接口
/// </summary>
public interface IEventListener   
{
    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="id">消息Id</param>
    /// <param name="param1">参数1</param>
    /// <param name="param2">参数2</param>
    /// <returns>是否终止消息派发</returns>
    bool HandleEvent(int id, object param1, object param2);

    /// <summary>
    /// 消息优先级
    /// </summary>
    /// <returns></returns>
    int EventPriority();
}
