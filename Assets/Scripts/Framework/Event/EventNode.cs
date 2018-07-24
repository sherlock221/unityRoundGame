// **********************************************************************
// 
// 文件名(File Name)：             EventNode.cs
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/8/24 22:49:39
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventNode : MonoBehaviour
{
    /// <summary>
    /// 节点优先级
    /// </summary>
    public int EventNodePriority { set; get; }

    /// <summary>
    /// 所有消息集合
    /// </summary>
    private Dictionary<int, List<IEventListener>> mListeners = new Dictionary<int, List<IEventListener>>();

    /// <summary>
    /// 消息节点
    /// </summary>
    private List<EventNode> mNodeList = new List<EventNode>();

    /// <summary>
    /// 挂载一个消息节点到当前节点上
    /// </summary>
    /// <param name="node">消息节点</param>
    /// <returns>如果当前节点里面已经包含要添加的这个节点那么返回false</returns>
    public bool AttachEventNode(EventNode node)
    {
        if (node == null)
        {
            return false;
        }

        if (mNodeList.Contains(node))
        {
            return false;
        }
        int pos = 0;
        for (int i = 0; i < mNodeList.Count;i++ )
        {
            if (node.EventNodePriority > mNodeList[i].EventNodePriority)
            {
                break;
            }
            pos++;
        }

        mNodeList.Insert(pos,node);
        return true;
    }

    /// <summary>
    /// 卸载一个消息节点
    /// </summary>
    /// <param name="node">消息节点</param>
    /// <returns>如果节点不存在那么返回false</returns>
    public bool DetachEventNode(EventNode node)
    {
        if (!mNodeList.Contains(node))
        {
            return false;
        }
        mNodeList.Remove(node);
        return true;
    }

    /// <summary>
    /// 挂载一个消息监听器到当前的消息节点
    /// </summary>
    /// <param name="key">消息ID</param>
    /// <param name="listener">消息监听器</param>
    /// <returns>当前消息节点已经挂载了这个消息监听器那么返回false</returns>
    public bool AttachEventListener(int key,IEventListener listener)
    {
        if (listener == null)
        {
            return false;
        }
        if (!mListeners.ContainsKey(key))
        {
            mListeners.Add(key,new List<IEventListener>() { listener });
            return true;
        }
        if (mListeners[key].Contains(listener))
        {
            return false;
        }
        int pos = 0;
        for (int i = 0;i< mListeners[key].Count;i++ )
        {
            if (listener.EventPriority() > mListeners[key][i].EventPriority())
            {
                break;
            }
            pos++;
        }
        mListeners[key].Insert(pos,listener);
        return true;
    }

    /// <summary>
    /// 卸载一个消息节点
    /// </summary>
    /// <returns>如果当前消息节点不存在那么返回false</returns>
    public bool DetachEventListener(int key,IEventListener listener)
    {
       if (mListeners.ContainsKey(key) && mListeners[key].Contains(listener))
       {
           mListeners[key].Remove(listener);
           return true;
       }
       return false;
    }

    public void SendEvent(int key,object param1 = null,object param2 = null)
    {
        DispatchEvent(key, param1, param2);
    }

    /// <summary>
    /// 派发消息到子消息节点以及自己节点下的监听器上
    /// </summary>
    /// <param name="key">消息ID</param>
    /// <param name="param1"></param>
    /// <param name="param2"></param>
    /// <returns>如果中断消息返回true</returns>
    private bool DispatchEvent(int key,object param1,object param2)
    {
        for (int i = 0; i < mNodeList.Count;i++ )
        {
            if (mNodeList[i].DispatchEvent(key, param1, param2)) return true;
        }
        return TriggerEvent(key, param1, param2);
    }

  
    /// <summary>
    /// 消息触发
    /// </summary>
    /// <param name="key">消息id</param>
    /// <param name="param1"></param>
    /// <param name="param2"></param>
    /// <returns>是否中断</returns>
    private bool TriggerEvent(int key,object param1,object param2)
    {
        if (!this.gameObject.activeSelf || !this.gameObject.activeInHierarchy || !this.enabled)
        {
            return false;
        }

        if (!mListeners.ContainsKey(key))
        {
            return false;
        }
        List<IEventListener> listeners = mListeners[key];
        for (int i = 0; i < listeners.Count; i++)
        {
            if (listeners[i].HandleEvent(key, param1, param2)) return true;
        }
        return false;
    }

   void OnApplicationQuit()
    {
        mListeners.Clear();
        mNodeList.Clear();
    }

}
