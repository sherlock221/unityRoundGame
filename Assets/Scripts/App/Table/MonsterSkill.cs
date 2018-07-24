

/// <summary> Generate FromMonsterSkill </summary> 
public class MonsterSkill
{
	/// <summary> 编号 </summary>
 	public int id;

	/// <summary> 类型（1共用 2boss） </summary>
 	public int type;

	/// <summary> 名称 </summary>
 	public string name;

	/// <summary> 作用描述 </summary>
 	public string describe;

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

}
// End of Auto Generated Code
