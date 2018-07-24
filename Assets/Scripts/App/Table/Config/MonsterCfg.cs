using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class MonsterCfg : TableDataMgr.TableDataBase {

	private List<Monster> _monsters = new List<Monster>();


	public MonsterCfg(string tableName)
		: base(tableName){

        
		         
	}

	protected override void ExtractJSON(string json)
	{
		_monsters = JsonMapper.ToObject<List<Monster>>(json);
	}


    

}
