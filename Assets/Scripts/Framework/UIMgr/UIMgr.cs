// **********************************************************************
// 
// 文件名(File Name)：             UIMgr.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/9/14 12:31:20
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UIMgr : EventNode 
{
    private static UIMgr mInstance;
    public static UIMgr Instance
    {
        get
        {
            return mInstance;
        }
    }

    /// <summary>
    /// 所有UI
    /// </summary>
    private Dictionary<string, BaseUI> mDicUI = new Dictionary<string, BaseUI>();

    /// <summary>
    /// 添加一个UI
    /// </summary>
    /// <param name="ui"></param>
    public void AddUI(BaseUI ui)
    {
        if (ui != null)
        {
            mDicUI[ui.UIName] = ui;
            
        }
        
    }

    /// <summary>
    /// 移除一个UI
    /// </summary>
    /// <param name="ui"></param>
    public void RemoveUI(BaseUI ui)
    {
        if (ui != null && mDicUI.ContainsKey(ui.UIName))
        {
            mDicUI.Remove(ui.UIName);
        }
    }

    /// <summary>
    /// 所有命令集合
    /// </summary>
    public List<Command> cmdList = new List<Command>();

    internal Transform UIROOT = null;
	void Awake() 
	{
        UIROOT = this.transform.Find("UIRoot");
        mInstance = this;
        DontDestroyOnLoad(this.gameObject);
	}

    public List<string> GetCurrentOpenUI()
    {
        List<string> list = new List<string>();
       foreach(BaseUI ui in mDicUI.Values)
       {
           if (ui.CacheGameObject.activeInHierarchy)
           {
               list.Add(ui.UIName);
           }
       }
       return list;
    }

    /// <summary>
    /// 隐藏当前所有打开的界面
    /// </summary>
    public void HideAllUI()
    {
        List<string> list = GetCurrentOpenUI();
        for (int i = 0; i < list.Count;i++ )
        {
            HideUI(list[i]);
        }
    }

    #region 创建UI

    /// <summary>
    /// 创建UI
    /// </summary>
    /// <param name="uiName">UI名称</param>
    /// <param name="type">要绑定的脚本</param>
    /// <param name="listener">创建完成的回调</param>
    public void CreateUI(string uiName, Type type, ILoadUIListener listener)
    {
        cmdList.Add(Command.CreateCmd(type,uiName,listener));
    }

    /// <summary>
    /// 创建UI的实体部分
    /// </summary>
    /// <param name="cmd">命令</param>
    private void _Create(Command cmd)
    {
       
        BaseUI ui= null;
        mDicUI.TryGetValue(cmd.uiName, out ui);
        if (ui != null)
        {
            if (cmd.listener != null) cmd.listener.FiniSh(ui);
        }
        else
        {

            ResMgr.Instance.Load(cmd.uiName,new LoadResFinish(cmd),typeof(GameObject),true);
        }
    }

    #endregion

    #region 显示UI

    /// <summary>
    /// 显示一个UI界面  如果不存在就创建
    /// </summary>
    /// <param name="uiName">ui名称</param>
    /// <param name="type">要绑定的脚本</param>
    /// <param name="listener">如果界面不存在则会有界面加载完成后的回调</param>
    /// <param name="param">要传入的参数</param>
    public void ShowUI(string uiName, Type type, ILoadUIListener listener,object param = null,bool createCanCall = false)
    {
        Command cmd = Command.ShowCmd(uiName, listener, param, createCanCall);
        cmd.type = type;
        cmdList.Add(cmd);
    }

    /// <summary>
    /// 显示一个界面
    /// </summary>
    /// <param name="cmd"></param>
    private void _ShowUI(Command cmd)
    {

        BaseUI ui = null;
        mDicUI.TryGetValue(cmd.uiName, out ui);
        if (ui != null)
        {
            if (cmd.listener != null)
            {
                cmd.listener.FiniSh(ui);
            }
            ui.Show(cmd.param);
            
        }
    }

   
    #endregion

    #region  隐藏UI

    /// <summary>
    /// 隐藏这个UI
    /// </summary>
    /// <param name="uiName"></param>
    public void HideUI(string uiName)
    {
        
        cmdList.Add(Command.HideCmd(uiName));
    }


    private void _HideUI(Command cmd)
    {
        BaseUI ui = null;
        mDicUI.TryGetValue(cmd.uiName, out ui);
        if (ui != null)
        {
            ui.Hide();
        }
    }
#endregion

    #region  删除UI

    /// <summary>
    /// 删除UI
    /// </summary>
    /// <param name="uiName">UI名称</param>
    public void DestroyUI(string uiName)
    {
        cmdList.Add(Command.DestroyCmd(uiName));
    }

    private void _DestroyUI(Command cmd)
    {
        BaseUI ui = null;
        mDicUI.TryGetValue(cmd.uiName, out ui);
        if (ui != null)
        {
            mDicUI.Remove(ui.UIName);
            Destroy(ui.CacheGameObject);
        }
    }

    #endregion

	// Update is called every frame, if the MonoBehaviour is enabled.
	void Update() 
	{

        if (cmdList.Count > 0)
        {
            Command tempCmd = null;
            tempCmd = cmdList[0];
            if (tempCmd == null)
            {
                cmdList.RemoveAt(0);
            }
            else
            {
                switch(tempCmd.cmdType)
                {
                    case Command.CmdType.CreateAndShow:

                        _Create(tempCmd);
                        break;
                    case Command.CmdType.Create:
                        _Create(tempCmd);
                        break;
                    case Command.CmdType.Destroy:
                        _DestroyUI(tempCmd);
                        break;
                    case Command.CmdType.Hide:
                        
                        _HideUI(tempCmd);
                        break;
                    case Command.CmdType.Show:
                         BaseUI ui = null;
                         mDicUI.TryGetValue(tempCmd.uiName, out ui);
                        if (ui == null)
                        {
                            cmdList.RemoveAt(0);
                            cmdList.Insert(0,Command.CreateAndShowCmd(tempCmd.uiName, tempCmd.type,tempCmd.listener, tempCmd.param, tempCmd.createCanCall));
                            
                            return;
                        }
                        _ShowUI(tempCmd);
                        break;
                }
                cmdList.RemoveAt(0);
            }
        }
	}

    /// <summary>
    /// UI资源加载完成的回调
    /// </summary>
    public class LoadResFinish : IResLoadListener
    {
        /// <summary>
        /// 命令
        /// </summary>
        public Command cmd;
        public LoadResFinish(Command _cmd)
        {
            cmd = _cmd;
        }

        public void Finish(object asset)
        {

            if (cmd == null)
            {
                return;
            }
            GameObject go = Instantiate<GameObject>(asset as GameObject);
            go.SetActive(false);
            BaseUI ui =  go.AddComponent(cmd.type) as BaseUI;
            ui.UIName = cmd.uiName;
            go.gameObject.name = ui.UIName;
            ui.CacheTransform.SetParent(UIMgr.Instance.UIROOT, false);
            UIMgr.Instance.AddUI(ui);
            if (cmd.cmdType == Command.CmdType.CreateAndShow)
            {
                UIMgr.Instance.ShowUI(cmd.uiName, cmd.type, cmd.listener, cmd.param, cmd.createCanCall);
            }
            else if (cmd.createCanCall && cmd.listener != null)
            {
                cmd.listener.FiniSh(ui);
            }

            ui.UIInit();
        }

        public void Failure()
        {
            if (cmd.createCanCall && cmd.listener != null)
            {
                cmd.listener.Failure();
            }
        }
    }

    /// <summary>
    /// 界面加载回调
    /// </summary>
    public interface ILoadUIListener
    {
        void FiniSh(BaseUI ui);
        void Failure();
    }

    /// <summary>
    /// 操作UI命令集
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        public enum CmdType
        {
            /// <summary>
            /// 创建
            /// </summary>
            CreateAndShow,
            /// <summary>
            /// 创建
            /// </summary>
            Create,
            /// <summary>
            /// 显示或者刷新
            /// </summary>
            Show,
            /// <summary>
            /// 隐藏
            /// </summary>
            Hide,
            /// <summary>
            /// 删除
            /// </summary>
            Destroy,
        }

        /// <summary>
        /// UI名称
        /// </summary>
        public string uiName;

        /// <summary>
        /// 要绑定的脚本
        /// </summary>
        public Type type;

        /// <summary>
        /// 加载完成之后的回调
        /// </summary>
        public ILoadUIListener listener;

        /// <summary>
        /// 要传入的数据
        /// </summary>
        public object param;

        /// <summary>
        /// 命令类型
        /// </summary>
        public CmdType cmdType;

        /// <summary>
        /// 创建时候需要回调
        /// </summary>
        public bool createCanCall = true;

        /// <summary>
        /// 获取一个显示的命令
        /// </summary>
        /// <param name="_uiName">UI名称</param>
        /// <param name="_param">要传入的参数</param>
        public static Command CreateAndShowCmd(string uiName, Type type, ILoadUIListener listener, object param , bool createCanCall)
        {
            Command cmd = new Command(CmdType.CreateAndShow, uiName, type);
            cmd.createCanCall = createCanCall;
            cmd.listener = listener;
            cmd.type = type;
            cmd.param = param;
            return cmd;
        }
        /// <summary>
        /// 获取一个显示的命令
        /// </summary>
        /// <param name="_uiName">UI名称</param>
        /// <param name="_param">要传入的参数</param>
        public static Command ShowCmd(string _uiName,ILoadUIListener listener ,object _param, bool _createCanCall)
        {
            Command cmd = new Command(CmdType.Show, _uiName, _param);
            cmd.createCanCall = _createCanCall;
            cmd.listener = listener;
            return cmd;
        }


        /// <summary>
        /// 获取一个创建的命令
        /// </summary>
        /// <param name="_type">要绑定的脚本</param>
        /// <param name="_listener">加载完成之后的回调</param>
        public static Command CreateCmd(Type _type,string _uiName, ILoadUIListener _listener)
        {
            return new Command(CmdType.Create, _uiName, _type, _listener);
        }

        /// <summary>
        /// 获取一个隐藏命令
        /// </summary>
        /// <param name="_uiName">要隐藏的UI名称</param>
        /// <returns></returns>
        public static Command HideCmd(string _uiName)
        {
            return new Command(CmdType.Hide, _uiName,null);
        }

        /// <summary>
        /// 获取一个删除的命令
        /// </summary>
        /// <param name="_uiName">UI名称</param>
        /// <returns></returns>
        public static Command DestroyCmd(string _uiName)
        {
            return new Command(CmdType.Destroy, _uiName, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_cmdType">命令类型</param>
        /// <param name="_uiName">UI名称</param>
        /// <param name="_param">要传入的参数</param>
        public Command(CmdType _cmdType, string _uiName, object _param)
        {
            uiName = _uiName;
            cmdType = _cmdType;
            param = _param;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_cmdType">命令类型</param>
        /// <param name="_type">要绑定的脚本</param>
        /// <param name="_listener">加载完成之后的回调</param>
        public Command(CmdType _cmdType,string _uiName,Type _type,ILoadUIListener _listener)
        {
            cmdType = _cmdType;
            type = _type;
            listener = _listener;
            uiName = _uiName;
        }
    }
}
