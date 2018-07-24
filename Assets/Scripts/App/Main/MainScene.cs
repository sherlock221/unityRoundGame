using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : BaseUI,UIMgr.ILoadUIListener {
    
    
	private List<string> mTransNames = new List<string>(){"BtnMenu","BtnTask","BtnFigure","ExpBar","TextExp"};
	private List<Transform> mTransformTargets = new List<Transform>();


	Scrollbar mExpScrollBar;
	Text mExpText;
	float flag = 0.0f;

    
	private void Update()
	{
		if(flag < 1){
			flag += 0.01f;
		}
		else{
			flag = 0.0f;
		}

		mExpScrollBar.size = flag;
		mExpText.text = flag.ToString();
	}



	/// <summary>
	/// 初始化当前界面
	/// </summary>
	protected override void OnInit()
	{
		ComUtil.GetTransformInChild(mTransNames, this.CacheTransform, ref mTransformTargets);


		for (int i = 0; i < mTransformTargets.Count; i++)
		{
			Transform transform = mTransformTargets[i];

			if(transform.name.Equals(mTransNames[0])){
				Button button = transform.GetComponent<Button>();
				button.onClick.AddListener(BtnClickMenu);
			}
			else if (transform.name.Equals(mTransNames[1]))
            {
                Button button = transform.GetComponent<Button>();
                button.onClick.AddListener(BtnClickTask);
            }
			else if (transform.name.Equals(mTransNames[2]))
            {
                Button button = transform.GetComponent<Button>();
				button.onClick.AddListener(BtnClickFigure);
            }

			else if (transform.name.Equals(mTransNames[3]))
            {
				mExpScrollBar = transform.GetComponent<Scrollbar>();
            }
			else{
				mExpText = transform.GetComponent<Text>();
			}
		}

	}


	private void BtnClickMenu()
	{
		Log.Debug("菜单按钮点击..");

		UIMgr.Instance.ShowUI(UIDef.SettingsUI, typeof(SettingCtrl),this);
	}

	private void BtnClickTask()
    {
        Log.Debug("任务按钮点击..");
    }

	private void BtnClickFigure()
    {
		UIMgr.Instance.ShowUI(UIDef.PlayerMenuUI, typeof(PlayerMenuCtrl), this);
    }



	protected override void OnShow(object param)
	{
		
	}

	public void FiniSh(BaseUI ui)
	{
		
	}

	public void Failure()
	{
		
	}
}
