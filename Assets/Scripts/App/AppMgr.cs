using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMgr : Singleton<AppMgr> {


	private Camera _mainCamera;

	public Camera  MainCamera {
		get { 
			if(_mainCamera == null){
				_mainCamera = Camera.current;
			}
			return _mainCamera;
		}
	}


    

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public const string MusicValKey = "MusicValKey";
	public const string SoundValKey = "SoundValKey";

	public float MusicVal {
		get { return PlayerPrefs.GetFloat(MusicValKey); }
		set{
			float f = value > 1 ? 1 : value;
			f = value < 0 ? 0 : value;
			PlayerPrefs.SetFloat(MusicValKey, f);
		}
	}

	public float SoundVal
    {
		get { return PlayerPrefs.GetFloat(SoundValKey); }
        set
        {
            float f = value > 1 ? 1 : value;
            f = value < 0 ? 0 : value;
			PlayerPrefs.SetFloat(SoundValKey, f);
        }
    }
    
}
