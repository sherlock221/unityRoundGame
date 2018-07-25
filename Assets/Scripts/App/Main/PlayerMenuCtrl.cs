using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMenuCtrl : BaseUI,IResLoadListener {


	/// <summary>
    /// 寻找游戏物体名称
    /// </summary>
    private List<string> mFindNames = new List<string>() { "0", "1", "2"};
    
    private List<PlayerUIBase> mTableList = new List<PlayerUIBase>(){null,null,null};
     
    /// <summary>
    /// 当前显示panel
    /// </summary>
    private PlayerUIBase mCurrent = null;


	/// <summary>
    /// 初始化当前界面
    /// </summary>
	protected override void OnInit() {

		ResMgr.Instance.Load(UIDef.PlayerUI, this);
		ResMgr.Instance.Load(UIDef.SkillUI, this);
		ResMgr.Instance.Load(UIDef.GoodsUI, this);


		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener(OnBtnClose);


		EventTrigger eventTrigger = ComUtil.FindTransformInChild(CacheTransform, "Close").GetComponent<EventTrigger>();
		eventTrigger.triggers.Add(entry);
        
		List<Transform> findTrans = new List<Transform>();
        ComUtil.GetTransformInChild(mFindNames, this.CacheTransform, ref findTrans);

        for (int i = 0; i < findTrans.Count; i++)
        {
            Transform cTran = findTrans[i];
			Toggle toggle = cTran.GetComponent<Toggle>();
			toggle.onValueChanged.AddListener((bool flag) => { OnValueChange(int.Parse(toggle.name), flag);});
        }
	
	}



	protected override void OnAwake() { }

    /// <summary>
    /// 显示当前界面
    /// </summary>
    /// <param name="param">附加参数</param>
	protected override void OnShow(object param) { 
        if (mCurrent)
        {
            mCurrent.OnShow();
        }
    }

    /// <summary>
    /// 隐藏当前界面
    /// </summary>
	protected override void OnHide() {
        if(mCurrent){
            mCurrent.OnHide();
        }
    }
    /// <summary>
    /// 删除当前UI 
    /// </summary>
	protected override void OnDestroy() { }
    


	private void OnValueChange(int id, bool flag)
    {   
		if(mCurrent != mTableList[id] && flag){
			mCurrent.gameObject.SetActive(false);
			mCurrent = mTableList[id];
            RefreshUI();
		}
    }



	public void Finish(object asset)
	{
		GameObject obj = asset as GameObject;



		int index = 0;
        Type type = typeof(PlayerInfoCtrl);
		switch (obj.name)
		{
			case  UIDef.PlayerUI :
				index = 0;
                type = typeof(PlayerInfoCtrl);
				break;

			case UIDef.SkillUI:
				index = 1;
                break;

			case UIDef.GoodsUI:
				index = 2;

                break;
			default:
				break;
		}

   

        GameObject instantObj = Instantiate(obj);
        PlayerUIBase playerUIBase = instantObj.AddComponent(type) as PlayerUIBase; ;
        mTableList[index] = playerUIBase;
        instantObj.transform.SetParent(CacheTransform,false);
        instantObj.SetActive(false);
        playerUIBase.OnInit();

		for (int i = 0; i < mTableList.Count; i++)
		{
			if (mTableList[i] == null) return; 
		}


		if (!mCurrent){
			mCurrent = mTableList[0];
            RefreshUI();			
		}

	}

	public void Failure()
	{
		
	}


    /// <summary>
    /// 刷新界面
    /// </summary>
    private void RefreshUI() { 
    
        mCurrent.gameObject.SetActive(true);
        mCurrent.OnShow();
    }


	private void OnBtnClose(BaseEventData e)
    {
		UIMgr.Instance.HideUI(this.UIName);
    }



    public abstract  class PlayerUIBase : MonoBehaviour {
        
        public abstract void OnInit();
        public abstract void OnShow();
        public abstract void OnDestory();
        public abstract void OnHide();
    }
}
