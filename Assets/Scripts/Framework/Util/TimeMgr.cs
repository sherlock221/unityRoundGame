// **********************************************************************
// 
// 文件名(File Name)：             TimeMgr.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/9/21 22:22:56
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeMgr : MonoBehaviour 
{
    private static TimeMgr mInstance;
    public static TimeMgr Instance
    {
        get
        {
            return mInstance;
        }
    }
    public delegate void Interval();
    private Dictionary<Interval, float> mDicinterval = new Dictionary<Interval, float>();

    public void AddInterval(Interval interval,float time)
    {
        if (null != interval)
        mDicinterval[interval] = Time.time + time;
    }

    public void RemoveInterval(Interval interval)
    {
         if (null != interval)
         {
             if (mDicinterval.ContainsKey(interval))
             {
                 mDicinterval.Remove(interval);
             }
         }
    }


    // Awake is called when the script instance is being loaded.
	void Awake() 
	{
        mInstance = this;
	}

    void Update()
    {
        if(mDicinterval.Count > 0)
        {
            List<Interval> remove = new List<Interval>();
            foreach(KeyValuePair<Interval,float> KeyValue in mDicinterval)
            {
                if (KeyValue.Value <= Time.time)
                {
                    remove.Add(KeyValue.Key);
                }
            }
            for (int i = 0; i < remove.Count;i++ )
            {
                remove[i]();
                mDicinterval.Remove(remove[i]);
            }
        }
        
    }
}
