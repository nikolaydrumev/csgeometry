using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	public class RectangleShape : Shape
	{
		#region Constructor
		
		public RectangleShape(RectangleF rect) : base(rect)
		{
			Name = "Rectangle";
		}
		
		public RectangleShape(RectangleShape rectangle) : base(rectangle)
		{
			Name = "Rectangle";
		}
		
		#endregion
		
		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// </summary>
		public override bool Contains(PointF point)
		{
			return base.Contains(point);
		}
		
		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			using (GraphicsPath path = GetPath()) {
				using (Matrix transform = GetTransformMatrix()) {
					path.Transform(transform);
				}
				
				using (SolidBrush brush = new SolidBrush(FillColor)) {
					grfx.FillPath(brush, path);
				}
				
				using (Pen pen = new Pen(StrokeColor, StrokeWidth)) {
					grfx.DrawPath(pen, path);
				}
			}
			
			base.DrawSelf(grfx);
		}
		
		protected override GraphicsPath GetPath()
		{
			GraphicsPath path = new GraphicsPath();
			path.AddRectangle(Rectangle);
			
			return path;
		}
	}
}
