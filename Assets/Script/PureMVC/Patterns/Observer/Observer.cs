﻿
using System;
using System.Reflection;

using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
	public class Observer : IObserver
	{
		public Observer(string notifyMethod, object notifyContext)
		{
			m_notifyMethod = notifyMethod;
			m_notifyContext = notifyContext;
		}

		public virtual void NotifyObserver(INotification notification)
		{
			object context;
			string method;

			// Retrieve the current state of the object, then notify outside of our thread safe block
			lock (m_syncRoot)
			{
				context = NotifyContext;
				method = NotifyMethod;
			}

			Type t = context.GetType();
			BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
			MethodInfo mi = t.GetMethod(method, f);
			mi.Invoke(context, new object[] { notification });
		}

		public virtual bool CompareNotifyContext(object obj)
		{
			lock (m_syncRoot)
			{
				// Compare on the current state
				return NotifyContext.Equals(obj);
			}
		}

		public virtual string NotifyMethod
		{
			private get
			{
				// Setting and getting of reference types is atomic, no need to lock here
				return m_notifyMethod;
			}
			set
			{
				// Setting and getting of reference types is atomic, no need to lock here
				m_notifyMethod = value;
			}
		}

		public virtual object NotifyContext
		{
			private get
			{
				// Setting and getting of reference types is atomic, no need to lock here
				return m_notifyContext;
			}
			set
			{
				// Setting and getting of reference types is atomic, no need to lock here
				m_notifyContext = value;
			}
		}

		private string m_notifyMethod;
		private object m_notifyContext;
		protected readonly object m_syncRoot = new object();
	}
}
