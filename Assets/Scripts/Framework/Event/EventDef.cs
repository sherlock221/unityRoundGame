using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EventDef
{
    #region 系统消息 1-1000
    
    public const int ResLoadFinish = 1;

	#endregion

   
	#region 界面相关消息 2000 - 4000

	/// <summary>
	/// 配置表加载完成
	/// </summary>
	public const int TableDataFinish = 2000;

    #endregion

    #region 战斗相关事件 5000 - 6000

    /// <summary>
    /// 状态结束消息
    /// </summary>
    public const int StateEndEvent = 5000;

    /// <summary>
    /// 所有角色创建完成
    /// </summary>
    public const int CreateRoleFinsh = StateEndEvent + 1;

    /// <summary>
    /// 攻击了怪物
    /// </summary>
    public const int AttackMonster = CreateRoleFinsh + 1;
  
    #endregion


}
