using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

// 收到 需要 逻辑处理，这里是从ui层发过来的消息
// command里会RetrieveProxy 和 尽量不RetrieveMediator 发消息过去
public class TestCommand : SimpleCommand
{
    public new const string NAME = "TestCommand";

    public override void Execute(PureMVC.Interfaces.INotification notification)
    {
        TestProxy proxy = (TestProxy)Facade.RetrieveProxy(TestProxy.NAME);
        proxy.ChangeLevel(1);
    }
}