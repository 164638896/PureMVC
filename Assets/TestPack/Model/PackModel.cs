using UnityEngine;
using System.Collections;

public class PackModel : ModelBase
{
    public int Count { get; set; }
    private int goodId;
    public GoodsModel good;

    public int GoodId
    {
        get
        {
            return this.goodId;

        }
        set { this.goodId = value; this.Count = 1; }
    }

    public PackModel(int id, int goodId, int count)
        : base(id)
    {
        this.Count = 0;
        this.GoodId = goodId;
        this.Count = count;
    }

    public PackModel(int id)
        : base(id)
    {

    }

    public PackModel()
    {
        Count = 0;
    }
}
