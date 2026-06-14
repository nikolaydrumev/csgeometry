using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Triangle primitive that uses the bounding rectangle as a guide for its geometry.
	/// </summary>
	public class TriangleShape : Shape
	{
		#region Constructor
		
		public TriangleShape(RectangleF rect) : base(rect)
		{
			Name = "Triangle";
		}
		
		public TriangleShape(TriangleShape triangle) : base(triangle)
		{
			Name = "Triangle";
		}
		
		#endregion
		
		/// <summary>
		/// Hit test against the transformed triangle path.
		/// </summary>
		public override bool Contains(PointF point)
		{
			using (GraphicsPath path = GetPath()) {
				using (Matrix transform = GetTransformMatrix()) {
					path.Transform(transform);
				}
				
				return path.IsVisible(point);
			}
		}
		
		/// <summary>
		/// Draws the filled triangle and its outline.
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
			PointF topPoint = new PointF(Rectangle.Left + Rectangle.Width / 2f, Rectangle.Top);
			PointF bottomRightPoint = new PointF(Rectangle.Right, Rectangle.Bottom);
			PointF bottomLeftPoint = new PointF(Rectangle.Left, Rectangle.Bottom);
			
			GraphicsPath path = new GraphicsPath();
			path.AddPolygon(new PointF[] {
				topPoint,
				bottomRightPoint,
				bottomLeftPoint
			});
			
			return path;
		}
	}
}
