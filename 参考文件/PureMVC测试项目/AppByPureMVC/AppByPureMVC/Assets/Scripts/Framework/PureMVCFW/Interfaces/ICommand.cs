
namespace PureMVC.Interfaces
{
    public interface ICommand
    {
		void Execute(INotification notification);
    }
}
