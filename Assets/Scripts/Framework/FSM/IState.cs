// **********************************************************************
// 
// 文件名(File Name)：             IState.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/8/16 18:24:18
//
// **********************************************************************

using UnityEngine;
using System.Collections;

public interface IState  
{
    /// <summary>
    /// 获取这个状态机的状态
    /// </summary>
    /// <returns></returns>
    uint GetStateID();

    /// <summary>
    /// 进入这个状态
    /// </summary>
    /// <param name="machine">状态机</param>
    /// <param name="prevState">上一个状态</param>
    /// <param name="param1">参数1</param>
    /// <param name="param2">参数2</param>
    void OnEnter(StateMachine machine, IState prevState, object param1, object param2);
    
    /// <summary>
    /// 离开当前这个状态
    /// </summary>
    /// <param name="nextState">下一个要进入的状态</param>
    /// <param name="param1">参数1</param>
    /// <param name="param2">参数2</param>
    void OnLeave(IState nextState, object param1, object param2);

    void OnUpate();
    void OnFixedUpdate();
    void OnLeteUpdate();
}
