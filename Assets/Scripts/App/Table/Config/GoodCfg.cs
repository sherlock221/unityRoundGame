using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class GoodCfg : TableDataMgr.TableDataBase
{

    private List<Goods> _goods = new List<Goods>();


    public GoodCfg(string tableName)
        : base(tableName)
    {



    }

    protected override void ExtractJSON(string json)
    {
        _goods = JsonMapper.ToObject<List<Goods>>(json);


    }




}
