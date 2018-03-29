using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 对Mediator管理
public class View
{
    private Dictionary<string, Mediator> nameToMediator;
    private Dictionary<string, Mediator> msgToMediator;

    public View()
    {
        nameToMediator = new Dictionary<string, Mediator>();
        msgToMediator = new Dictionary<string, Mediator>();
    }

    public void ResiterMediator(Mediator mediator)
    {
        if (!nameToMediator.ContainsKey(mediator.Name))
        {
            nameToMediator.Add(mediator.Name, mediator);
            foreach (var msg in mediator.MsgList)
            {
                this.msgToMediator.Add(msg, mediator);
            }
        }
    }

    public void UnResiterMediator(Mediator mediator)
    {
        if (nameToMediator.ContainsKey(mediator.Name))
        {
            nameToMediator.Remove(mediator.Name);

            foreach (var msg in mediator.MsgList)
            {
                this.msgToMediator.Remove(msg);
            }
        }
    }

    public void Excute(INotifier notifier)
    {
        if (msgToMediator.ContainsKey(notifier.msg))
        {
            Mediator mediator = msgToMediator[notifier.msg];
            mediator.Execute(notifier);
        }
    }
}
