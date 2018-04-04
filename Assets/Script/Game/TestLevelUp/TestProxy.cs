using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

// 接收服务器的消息，并存储数据，然后发出通知
public class TestProxy : Proxy
{
    public new const string NAME = "TestProxy";
    public CharacterInfo Data { get; set; }

    public TestProxy() : base(NAME)
    {
        Data = new CharacterInfo();
    }

    public void ChangeLevel(int change)
    {
        Data.Level += change;
        SendNotification(NotificationConstant.LevelChange, Data);
    }

}
