using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class MonsterInfoCfg : TableDataMgr.TableDataBase {

	private List<MonsterInfo> _monsterInfos = new List<MonsterInfo>();


	public MonsterInfoCfg(string tableName)
		: base(tableName){

        
		         
	}

	protected override void ExtractJSON(string json)
	{
		_monsterInfos = JsonMapper.ToObject<List<MonsterInfo>>(json);

	}


    

}
