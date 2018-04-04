
using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Patterns
{
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
        // facade->view->Observer->ExecuteCommand->Execute
        public virtual void Execute(INotification notification)
		{
		}
	}
}
