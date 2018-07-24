using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class SkillCfg : TableDataMgr.TableDataBase {

	private List<Skill> _skills = new List<Skill>();


	public SkillCfg(string tableName)
		: base(tableName){

        
		         
	}

	protected override void ExtractJSON(string json)
	{
		_skills = JsonMapper.ToObject<List<Skill>>(json);
      
        
	}


    

}
