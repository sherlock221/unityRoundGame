using UnityEngine;
using System.Collections;

public class UIDef
{
    /// <summary>
    /// 主界面UI名称
    /// </summary>
    public const string MainUI = "CanvasMain";

    /// <summary>
    /// 设置界面
    /// </summary>
    public const string SettingsUI = "CanvasSetting";
    /// <summary>人物管理界面</summary>
    public const string PlayerMenuUI = "CanvasPlayerMenu";
    /// <summary>人物UI</summary>
    public const string PlayerUI = "PlayerUI";
    /// <summary>技能UI</summary>
    public const string SkillUI = "SkillUI";
    /// <summary>物品UI</summary>
    public const string GoodsUI = "GoodsUI";
    /// <summary> 技能信息UI </summary>
    public const string SkillInfoUI = "CanvasSkillInfo";
    /// <summary> 任务UI </summary>
    public const string TaskUI = "TaskUI";
    /// <summary>装备UI</summary>
    public const string EquipmentUI = "CanvasEquipmentInfo";
    /// <summary>物品信息UI</summary>
    public const string GoodsInfoUI = "CanvasGoodsInfo";

    /// <summary>
    /// 战斗的UI
    /// </summary>
    public const string BattleUI = "CanvasBattle";

    public static int GetUIOrderLayer(string uianme)
    {
        switch (uianme)
        {
            case MainUI:
                return 0;
            case SettingsUI:
                return 100;
            case PlayerMenuUI:
                return 100;
            case SkillInfoUI:
                return 105;
            case TaskUI:
                return 100;
            case EquipmentUI:
                return 105;
            case GoodsInfoUI:
                return 105;
            case BattleUI:
                return 0;
        }
        return 0;
    }


    /// <summary>
    /// 技能图标
    /// </summary>
    public const string SkillAtlasPrefab = "Skill_Atlas";

    /// <summary>
    /// 物品图标图集
    /// </summary>
    public const string GoodsAtlasPrefab = "Goods_Atlas";
    /// <summary>
    /// 装备图标图集
    /// </summary>
    public const string EquipmentAtlasPrefab = "Equipment_Atlas";
}
