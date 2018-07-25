using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData{

    private static PlayerData mInstance;

    public static PlayerData Instance
    {
        get {
            if(mInstance == null)
                mInstance = new PlayerData();

            return mInstance;
        }
    }



    private const string LevelKEY = "YOUKELevelKEY";

    /// <summary>
    /// 人物等级
    /// </summary>
    public int Level
    {
        set
        {
            PlayerPrefs.SetInt(LevelKEY, value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt(LevelKEY, 1);
        }
    }

    private const string AttackKEY = "YOUKEAttackKEY";

    /// <summary>
    /// 人物攻击力
    /// </summary>
    public int Attack
    {
        set
        {
            PlayerPrefs.SetInt(AttackKEY, value);
            PlayerPrefs.Save();

        }
        get
        {
            return PlayerPrefs.GetInt(AttackKEY, 6);
        }
    }

    private const string DEFKEY = "YOUKEDEFKEY";

    /// <summary>
    /// 人物防御力
    /// </summary>
    public int DEF
    {
        set
        {
            PlayerPrefs.SetInt(DEFKEY, value);
            PlayerPrefs.Save();

        }
        get
        {
            return PlayerPrefs.GetInt(DEFKEY, 2);
        }
    }

    private const string HPKEY = "YOUKEHPKEY";

    /// <summary>
    /// 人物生命值
    /// </summary>
    public int HP
    {
        set
        {
            PlayerPrefs.SetInt(HPKEY, value);
            PlayerPrefs.Save();

        }
        get
        {
            return PlayerPrefs.GetInt(HPKEY, 50);
        }
    }


    private const string MPKEY = "YOUKEMPKEY";

    /// <summary>
    /// 人物魔法值
    /// </summary>
    public int MP
    {
        set
        {
            PlayerPrefs.SetInt(MPKEY, value);
            PlayerPrefs.Save();

        }
        get
        {
            return PlayerPrefs.GetInt(MPKEY, 30);
        }
    }

    private const string EXPKEY = "YOUKEEXPKEY";

    /// <summary>
    /// 人物经验值
    /// </summary>
    public int EXP
    {
        set
        {
            PlayerPrefs.SetInt(EXPKEY, value);
            PlayerPrefs.Save();

        }
        get
        {
            return PlayerPrefs.GetInt(EXPKEY, 0);
        }
    }

    private const string GoldKEY = "YOUKEGoldKEY";

    /// <summary>
    /// 人物金币
    /// </summary>
    public int Gold
    {
        set
        {
            PlayerPrefs.SetInt(GoldKEY, value);
            PlayerPrefs.Save();

        }
        get
        {
            return PlayerPrefs.GetInt(GoldKEY, 0);
        }
    }
}
