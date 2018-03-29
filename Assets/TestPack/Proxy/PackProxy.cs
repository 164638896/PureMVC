using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PackProxy : BaseProxy<PackModel>
{
    private static PackProxy packProxy;

    public PackProxy() : base()
    {
        // 测试数据，应该从服务器接收数据
        for (int i = 0; i < 15; i++)
        {
            this.AddModelToList(new PackModel(i));
        }
    }
    
    internal static PackProxy GetIntance()
    {
        if (packProxy == null)
        {
            packProxy = new PackProxy();
        }
        return packProxy;
    }

    public PackModel GetEmptyModel()
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

    internal bool TryGetGoodModel(int id, out PackModel model)
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
}
