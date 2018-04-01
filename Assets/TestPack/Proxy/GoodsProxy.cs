using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GoodsProxy : BaseProxy
{
    public new const string NAME = "GoodsProxy";
    public List<GoodsData> modelList
    {
        get { return (List<GoodsData>)base.Data; }
    }

    public GoodsProxy() : base(NAME, new List<GoodsData>())
    {
        // 测试数据，应该从服务器接收数据
        // 测试数据，应该从服务器接收数据
        this.AddModelToList(new GoodsData(this.GetMaxId() + 1, "Goods0"));
        this.AddModelToList(new GoodsData(this.GetMaxId() + 1, "goods1"));
        this.AddModelToList(new GoodsData(this.GetMaxId() + 1, "goods2"));
    }

    public void AddModelToList(GoodsData model)
    {
        this.modelList.Add(model);
    }

    public int GetMaxId()
    {
        if (this.modelList.Count == 0)
        {
            return 0;
        }
        return this.modelList.Max(a => a.ID);
    }

    public GoodsData GetModelById(int id)
    {
        return modelList.FirstOrDefault(a => a.ID == id);
    }
}