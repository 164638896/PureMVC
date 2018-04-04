
using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Core
{
    public class View : IView
    {
		protected View()
		{
			m_mediatorMap = new Dictionary<string, IMediator>();
			m_observerMap = new Dictionary<string, IList<IObserver>>();
            InitializeView();
		}

		public virtual void RegisterObserver(string notificationName, IObserver observer)
		{
			lock (m_syncRoot)
			{
				if (!m_observerMap.ContainsKey(notificationName))
				{
					m_observerMap[notificationName] = new List<IObserver>();
				}

				m_observerMap[notificationName].Add(observer);
			}
		}

		public virtual void NotifyObservers(INotification notification)
		{
			IList<IObserver> observers = null;

			lock (m_syncRoot)
			{
				if (m_observerMap.ContainsKey(notification.Name))
				{
					// Get a reference to the observers list for this notification name
					IList<IObserver> observers_ref = m_observerMap[notification.Name];
					// Copy observers from reference array to working array, 
					// since the reference array may change during the notification loop
					observers = new List<IObserver>(observers_ref);
				}
			}

			// Notify outside of the lock
			if (observers != null)
			{
				// Notify Observers from the working array				
				for (int i = 0; i < observers.Count; i++)
				{
					IObserver observer = observers[i];
					observer.NotifyObserver(notification);
				}
			}
		}

		public virtual void RemoveObserver(string notificationName, object notifyContext)
		{
			lock (m_syncRoot)
			{
				// the observer list for the notification under inspection
				if (m_observerMap.ContainsKey(notificationName))
				{
					IList<IObserver> observers = m_observerMap[notificationName];

					// find the observer for the notifyContext
					for (int i = 0; i < observers.Count; i++)
					{
						if (observers[i].CompareNotifyContext(notifyContext))
						{
							// there can only be one Observer for a given notifyContext 
							// in any given Observer list, so remove it and break
							observers.RemoveAt(i);
							break;
						}
					}

					// Also, when a Notification's Observer list length falls to 
					// zero, delete the notification key from the observer map
					if (observers.Count == 0)
					{
						m_observerMap.Remove(notificationName);
					}
				}
			}
		}

		public virtual void RegisterMediator(IMediator mediator)
		{
			lock (m_syncRoot)
			{
				// do not allow re-registration (you must to removeMediator fist)
				if (m_mediatorMap.ContainsKey(mediator.MediatorName)) return;

				// Register the Mediator for retrieval by name
				m_mediatorMap[mediator.MediatorName] = mediator;

				// Get Notification interests, if any.
				IList<string> interests = mediator.ListNotificationInterests();

				// Register Mediator as an observer for each of its notification interests
				if (interests.Count > 0)
				{
					// Create Observer
					IObserver observer = new Observer("handleNotification", mediator);

					// Register Mediator as Observer for its list of Notification interests
					for (int i = 0; i < interests.Count; i++)
					{
						RegisterObserver(interests[i].ToString(), observer);
					}
				}
			}

			// alert the mediator that it has been registered
			mediator.OnRegister();
		}

		public virtual IMediator RetrieveMediator(string mediatorName)
		{
			lock (m_syncRoot)
			{
				if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
				return m_mediatorMap[mediatorName];
			}
		}

		public virtual IMediator RemoveMediator(string mediatorName)
		{
			IMediator mediator = null;

			lock (m_syncRoot)
			{
				// Retrieve the named mediator
				if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
				mediator = (IMediator) m_mediatorMap[mediatorName];

				// for every notification this mediator is interested in...
				IList<string> interests = mediator.ListNotificationInterests();

				for (int i = 0; i < interests.Count; i++)
				{
					// remove the observer linking the mediator 
					// to the notification interest
					RemoveObserver(interests[i], mediator);
				}

				// remove the mediator from the map		
				m_mediatorMap.Remove(mediatorName);
			}

			// alert the mediator that it has been removed
			if (mediator != null) mediator.OnRemove();
			return mediator;
		}

		public virtual bool HasMediator(string mediatorName)
		{
			lock (m_syncRoot)
			{
				return m_mediatorMap.ContainsKey(mediatorName);
			}
		}
  
		public static IView Instance
		{
			get
			{
				if (m_instance == null)
				{
					lock (m_staticSyncRoot)
					{
						if (m_instance == null) m_instance = new View();
					}
				}

				return m_instance;
			}
		}

        static View()
        {
		}

        protected virtual void InitializeView()
		{
		}

		protected IDictionary<string, IMediator> m_mediatorMap;
		protected IDictionary<string, IList<IObserver>> m_observerMap;
		protected static volatile IView m_instance;
		protected readonly object m_syncRoot = new object();
		protected static readonly object m_staticSyncRoot = new object();
	
	}
}
