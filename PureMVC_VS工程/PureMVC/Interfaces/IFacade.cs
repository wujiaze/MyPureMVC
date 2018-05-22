using System;

namespace PureMVC.Interfaces
{
    public interface IFacade : INotifier
	{
		void RegisterProxy(IProxy proxy);
		IProxy RetrieveProxy(string proxyName);
        IProxy RemoveProxy(string proxyName);
		bool HasProxy(string proxyName);



		#region Command
        void RegisterCommand(string notificationName, Type commandType);
        void RemoveCommand(string notificationName);
		bool HasCommand(string notificationName);

		#endregion

		#region Mediator
		void RegisterMediator(IMediator mediator);
		IMediator RetrieveMediator(string mediatorName);
        IMediator RemoveMediator(string mediatorName);
		bool HasMediator(string mediatorName);

		#endregion

		#region Observer
		void NotifyObservers(INotification note);

		#endregion
	}
}
