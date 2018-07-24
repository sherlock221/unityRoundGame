using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Text;

public class ComUtil 
{
    #region 节点创建

    /// <summary>
    /// 创建或者获取组件，如果target对象已存在组件，直接返回，否则增加组件后返回组件
    /// </summary>
    /// <typeparam name="T">Component</typeparam>
    /// <param name="target"></param>
    /// <returns></returns>
    public static T CreateOrGetComponent<T>(GameObject target) where T : Component
    {
        if (null == target)
        {
            return null;
        }

        T comp = target.GetComponent<T>();
        if (null == comp)
        {
            comp = target.AddComponent<T>();
        }

        return comp;
    }

 

    /// <summary>
    /// 把transChild加到transParent里，并把child的Transform的localPosition,localRotation,localScale的值重置
    /// </summary>
    /// <param name="transChild"></param>
    /// <param name="transParent"></param>
    public static void AddGameObjectToZeroPos(Transform transChild, Transform transParent)
    {
        if (null == transChild || null == transParent)
        {
            return;
        }

        transChild.parent = transParent;
        transChild.localPosition = Vector3.zero;
        transChild.localRotation = Quaternion.identity;
        transChild.localScale = Vector3.one;
    }

    /// <summary>
    /// 把transChild加到transParent里，使得transChild为parentObj的孩子
    /// 会重新计算孩子的位置
    /// </summary>
    public static void AddGameObjectTo(Transform transChild, Transform transParent)
    {
        if (null == transChild || null == transParent || transChild.parent == transParent)
        {
            return;
        }

        transChild.parent = transParent;
        Transform trans = transParent;
        Vector3 scaleParam = new Vector3();
        while (null != trans)
        {
            scaleParam = trans.localScale;
            scaleParam.Scale(transChild.localPosition);
            transChild.localPosition = trans.localPosition + scaleParam;
            transChild.localRotation = trans.localRotation * transChild.localRotation;

            scaleParam = transChild.localScale;
            scaleParam.Scale(trans.localScale);
            transChild.localScale = scaleParam;

            trans = trans.parent;
        }
    }

#endregion

#region GetUTF8StringEX

    private static StringBuilder strBuilder = new StringBuilder(4048);

	/// <summary>
    /// 非线程安全
	/// </summary>
	/// <returns>
	/// The UT f8 string E.
	/// </returns>
	/// <param name='buffer'>
	/// Buffer.
	/// </param>
	public static string GetUTF8StringTrimZero(byte[] buffer)
	{
        if (null == buffer || 0 == buffer.Length)
        {
            return "";
        }
        strBuilder.Remove(0, strBuilder.Length);
        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] != 0)
            {
                strBuilder.Append((char)buffer[i]);
            }
            else
            {
                break;
            }
        }
        return strBuilder.ToString();
	}
	
#endregion

#region 文本处理

    /// <summary>
    /// replace all "\n" with '\n'
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string GetVerticalString(string val)
    {
        string ret = null;
        if (null != val)
        {
            string n = "" + '\n';
            ret = val.Replace("[/n]", n);
        }
        return ret;
    }

#endregion

    #region 节点查找

    public static bool IsSubClassOfRawGeneric(Type generic, Type toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
    }

    public static bool IsSubClassOf(Type baseType, Type toCheck)
    {
        return baseType.IsAssignableFrom(toCheck);
    }

    /// <summary>
    /// 用于从一个节点开始将其与其下所有T类型的并继承自Component的返回回来
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root"></param>
    /// <param name="enable"></param>
    /// <returns></returns>
    public static List<T> FindAllComponentsUnderRoot<T>(Transform root) where T : Component
    {
        List<T> retList = new List<T>();

        if (root.childCount != 0)
        {
            for (int i = 0; i < root.childCount; i++)
            {
                retList.AddRange(FindAllComponentsUnderRoot<T>(root.GetChild(i)));
            }
        }

        T t = root.GetComponent<T>();
        if (t != null)
        {
            retList.Add(t);
        }
        return retList;

    }

    #endregion

    #region 节点排序

    /// <summary>
    /// 排序root下tag不为tag的节点
    /// </summary>
    /// <param name="root"></param>
    /// <param name="tag"></param>
    public static List<Transform> SortRoutePointExcludeTag(GameObject root, string tag)
    {
		return SortRoutePoint(root, tag, true);
	}
	
	/// <summary>
	/// 排序root下tag为tag的节点
	/// </summary>
	/// <param name="root"></param>
	/// <param name="tag"></param>
	public static List<Transform> SortRoutePointWithTag(GameObject root, string tag) 
	{
		return SortRoutePoint(root, tag, false);
	}
	
	private static List<Transform> SortRoutePoint(GameObject root, string tag, bool isExclude)
	{
		ArrayList routePointNames = new ArrayList();
		for (int i = 0; i < root.transform.childCount; i++)
		{
			Transform trans = root.transform.GetChild(i);
			bool isAdd = isExclude ? trans.tag != tag : trans.tag == tag;
			if (isAdd)
			{
				routePointNames.Add(trans.name);
			}
		}
		routePointNames.Sort();
		
		List<Transform> ret = new List<Transform>();
		foreach (string na in routePointNames)
		{
			for (int i = 0; i < root.transform.childCount; i++)
			{
				Transform trans = root.transform.GetChild(i);
				if (trans.name == na)
				{
					ret.Add(trans);
					break;
				}
			}
		}
		return ret;
	}
	
	#endregion

    #region 通过指定路径获取组件
    /// <summary>通过指定路径获取组件</summary>
    public static T GetComponentByLocalPath<T>(GameObject go, string localPath) where T : Component
    {
        GameObject obj = null;
        if (go == null)
        {
            obj = GameObject.Find(localPath) as GameObject;
        }
        else if (go.transform.childCount > 0)
        {
            Transform tran = go.transform.Find(localPath);
            if (tran != null)
            {
                obj = tran.gameObject;
            }
        }
        if (obj == null)
        {
            Debug.LogWarning(string.Format("GetComponentByLocalPath Null={0}", localPath));
            return null;
        }
        else
        {
            return obj.GetComponent<T>();
        }
    }
    #endregion

    #region 通过指定路径获取游戏物体
    /// <summary>通过指定路径获取组件</summary>
    public static GameObject FindGameObjectByLocalPath(GameObject go, string localPath)
    {
        GameObject obj = null;
        if (go == null)
        {
            obj = GameObject.Find(localPath) as GameObject;
        }
        else if (go.transform.childCount > 0)
        {
            Transform tran = go.transform.Find(localPath);
            if (tran != null)
            {
                obj = tran.gameObject;
            }
        }
        if (obj == null)
        {
            Debug.LogWarning(string.Format("GetComponentByLocalPath Null={0}", localPath));
            return null;
        }
        else
        {
            return obj;
        }
    }
    #endregion

    #region 随机数
    /// <summary>
    /// 返回一个0 - 1之间的随机数,参数是种子
    /// </summary>
    /// <returns></returns>
    public static float GetRandIn0_1(int seed = 3)
    {
        //UnityEngine.Random.seed = seed;
       return UnityEngine.Random.value;
    }
    #endregion

    #region 根据名字获取该游戏对象下子物体

    /// <summary>
    ///  根据名字获取该游戏对象下子物体
    /// </summary>
    /// <param name="tan">主要对象</param>
    /// <param name="name">当前的名字</param>
    /// <returns></returns>
    public static Transform FindTransformInChild(Transform tan, string name, bool oneLevel = false)
    {
        List<Transform> ret = FindTransformInChild(tan, new List<string>(){ name });
        if (ret.Count > 0)
        {
            return ret[0];
        }
        return null;
    }

    /// <summary>
    /// 根据名字获取该游戏对象下子物体
    /// </summary>
    /// <param name="tan"></param>
    /// <param name="names"></param>
    /// <returns></returns>
    public static List<Transform> FindTransformInChild(Transform tan, List<string> names, bool oneLevel = false)
    {
        List<Transform> ret = new List<Transform>();

        if (tan == null)
        {
            Log.Error("ComUtil.FindTransformInChild -> tan Is Null");
            return ret;
        }

        if (tan.childCount != 0)
        {
            for (int i = 0; i < tan.childCount; i++)
            {
                if (oneLevel)
                {
                    if (names.Contains(tan.GetChild(i).name))
                    {
                        ret.Add(tan);
                    }
                }
                else
                {
                    ret.AddRange(FindTransformInChild(tan.GetChild(i), names));
                }
            }
        }

        if (names.Contains(tan.name))
        {
            ret.Add(tan);
        }

        return ret;
    }

    #endregion

    #region 根据名字获取该游戏对象下子物体的组件
    /// <summary>
    ///  根据名字获取该游戏对象下子物体
    /// </summary>
    /// <param name="tan">主要对象</param>
    /// <param name="name">当前的名字</param>
    /// <returns></returns>
    public static T FindComponentInChild<T>(Transform tan, string name) where T : Component
    {
        List<Transform> ret = FindTransformInChild(tan, new List<string>() { name });
        if (ret.Count > 0)
        {
            return ret[0].GetComponent<T>();
        }
        return null;
    }
    #endregion

    /// <summary>
    /// 获取数字位数
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static int GetNumberLen(int number)
    {
        int tmp = number;
        int ret = 0;
        while (tmp > 0)
        {
            ret += 1;
            tmp /= 10;
        }
        return ret;
    }

    /// <summary>
    /// 寻找自物体下的游戏物体
    /// </summary>
    /// <param name="listName">要寻找的物体列表</param>
    /// <param name="tran">父对象</param>
    /// <param name="trans">返回的结果</param>
    public static void GetTransformInChild(List<string> listName, Transform tran, ref List<Transform> trans)
    {
        if (listName.Contains(tran.name))
        {
            trans.Add(tran);
        }
        if (trans.Count == listName.Count)
        {
            return;
        }
        for (int i = 0; i < tran.childCount; i++)
        {
            GetTransformInChild(listName, tran.GetChild(i), ref trans);
        }
    }
}
