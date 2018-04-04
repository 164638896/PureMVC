using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

//http://puremvc.org/
//Facade :  只发送不接受Notification （管理mvc）
//Proxy :   只发送不接受Notification (管理数据，接收服务器的消息)
//Command : 可以发送和接受Notification（逻辑）
//Mediator :可以发送和接受Notification（显示，比如ui）

public class AppFacade : Facade
{
    public AppFacade(GameObject canvas)
    {
        RegisterCommand(NotificationConstant.LevelUp, typeof(TestCommand));
        RegisterMediator(new TestMediator(canvas));
        RegisterProxy(new TestProxy());
    }
}