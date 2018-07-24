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
    
	private List<Transform> mTableList = new List<Transform>(){null,null,null};
     
    /// <summary>
    /// 当前显示panel
    /// </summary>
	private Transform mCurrent = null;


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
	protected override void OnShow(object param) { }

    /// <summary>
    /// 隐藏当前界面
    /// </summary>
	protected override void OnHide() { }
    /// <summary>
    /// 删除当前UI 
    /// </summary>
	protected override void OnDestroy() { }
    


	private void OnValueChange(int id, bool flag)
    {   
		if(mCurrent != mTableList[id] && flag){
			mCurrent.gameObject.SetActive(false);
			mCurrent = mTableList[id];
			mCurrent.gameObject.SetActive(true);
		}
    }



	public void Finish(object asset)
	{
		GameObject obj = asset as GameObject;

		int index = 0;
		switch (obj.name)
		{
			case  UIDef.PlayerUI :
				index = 0;
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

		mTableList[index] = Instantiate(obj).transform;	
		mTableList[index].SetParent(CacheTransform,false);
		mTableList[index].gameObject.SetActive(false);


		for (int i = 0; i < mTableList.Count; i++)
		{
			if (mTableList[i] == null) return; 
		}


		if (!mCurrent){
			mCurrent = mTableList[0];
			mCurrent.gameObject.SetActive(true);
		}

	}

	public void Failure()
	{
		
	}


	private void OnBtnClose(BaseEventData e)
    {
		UIMgr.Instance.HideUI(this.UIName);
    }
}
