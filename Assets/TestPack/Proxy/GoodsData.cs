using UnityEngine;
using System.Collections;

public class GoodsData : DataBase
{
    public string Src { get; set; }

    public GoodsData(int id,string src)
    {
        this.ID = id;
        this.Src = src;
    }

    public GoodsData()
    {

    }
}
