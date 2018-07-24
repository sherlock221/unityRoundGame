using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class BufferCfg : TableDataMgr.TableDataBase {

	private List<Buff> _buffs = new List<Buff>();


	public BufferCfg(string tableName)
		: base(tableName){

        
		         
	}

	protected override void ExtractJSON(string json)
	{
		_buffs = JsonMapper.ToObject<List<Buff>>(json);



        
	}


    

}
