using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PackProxy : BaseProxy
{
    public new const string NAME = "PackProxy";
    public List<PackData> modelList
    {
        get { return (List<PackData>)base.Data; }
    }

    public PackProxy() : base(NAME, new List<PackData>())
    {
        // 测试数据，应该从服务器接收数据
        for (int i = 0; i < 15; i++)
        {
            this.AddModelToList(new PackData(i));
        }
    }

    public void AddModelToList(PackData order)
    {
        modelList.Add(order);
    }

    public void RemoveOrder(PackData order)
    {
        modelList.Remove(order);
    }

    public PackData GetEmptyModel()
    {
        return this.modelList.FirstOrDefault(a => a.GoodId == 0);
    }

    internal bool IsFull()
    {
        int count = this.modelList.Count(a => a.GoodId == 0);
        if (count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal bool TryGetGoodModel(int id, out PackData model)
    {
        model = null;
        model = this.modelList.FirstOrDefault(a => a.GoodId == id);
        if (model == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Update(PackData model)
    {
        PackData tmpModel = this.GetModelById(model.ID);
        tmpModel = model;
    }

    public PackData GetModelById(int id)
    {
        return modelList.FirstOrDefault(a => a.ID == id);
    }

    public List<PackData> GetModelList()
    {
        return this.modelList;
    }

}