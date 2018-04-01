using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 对proxy管理
public class Model
{
    private Dictionary<string, IProxy> m_proxyMap;

    public Model()
    {
        this.m_proxyMap = new Dictionary<string, IProxy>();
    }
    public virtual bool HasProxy(string Name)
    {
        return this.m_proxyMap.ContainsKey(Name);
    }

    public virtual void RegisterProxy(IProxy proxy)
    {
        if (!m_proxyMap.ContainsKey(proxy.Name))
        {
            this.m_proxyMap[proxy.Name] = proxy;
        }
        proxy.OnRegister();
    }

    public virtual IProxy UnResiterProxy(string proxyName)
    {
        IProxy proxy = null;
        if (this.m_proxyMap.ContainsKey(proxyName))
        {
            proxy = this.findProxy(proxyName);
            this.m_proxyMap.Remove(proxyName);
        }
        if (proxy != null)
        {
            proxy.OnRemove();
        }
        return proxy;
    }

    public virtual IProxy findProxy(string proxyName)
    {
        if (!this.m_proxyMap.ContainsKey(proxyName))
        {
            return null;
        }
        return this.m_proxyMap[proxyName];
    }
    
}

