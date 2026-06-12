using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Base processor responsible for rendering the current shape list.
	/// </summary>
	public class DisplayProcessor
	{
		/// <summary>
		/// All top-level shapes that participate in drawing and hit testing.
		/// </summary>
		private List<Shape> shapeList = new List<Shape>();
		public List<Shape> ShapeList {
			get { return shapeList; }
			set { shapeList = value ?? new List<Shape>(); }
		}
		
		/// <summary>
		/// Redraws the complete scene in the viewport.
		/// </summary>
		public void ReDraw(object sender, PaintEventArgs e)
		{
			if (e == null) {
				throw new ArgumentNullException("e");
			}
			
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Draw(e.Graphics);
		}
		
		/// <summary>
		/// Draws all shapes in display order.
		/// </summary>
		/// <param name="grfx">Graphics surface used for drawing.</param>
		public virtual void Draw(Graphics grfx)
		{
			if (grfx == null) {
				throw new ArgumentNullException("grfx");
			}
			
			foreach (Shape item in ShapeList) {
				DrawShape(grfx, item);
			}
		}
		
		/// <summary>
		/// Draws a single shape if it is available.
		/// </summary>
		/// <param name="grfx">Graphics surface used for drawing.</param>
		/// <param name="item">Shape to render.</param>
		public virtual void DrawShape(Graphics grfx, Shape item)
		{
			if (item == null) {
				return;
			}
			
			item.DrawSelf(grfx);
		}
	}
}
