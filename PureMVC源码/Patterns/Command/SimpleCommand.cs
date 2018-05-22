/*
 *      简单命令类
 */

using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
    public class SimpleCommand : Notifier, ICommand
    {
		public virtual void Execute(INotification notification)
		{
		}
	}
}
