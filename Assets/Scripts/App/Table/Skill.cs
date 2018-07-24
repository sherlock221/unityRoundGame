

/// <summary> Generate FromSkill </summary> 
public class Skill
{
	/// <summary> 编号 </summary>
 	public int id;

	/// <summary> 名称 </summary>
 	public string name;

	/// <summary> 类别（1曜日 2生命 3天地被动 4究极） </summary>
 	public int type;

	/// <summary> 技能所在位置 </summary>
 	public string pos;

	/// <summary> 作用描述 </summary>
 	public string describe;

	/// <summary> 魔法消耗 </summary>
 	public int manaCost;

	/// <summary> 前置技能 </summary>
 	public string preSkill;

	/// <summary> 要求等级 </summary>
 	public int unLockLevel;

	/// <summary> 群体伤害还是单体伤害（1单体对敌2群体对敌3对自己4对自己且别人） </summary>
 	public int zielscheibe;

	/// <summary> 附加Buff </summary>
 	public int buff;

	/// <summary> 持续回合 </summary>
 	public int time;

	/// <summary> 回血比例 </summary>
 	public double bloodReturn;

	/// <summary> 伤害 </summary>
 	public double attack;

	/// <summary> 图片id </summary>
 	public string imageId;

}
// End of Auto Generated Code
