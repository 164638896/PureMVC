using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// Command 主要辅助 取Proxy里的数据 到 view 上显示
public class AddGoodCommand : ICommand
{
    PackProxy packProxy = Facade.Instance.findProxy(PackProxy.NAME) as PackProxy;
    GoodsProxy goodProxy = Facade.Instance.findProxy(GoodsProxy.NAME) as GoodsProxy;
    
    public void Excute(INotifier inotifier)
    {
        PackData model = null;

        //1.判断物体是不是存在
        int id = 1;

        if (!int.TryParse(inotifier.body.ToString(), out id))
        {
            return;
        }

        if (packProxy.TryGetGoodModel(id, out model))
        {
            model.Count++;
            packProxy.Update(model);

        }
        else if (packProxy.IsFull())   //2.判段背包是不是已经满了
        {
            return;
        }
        else //3.添加
        {
            model = packProxy.GetEmptyModel();
            model.GoodId = id;
        }

        Facade.Instance.ExcuteCommand(new INotifier(NotifierConstant.StrRenderToViewCommand));
    }
}

