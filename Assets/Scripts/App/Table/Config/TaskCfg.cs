using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class TaskCfg : TableDataMgr.TableDataBase {

	private List<Task> _buffs = new List<Task>();


	public TaskCfg(string tableName)
		: base(tableName){

        
		         
	}

	protected override void ExtractJSON(string json)
	{
		_buffs = JsonMapper.ToObject<List<Task>>(json);
              
        
	}


    

}
