using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PackMediator : Mediator
{
    public static PackMediator Intance;
    PackCompent packCompent;

    public PackMediator()
    {
        packCompent = PackCompent.Intance;
    }

    public override string Name
    {
        get { return "PackMediator"; }
    }

    public override List<string> MsgList
    {
        get
        {
            List<string> msgList = new List<string>();
            msgList.Add(NotifierConstant.StrPackViewShow);
            return msgList;
        }
    }

    public override void Execute(INotifier inofifier)
    {
        switch (inofifier.msg)
        {
            case NotifierConstant.StrPackViewShow:
                List<PackData> packModelList = (List<PackData>)inofifier.body;
                packCompent.ShowPack(packModelList);
                break;
            default:
                break;
        }
    }
}
