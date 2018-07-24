using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class MonsterSkillCfg : TableDataMgr.TableDataBase {

	private List<MonsterSkill> _monsters = new List<MonsterSkill>();


	public MonsterSkillCfg(string tableName)
		: base(tableName){

        
		         
	}

	protected override void ExtractJSON(string json)
	{
		_monsters = JsonMapper.ToObject<List<MonsterSkill>>(json);
	}


    

}
