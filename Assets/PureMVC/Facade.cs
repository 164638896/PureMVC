using System.Collections;

public class Facade
{
    protected static volatile Facade m_instance;
    protected Controller controller;
    protected View view;
    protected Model model;

    public Facade()
    {
        controller = new Controller();
        view = new View();
        model = new Model();
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

    public void RegisterProxy(IProxy proxy)
    {
        this.model.RegisterProxy(proxy);
    }

    public void UnRegisterProxy(string proxyName)
    {
        this.model.UnResiterProxy(proxyName);
    }

    public IProxy findProxy(string proxyName)
    {
        return this.model.findProxy(proxyName);
    }

    public static Facade Instance
    {
        get
        {
            if (m_instance == null)
            {
                if (m_instance == null)
                {
                    m_instance = new Facade();
                }
            }
            return m_instance;
        }
    }
}
