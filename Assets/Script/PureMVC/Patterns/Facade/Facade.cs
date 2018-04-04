
using System;

using PureMVC.Core;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Patterns
{
    public class Facade : IFacade
	{
        protected Facade() 
        {
			InitializeFacade();
		}

		public virtual void RegisterProxy(IProxy proxy)
		{
			// The model is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the model.
			m_model.RegisterProxy(proxy);
		}

        public virtual IProxy RetrieveProxy(string proxyName)
		{
			// The model is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the model.
			return m_model.RetrieveProxy(proxyName);
		}

        public virtual IProxy RemoveProxy(string proxyName)
		{
			// The model is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the model.
			return m_model.RemoveProxy(proxyName);
		}

        public virtual bool HasProxy(string proxyName)
		{
			// The model is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the model.
			return m_model.HasProxy(proxyName);
		}

        public virtual void RegisterCommand(string notificationName, Type commandType)
		{
			// The controller is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the controller.
			m_controller.RegisterCommand(notificationName, commandType);
		}

        public virtual void RemoveCommand(string notificationName)
		{
			// The controller is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the controller.
			m_controller.RemoveCommand(notificationName);
		}

        public virtual bool HasCommand(string notificationName)
		{
			// The controller is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the controller.
			return m_controller.HasCommand(notificationName);
		}

        public virtual void RegisterMediator(IMediator mediator)
		{
			// The view is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the view.
			m_view.RegisterMediator(mediator);
		}

        public virtual IMediator RetrieveMediator(string mediatorName)
		{
			// The view is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the view.
			return m_view.RetrieveMediator(mediatorName);
		}

        public virtual IMediator RemoveMediator(string mediatorName)
		{
			// The view is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the view.
			return m_view.RemoveMediator(mediatorName);
		}

        public virtual bool HasMediator(string mediatorName)
		{
			// The view is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the view.
			return m_view.HasMediator(mediatorName);
		}

        public virtual void NotifyObservers(INotification notification)
		{
			// The view is initialized in the constructor of the singleton, so this call should be thread safe.
			// This method is thread safe on the view.
			m_view.NotifyObservers(notification);
		}

        public virtual void SendNotification(string notificationName)
		{
			NotifyObservers(new Notification(notificationName));
		}

        public virtual void SendNotification(string notificationName, object body)
		{
			NotifyObservers(new Notification(notificationName, body));
		}

        public virtual void SendNotification(string notificationName, object body, string type)
		{
			NotifyObservers(new Notification(notificationName, body, type));
		}

		public static IFacade Instance
		{
			get
			{
				if (m_instance == null)
				{
					lock (m_staticSyncRoot)
					{
						if (m_instance == null) m_instance = new Facade();
					}
				}

				return m_instance;
			}
		}

        static Facade()
        {
        }

        protected virtual void InitializeFacade()
        {
			InitializeModel();
			InitializeController();
			InitializeView();
		}

		protected virtual void InitializeController()
        {
			if (m_controller != null) return;
			m_controller = Controller.Instance;
		}

        protected virtual void InitializeModel()
        {
			if (m_model != null) return;
			m_model = Model.Instance;
		}

        protected virtual void InitializeView()
        {
			if (m_view != null) return;
			m_view = View.Instance;
		}

		protected IController m_controller;
        protected IModel m_model;
        protected IView m_view;

        protected static volatile IFacade m_instance;
		protected static readonly object m_staticSyncRoot = new object();
	}
}
