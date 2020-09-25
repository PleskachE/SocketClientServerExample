using System.Windows;
using Service.Intefaces;

namespace Service
{
	public sealed class WindowFactory : IWindowFactory
	{
		public Window CreateWindow(WindowCreationOptions options)
		{
			return new Window()
			{
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
				Title = options.Title,
				Width = options.WindowSize.Size.Width,
				Height = options.WindowSize.Size.Height,
				Owner = Application.Current.MainWindow
			};
		}
	}
}
