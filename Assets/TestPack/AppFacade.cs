using UnityEngine;
using System.Collections;

public class AppFacade : MonoBehaviour
{
    public static AppFacade Intance;
    Controller controller;
    View view;

    public void Awake()
    {
        Intance = this;
        controller = new Controller();
        view = new View();
    }

    // Use this for initialization
    void Start()
    {
        // 可以在程序的任意地方注册
        // 注册 Command       
        this.RestierCommand(NotifierConstant.StrRenderToViewCommand, new RenderToViewCommand());
        this.RestierCommand(NotifierConstant.StrAddGoodCommand, new AddGoodCommand());

        // 注册 Mediator
        this.ResierMediator(new PackMediator());
    }

    public void Update()
    {
        // 测试代码, 可以在proxy，view，controller 模块里发消息
        // proxy：接收到服务器消息，时候发个消息出来
        // view：用户输入事件发出消息，比如按钮消息
        // controller：业务逻辑 更新界面发消息
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.ExcuteCommand(new INotifier(NotifierConstant.StrRenderToViewCommand));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.ExcuteCommand(new INotifier(NotifierConstant.StrAddGoodCommand, 1));
        }
    }

    public void ResierMediator(Mediator mediator)
    {
        if (mediator != null)
        {
            view.ResiterMediator(mediator);
        }
    }

    public void UnResierMediator(Mediator mediator)
    {
        if (mediator != null)
        {
            view.UnResiterMediator(mediator);
        }
    }

    public void ExcuteMediator(INotifier notifier)
    {
        if (notifier != null)
        {
            view.Excute(notifier);
        }
    }

    public void RestierCommand(string msg, ICommand command)
    {
        this.controller.ResiterCommand(msg, command);
    }

    public void UnRestierCommand(string msg)
    {
        this.controller.UnResiterCommand(msg);
    }

    public void ExcuteCommand(INotifier inotifier)
    {
        this.controller.ExcuteCommand(inotifier);
    }
}
