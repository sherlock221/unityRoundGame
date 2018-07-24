

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TableDataMgr : MonoBehaviour 
{
    public static TableDataMgr Instance
    {
        private set;
        get;
    }

    /// <summary>
    /// 加载的配置表
    /// </summary>
	private List<TableDataBase> _LoadTableList = new List<TableDataBase>();

    /// <summary>
    /// Buffer
    /// </summary>
	private BufferCfg bufferCfg;

    /// <summary>
    /// 装备
    /// </summary>
	private EquipmentCfg equipmentCfg;

    /// <summary>
    /// 物品
    /// </summary>
	private GoodCfg goodCfg;

    /// <summary>
    /// 怪物
    /// </summary>
	private MonsterCfg monsterCfg;

    /// <summary>
    /// 怪物信息
    /// </summary>
	private MonsterInfoCfg monsterInfoCfg;

    /// <summary>
    /// 怪物技能
    /// </summary>
	private MonsterSkillCfg monsterSkillCfg;

    /// <summary>
    /// 技能
    /// </summary>
	private SkillCfg skillCfg;



	private void Awake()
	{      
		Instance = this;

		bufferCfg = new BufferCfg("Buff"); 
		equipmentCfg = new EquipmentCfg("Equipment"); 
		goodCfg = new GoodCfg("Goods"); 
		monsterCfg = new MonsterCfg("Monster"); 
		monsterSkillCfg = new MonsterSkillCfg("MonsterSkill"); 
		monsterInfoCfg = new MonsterInfoCfg("MonsterInfo"); 
		skillCfg = new SkillCfg("Skill"); 
       
        
		for (int i = 0; i < _LoadTableList.Count; i++)
		{
			ResMgr.Instance.Load(_LoadTableList[i].tableName, _LoadTableList[i]);
		}

	}


    /// <summary>
    /// 配置表基类
    /// </summary>
	public class TableDataBase : IResLoadListener
    {
        public string tableName;

        public TableDataBase(string tableName)
        {
            this.tableName = tableName;

			Instance._LoadTableList.Add(this);
        }


        public void Failure()
        {

        }

        public void Finish(object asset)
        {
            TextAsset assetText = asset as TextAsset;
            if (assetText != null)
            {
                ExtractJSON(assetText.text);
            }

			Instance._LoadTableList.Remove(this);

            //全家加载完成
			if(Instance._LoadTableList.Count == 0 ){
				EntranceSceneCtrl.Instance.SendEvent(EventDef.TableDataFinish, null, null);
			}
            
        }

        /// <summary>
        /// 解析json数据
        /// </summary>
        /// <param name="json">Json.</param>
        protected virtual void ExtractJSON(string json)
        {

        }
    }




}
