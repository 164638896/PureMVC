
using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Core
{
    public class Controller : IController
	{
		protected Controller()
		{
			m_commandMap = new Dictionary<string, Type>();	
			InitializeController();
		}

        //这个函数执行的顺序是： facade->view->Observer->ExecuteCommand
        public virtual void ExecuteCommand(INotification note)
		{
			Type commandType = null;

			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(note.Name)) return;
				commandType = m_commandMap[note.Name];
			}

			object commandInstance = Activator.CreateInstance(commandType);

			if (commandInstance is ICommand)
			{
				((ICommand) commandInstance).Execute(note);
			}
		}

        // 注册到view->Observer
        public virtual void RegisterCommand(string notificationName, Type commandType)
		{
			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(notificationName))
				{
					// This call needs to be monitored carefully. Have to make sure that RegisterObserver
					// doesn't call back into the controller, or a dead lock could happen.
					m_view.RegisterObserver(notificationName, new Observer("executeCommand", this));
				}

				m_commandMap[notificationName] = commandType;
			}
		}

		public virtual bool HasCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				return m_commandMap.ContainsKey(notificationName);
			}
		}

		public virtual void RemoveCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				if (m_commandMap.ContainsKey(notificationName))
				{
					// remove the observer

					// This call needs to be monitored carefully. Have to make sure that RemoveObserver
					// doesn't call back into the controller, or a dead lock could happen.
					m_view.RemoveObserver(notificationName, this);
					m_commandMap.Remove(notificationName);
				}
			}
		}

		public static IController Instance
		{
			get
			{
				if (m_instance == null)
				{
					lock (m_staticSyncRoot)
					{
						if (m_instance == null) m_instance = new Controller();
					}
				}

				return m_instance;
			}
		}

		static Controller()
		{
		}

		protected virtual void InitializeController()
		{
			m_view = View.Instance;
		}

		protected IView m_view;
        protected IDictionary<string, Type> m_commandMap;
		protected static volatile IController m_instance;
		protected readonly object m_syncRoot = new object();
		protected static readonly object m_staticSyncRoot = new object();

	}
}
