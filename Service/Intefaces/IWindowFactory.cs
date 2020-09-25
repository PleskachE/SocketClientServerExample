using System.Windows;

namespace Service.Intefaces
{
    public interface IWindowFactory
    {
        Window CreateWindow(WindowCreationOptions options);
    }

    public class WindowCreationOptions
    {
        public string Title { get; set; } = "";
        public WindowSize WindowSize { get; set; }
    }

	public class WindowSize
	{
		#region Static Fields and Constants

		public static readonly WindowSize Default = new WindowSize(new Size(double.NaN, double.NaN))
		{
			MinSize = new Size(300, 150),
			MaxSize = new Size(300, 150)
		};

		#endregion

		#region Ctors

		public WindowSize()
		{
			Size = new Size(double.NaN, double.NaN);
			MinSize = new Size(0, 0);
			MaxSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
		}

		public WindowSize(Size size) : this()
		{
			Size = size;
		}

		#endregion

		#region Properties

		public bool IsFixedSize { get; set; }
		public Size MaxSize { get; set; }
		public Size MinSize { get; set; }
		public Size Size { get; set; }
		public SizeToContent SizeToContent { get; set; } = SizeToContent.Manual;

		#endregion
	}
}
