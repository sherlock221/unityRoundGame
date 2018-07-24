using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class EquipmentCfg : TableDataMgr.TableDataBase {

	private List<Equipment> _equipments = new List<Equipment>();
  

	public EquipmentCfg(string tableName)
        : base(tableName){
                
                 
    }

    protected override void ExtractJSON(string json)
    {
		_equipments = JsonMapper.ToObject<List<Equipment>>(json);
       
        
    }

   
    

}

