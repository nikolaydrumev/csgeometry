using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Класът елипса е примитив, който е наследник на базовия Shape.
	/// </summary>
	public class EllipseShape : Shape
	{
		#region Constructor
		
		public EllipseShape(RectangleF rect) : base(rect)
		{
			Name = "Ellipse";
		}
		
		public EllipseShape(EllipseShape ellipse) : base(ellipse)
		{
			Name = "Ellipse";
		}
		
		#endregion
		
		/// <summary>
		/// Проверка за принадлежност на точка point към елипсата.
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
			path.AddEllipse(Rectangle);
			
			return path;
		}
	}
}
