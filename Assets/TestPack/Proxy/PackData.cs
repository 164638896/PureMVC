using UnityEngine;
using System.Collections;

public class PackData : DataBase
{
    public int Count { get; set; }
    private int goodId;
    public GoodsData good;

    public int GoodId
    {
        get
        {
            return this.goodId;

        }
        set { this.goodId = value; this.Count = 1; }
    }

    public PackData(int id, int goodId, int count)
        : base(id)
    {
        this.Count = 0;
        this.GoodId = goodId;
        this.Count = count;
    }

    public PackData(int id)
        : base(id)
    {

    }

    public PackData()
    {
        Count = 0;
    }
}
