using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {
	
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		StartGame();
        	
	}

	void Start () {
		
	}

    
    /// <summary>
    /// 开始游戏
    /// </summary>
	void StartGame(){
		
        //资源管理
		this.gameObject.AddComponent<ResMgr>();
        //ui管理
		this.gameObject.AddComponent<UIMgr>();
        //app
		this.gameObject.AddComponent<AppMgr>();

		this.gameObject.AddComponent<TableDataMgr>();



	}





}
