using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Command 主要辅助 取Proxy里的数据 到 view 上显示
public class RenderToViewCommand : ICommand
{
    PackProxy packProxy = PackProxy.GetIntance();
    GoodsProxy goodProxy = GoodsProxy.GetIntance();

    public void Excute(INotifier inotifier)
    {
        List<PackModel> modelList = packProxy.GetModelList();

        for (int i = 0; i < modelList.Count; i++)
        {
            if (modelList[i].GoodId != 0)
            {
                modelList[i].good = goodProxy.GetModelById(modelList[i].GoodId);
            }
        }

        AppFacade.Intance.ExcuteMediator(new INotifier(NotifierConstant.StrPackViewShow, modelList));

        //TODO:view 与command 的解耦
        //view.ShowPack(modelList);
    }
}

