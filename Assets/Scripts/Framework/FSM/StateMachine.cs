// **********************************************************************
// 
// 文件名(File Name)：             StateMachine.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/8/16 18:29:38
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class StateMachine  
{
    /// <summary>
    /// 所有的状态集合
    /// </summary>
    private Dictionary<uint, IState> mStateDic = null;

    /// <summary>
    /// 当前状态
    /// </summary>
    private IState mCurrentState = null;

    /// <summary>
    /// 当前状态
    /// </summary>
    public IState CurrentState
    {
        get
        {
            return mCurrentState;
        }
    }

    /// <summary>
    /// 当前状态id
    /// </summary>
    public uint CurrentID
    {
        get
        {
            return mCurrentState == null ? 0 : mCurrentState.GetStateID();
        }
    }

    public StateMachine()
    {
        mStateDic = new Dictionary<uint, IState>();
        mCurrentState = null;
    }

    /// <summary>
    /// 注册一个状态
    /// </summary>
    /// <param name="state">状态对象</param>
    /// <returns>成功还是失败</returns>
    public bool RegisterState(IState state)
    {
        if (state == null)
        {
            Debug.LogError("StateMachine.RegisterState state is Null !");
            return false;
        }
        if (mStateDic.ContainsKey(state.GetStateID()))
        {
            Debug.LogError("StateMachine.RegisterState mStateDic hava this key key = " + state.GetStateID());
            return false;
        }
        mStateDic.Add(state.GetStateID(), state);
        return true;
    }

    /// <summary>
    /// 移除一个状态
    /// </summary>
    /// <param name="stateId">状态Id</param>
    /// <returns>当状态不存在或者状态正在运行那么返回失败</returns>
    public bool RemoveState(uint stateId)
    {
        if (!mStateDic.ContainsKey(stateId))
        {
            return false;
        }
        if (mCurrentState != null && mCurrentState.GetStateID() == stateId)
        {
            return false;
        }
        mStateDic.Remove(stateId);
        return true;
    }

    /// <summary>
    /// 获取一个状态
    /// </summary>
    /// <param name="stateId">状态id</param>
    /// <returns></returns>
    public IState GetState(uint stateId)
    {
        IState stste = null;
        mStateDic.TryGetValue(stateId, out stste);
        return stste;
    }

    /// <summary>
    /// 停止当前状态
    /// </summary>
    /// <param name="param1">参数1</param>
    /// <param name="param2">参数2</param>
    public void StopState(object param1,object param2)
    {
        if (null == mCurrentState)
        {
            return;
        }
        mCurrentState.OnLeave(null, param1, param2);
        mCurrentState = null;
    }

    /// <summary>
    /// 切换状态的回调
    /// </summary>
    public BetweenSwitchState BetweenSwitchStateCallBack = null;

    /// <summary>
    /// 切换状态回调委托
    /// </summary>
    /// <param name="from">当前状态</param>
    /// <param name="to">要跳转的状态</param>
    /// <param name="param1">参数1</param>
    /// <param name="param2">参数2</param>
    public delegate void BetweenSwitchState(IState from, IState to, object param1, object param2);

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newSatetId">要切换的状态id</param>
    /// <param name="param1">参数1</param>
    /// <param name="param2">参数2</param>
    /// <returns>如果不存在这个状态或者当前状态等于要切换的状态 那么返回失败</returns>
    public bool SwitchState(uint newSatetId, object param1, object param2)
    {
        if (mCurrentState != null && mCurrentState.GetStateID() == newSatetId)
        {
            return false;
        }
        IState newState = null;
        mStateDic.TryGetValue(newSatetId, out newState);
        if (newState == null)
        {
            return false;
        }
        if (mCurrentState != null)
        {
            mCurrentState.OnLeave(newState, param1, param2);
        }

        IState oldState = mCurrentState;

        mCurrentState = newState;

        if (BetweenSwitchStateCallBack != null)
        {
            BetweenSwitchStateCallBack(oldState, mCurrentState, param1, param2);
        }

        newState.OnEnter(this, oldState, param1, param2);

        return true;
    }

    /// <summary>
    /// 判断当前状态是否是摸个状态
    /// </summary>
    /// <param name="stateId">状态Id</param>
    /// <returns>是？不是</returns>
    public bool IsInState(uint stateId)
    {
        return mCurrentState == null ? false : mCurrentState.GetStateID() == stateId;
    }


    public void OnUpate()
    {
        if (mCurrentState != null)
        {
            mCurrentState.OnUpate();
        }
    }

    public void OnFixedUpdate()
    {
        if (mCurrentState != null)
        {
            mCurrentState.OnFixedUpdate();
        }
    }

    public void OnLeteUpdate()
    {
        if (mCurrentState != null)
        {
            mCurrentState.OnLeteUpdate();
        }
    }
}
