using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoCtrl : PlayerMenuCtrl.PlayerUIBase
{

    private List<string> mFindNames = new List<string>() { "TextLevel","TextAttack","TextDef","TextHP","TextMP","BtnHead","BtnChest","BtnWeapon","BtnLeg","BtnShoulder"};
    private Text mTextLevel;
    private Text mTextAttack;
    private Text mTextDef;
    private Text mTextHP;
    private Text mTextMP;
  

    public override void OnDestory()
    {
        
    }

    public override void OnHide()
    {
        
    }

    public override void OnInit()
    {
        List<Transform> findTrans = new List<Transform>();
        ComUtil.GetTransformInChild(mFindNames, this.transform, ref findTrans);
        for (int i = 0; i < findTrans.Count; i++)
        {
            Transform tran = findTrans[i];
            //处理所有按钮
            if(tran.name.Contains("Btn")){
                Button button = tran.GetComponent<Button>();
                button.onClick.AddListener(()=>{
                    OnBtnClick(button);
                });
            }
            //处理所有文本
            else if (tran.name.Contains("Text")){
                InitTextLayer(tran);
            }
        }
    }

    private void InitTextLayer(Transform tran)
    {
      
        if(mFindNames[0].Equals(tran.name)){

            mTextLevel = tran.GetComponent<Text>();
        }
        else if (mFindNames[1].Equals(tran.name))
        {
            mTextAttack = tran.GetComponent<Text>();
        }
        else if (mFindNames[2].Equals(tran.name))
        {
            mTextDef = tran.GetComponent<Text>();
        }
        else if (mFindNames[3].Equals(tran.name))
        {
            mTextHP = tran.GetComponent<Text>();
        }
        else if (mFindNames[4].Equals(tran.name))
        {
            mTextMP = tran.GetComponent<Text>();
        }
               
    }

    private void OnBtnClick(Button button)
    {
        Log.Debug("btn name" + button.name);
    }

    public override void OnShow()
    {

        if(mTextLevel != null){
            mTextLevel.text = String.Format("Lv.{0}",PlayerData.Instance.Level);
        }

        if (mTextAttack != null)
        {
            mTextAttack.text = String.Format("攻击 : {0}", PlayerData.Instance.Attack);
        }

        if (mTextDef != null)
        {
            mTextDef.text = String.Format("防御 : {0}", PlayerData.Instance.DEF);
        }

        if (mTextHP != null)
        {
            mTextHP.text = String.Format("生命 : {0}", PlayerData.Instance.HP);
        }

        if (mTextMP != null)
        {
            mTextMP.text = String.Format("魔法 : {0}", PlayerData.Instance.MP);
        }

    }

   

}
