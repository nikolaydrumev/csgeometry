using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Класът линия е примитив, който е наследник на базовия Shape.
	/// </summary>
	public class LineShape : Shape
	{
		#region Constructor
		
		public LineShape(PointF startPoint, PointF endPoint) : base()
		{
			Name = "Line";
			StartPoint = startPoint;
			EndPoint = endPoint;
		}
		
		public LineShape(LineShape line) : base(line)
		{
			Name = "Line";
			startPoint = line.StartPoint;
			endPoint = line.EndPoint;
			UpdateBounds();
		}
		
		#endregion
		
		#region Properties
		
		private PointF startPoint;
		public PointF StartPoint {
			get { return startPoint; }
			set {
				startPoint = value;
				UpdateBounds();
			}
		}
		
		private PointF endPoint;
		public PointF EndPoint {
			get { return endPoint; }
			set {
				endPoint = value;
				UpdateBounds();
			}
		}
		
		public override PointF Location {
			get { return Rectangle.Location; }
			set {
				float deltaX = value.X - Rectangle.X;
				float deltaY = value.Y - Rectangle.Y;
				
				startPoint = new PointF(startPoint.X + deltaX, startPoint.Y + deltaY);
				endPoint = new PointF(endPoint.X + deltaX, endPoint.Y + deltaY);
				
				UpdateBounds();
			}
		}
		
		#endregion
		
		/// <summary>
		/// Проверка за принадлежност на точка point към линията.
		/// </summary>
		public override bool Contains(PointF point)
		{
			using (GraphicsPath path = GetPath()) {
				using (Matrix transform = GetTransformMatrix()) {
					path.Transform(transform);
				}
				
				using (Pen pen = new Pen(Color.Black, StrokeWidth + 6f)) {
					pen.StartCap = LineCap.Round;
					pen.EndCap = LineCap.Round;
					pen.LineJoin = LineJoin.Round;
					
					return path.IsOutlineVisible(point, pen);
				}
			}
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
				
				using (Pen pen = new Pen(StrokeColor, StrokeWidth)) {
					pen.StartCap = LineCap.Round;
					pen.EndCap = LineCap.Round;
					pen.LineJoin = LineJoin.Round;
					grfx.DrawPath(pen, path);
				}
			}
			
			base.DrawSelf(grfx);
		}
		
		protected override GraphicsPath GetPath()
		{
			GraphicsPath path = new GraphicsPath();
			path.AddLine(StartPoint, EndPoint);
			
			return path;
		}
		
		private void UpdateBounds()
		{
			Rectangle = RectangleF.FromLTRB(
				Math.Min(startPoint.X, endPoint.X),
				Math.Min(startPoint.Y, endPoint.Y),
				Math.Max(startPoint.X, endPoint.X),
				Math.Max(startPoint.Y, endPoint.Y));
		}
	}
}
