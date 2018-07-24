// **********************************************************************
// 
// 文件名(File Name)：             Singleton.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/9/12 17:32:34
//
// **********************************************************************

using UnityEngine;
using System.Collections;

public class Singleton <T> :MonoBehaviour
    where T : MonoBehaviour
{
    private static T mInstance;
    public static T Instance
    {
        get { return mInstance; }
    }

	// Awake is called when the script instance is being loaded.
	void Awake() 
	{
        mInstance = GetComponent<T>();
        OnAwake();
	}
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    protected virtual void OnDestroy()
	{
        mInstance = null;
	}

    protected virtual void OnAwake() { }
}
