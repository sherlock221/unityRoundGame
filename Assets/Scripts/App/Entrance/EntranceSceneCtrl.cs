using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 场景管理的
/// </summary>
public class EntranceSceneCtrl : EventNode,IEventListener,UIMgr.ILoadUIListener {


	private static EntranceSceneCtrl _entranceSceneCtrl;

	public static EntranceSceneCtrl Instance{
		get{
			return _entranceSceneCtrl;
		}
	}


	private void Awake()
	{
		_entranceSceneCtrl = this;

		AttachEventListener(EventDef.TableDataFinish, this);
	}

	private void OnDestroy()
    {
        DetachEventListener(EventDef.TableDataFinish, this);
    }


	public int EventPriority()
    {
        return 0;
    }

    public bool HandleEvent(int id, object param1, object param2)
    {

        switch (id)
        {
            case EventDef.TableDataFinish:
				UIMgr.Instance.ShowUI(UIDef.MainUI, typeof(MainScene),this);
				Log.Debug("开始游戏..");
                break;

            default:
                break;
        }


        return false;
    }

	public void FiniSh(BaseUI ui)
	{
		
	}

	public void Failure()
	{
		
	}
}
