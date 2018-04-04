
using System;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Patterns
{
    public class Notifier : INotifier
    {
		public virtual void SendNotification(string notificationName) 
		{
			// The Facade SendNotification is thread safe, therefore this method is thread safe.
			m_facade.SendNotification(notificationName);
		}
		public virtual void SendNotification(string notificationName, object body)
		{
			// The Facade SendNotification is thread safe, therefore this method is thread safe.
			m_facade.SendNotification(notificationName, body);
		}

		public virtual void SendNotification(string notificationName, object body, string type)
		{
			// The Facade SendNotification is thread safe, therefore this method is thread safe.
            m_facade.SendNotification(notificationName, body, type);
		}

		protected IFacade Facade
		{
			get { return m_facade; }
		}

		private IFacade m_facade = PureMVC.Patterns.Facade.Instance;
	}
}
