using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Viewport control used to render the drawing with double buffering enabled.
	/// </summary>
	public partial class DoubleBufferedPanel : UserControl
	{
		/// <summary>
		/// Initializes the double-buffered viewport control.
		/// </summary>
		public DoubleBufferedPanel()
		{
			InitializeComponent();
		}
	}
}
