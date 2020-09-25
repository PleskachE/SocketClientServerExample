using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfTcpClient.ViewModel
{
	public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
	{

		public event PropertyChangedEventHandler PropertyChanged;

		public virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			var handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Dispose()
		{
			OnDispose();
		}

		protected virtual void OnDispose()
		{
		}
	}
}
