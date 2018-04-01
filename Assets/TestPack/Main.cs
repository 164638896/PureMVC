using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // 可以在程序的任意地方注册

        // 注册 model
        Facade.Instance.RegisterProxy(new PackProxy());
        Facade.Instance.RegisterProxy(new GoodsProxy());

        // 注册 Mediator
        Facade.Instance.ResierMediator(new PackMediator());

        // 注册 Command       
        Facade.Instance.RestierCommand(NotifierConstant.StrRenderToViewCommand, new RenderToViewCommand());
        Facade.Instance.RestierCommand(NotifierConstant.StrAddGoodCommand, new AddGoodCommand());
    }

    public void Update()
    {
        // 测试代码, 可以在proxy，view，controller 模块里发消息
        // proxy：接收到服务器消息，时候发个消息出来
        // view：用户输入事件发出消息，比如按钮消息
        // controller：业务逻辑 更新界面发消息
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Facade.Instance.ExcuteCommand(new INotifier(NotifierConstant.StrRenderToViewCommand));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Facade.Instance.ExcuteCommand(new INotifier(NotifierConstant.StrAddGoodCommand, 1));
        }
    }
}
