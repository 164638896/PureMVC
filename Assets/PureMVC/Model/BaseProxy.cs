using System.Collections;
using System.Collections.Generic;
using System.Linq;

// proxy 还得支持 收发消息

public interface IProxy
{
    //注册
    void OnRegister();
    //注销
    void OnRemove();

    //原始数据
    object Data { get; set; }
    //代理名称
    string Name { get; }
}

public class BaseProxy : IProxy
{ 
    protected object m_data;
    protected string m_proxyName;
    public static string NAME = "Proxy";

    public BaseProxy() : this(NAME, null)
    {
    }

    public BaseProxy(string proxyName) : this(proxyName, null)
    {
    }

    public BaseProxy(string proxyName, object data)
    {
        this.m_proxyName = (proxyName != null) ? proxyName : NAME;
        if (data != null)
        {
            this.m_data = data;
        }
    }

    public virtual void OnRegister()
    {
    }

    public virtual void OnRemove()
    {
    }

    public object Data
    {
        get
        {
            return this.m_data;
        }
        set
        {
            this.m_data = value;
        }
    }

    public string Name
    {
        get
        {
            return this.m_proxyName;
        }
    }
}