using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCtrl : BaseUI,UIMgr.ILoadUIListener {

    /// <summary>
    /// 寻找游戏物体名称
    /// </summary>
	private List<string> mFindNames = new List<string>() {"BtnContinue","BtnExit","MusicBar","SoundBar"};


	private Scrollbar mMusicBar = null;
	private Scrollbar mSoundBar = null;



	/// <summary>
    /// 初始化当前界面
    /// </summary>
	protected override void OnInit() {
		List<Transform> findTrans = new List<Transform>();
		ComUtil.GetTransformInChild(mFindNames, this.CacheTransform, ref findTrans);

		for (int i = 0; i < findTrans.Count; i++)
		{
			Transform cTran = findTrans[i];
			if(cTran.name.Equals(mFindNames[0])){
				Button button = cTran.GetComponent<Button>();
				button.onClick.AddListener(OnClickBtnContinue);
			}
			else if (cTran.name.Equals(mFindNames[1]))
            {
                Button button = cTran.GetComponent<Button>();
				button.onClick.AddListener(OnClickBtnExit);
            }
			else if (cTran.name.Equals(mFindNames[2]))
            {
				mMusicBar = cTran.GetComponent<Scrollbar>();
				mMusicBar.onValueChanged.AddListener(OnMusicBarValueChanged);
            }
			else if (cTran.name.Equals(mFindNames[3]))
            {
				mSoundBar = cTran.GetComponent<Scrollbar>();
				mSoundBar.onValueChanged.AddListener(OnSoundBarValueChanged);
            }
			                           

		}

	}



	protected override void OnAwake() { }

    /// <summary>
    /// 显示当前界面
    /// </summary>
    /// <param name="param">附加参数</param>
	protected override void OnShow(object param) {
	    
		if(mMusicBar != null){
			mMusicBar.value = AppMgr.Instance.MusicVal;
		}

		if (mSoundBar != null)
        {
			mSoundBar.value = AppMgr.Instance.SoundVal;
        }
	
	}

    /// <summary>
    /// 隐藏当前界面
    /// </summary>
	protected override void OnHide() { }
    /// <summary>
    /// 删除当前UI 
    /// </summary>
	protected override void OnDestroy() { }



    /// <summary>
    /// 继续按钮点击
    /// </summary>
	private void OnClickBtnContinue()
    {
		UIMgr.Instance.HideUI(this.UIName);
    }


    /// <summary>
    /// 退出
    /// </summary>
	private void OnClickBtnExit()
    {
		Application.Quit();
    }



    /// <summary>
    /// 背景音乐变化
    /// </summary>
    /// <param name="arg0">Arg0.</param>
	private void OnMusicBarValueChanged(float v)
    {
		AppMgr.Instance.MusicVal = v;
        
    }

    /// <summary>
    /// 音效变化
    /// </summary>
    /// <param name="arg0">Arg0.</param>
	private void OnSoundBarValueChanged(float v)
    {
		AppMgr.Instance.SoundVal = v;
    }



	public void FiniSh(BaseUI ui)
	{
		
	}

	public void Failure()
	{
		
	}
}
