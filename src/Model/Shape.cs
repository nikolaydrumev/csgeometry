using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Base shape class that stores the common geometry, styling and transformation data.
	/// </summary>
	public abstract class Shape
	{
		public Shape()
		{
			name = string.Empty;
			fillColor = Color.White;
			strokeColor = Color.Black;
			strokeWidth = 1f;
			rotationAngle = 0f;
			scaleFactor = 1f;
		}
		
		public Shape(RectangleF rect)
			: this()
		{
			rectangle = rect;
		}
		
		public Shape(Shape shape)
		{
			if (shape == null) {
				throw new ArgumentNullException("shape");
			}
			
			rectangle = shape.rectangle;
			Name = shape.Name;
			FillColor = shape.FillColor;
			StrokeColor = shape.StrokeColor;
			StrokeWidth = shape.StrokeWidth;
			IsSelected = shape.IsSelected;
			RotationAngle = shape.RotationAngle;
			ScaleFactor = shape.ScaleFactor;
		}
		
		/// <summary>
		/// Bounding rectangle of the shape in model coordinates.
		/// </summary>
		private RectangleF rectangle;
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}
		
		/// <summary>
		/// Width of the shape.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Height of the shape.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Top-left location of the shape.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// <summary>
		/// User-friendly shape name.
		/// </summary>
		private string name;
		public virtual string Name {
			get { return name; }
			set { name = value; }
		}
		
		/// <summary>
		/// Fill color of the shape.
		/// </summary>
		private Color fillColor;
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}
		
		/// <summary>
		/// Stroke color of the shape.
		/// </summary>
		private Color strokeColor;
		public virtual Color StrokeColor {
			get { return strokeColor; }
			set { strokeColor = value; }
		}
		
		/// <summary>
		/// Stroke width of the shape.
		/// </summary>
		private float strokeWidth;
		public virtual float StrokeWidth {
			get { return strokeWidth; }
			set { strokeWidth = value < 1f ? 1f : value; }
		}
		
		/// <summary>
		/// Indicates whether the shape is currently selected.
		/// </summary>
		private bool isSelected;
		public virtual bool IsSelected {
			get { return isSelected; }
			set { isSelected = value; }
		}
		
		/// <summary>
		/// Rotation angle in degrees.
		/// </summary>
		private float rotationAngle;
		public virtual float RotationAngle {
			get { return rotationAngle; }
			set { rotationAngle = value; }
		}
		
		/// <summary>
		/// Scale factor applied around the shape center.
		/// </summary>
		private float scaleFactor;
		public virtual float ScaleFactor {
			get { return scaleFactor; }
			set { scaleFactor = value <= 0.1f ? 0.1f : value; }
		}
		
		/// <summary>
		/// Determines whether a point belongs to the transformed shape.
		/// </summary>
		/// <param name="point">Point to test.</param>
		/// <returns><c>true</c> if the point is inside the shape; otherwise <c>false</c>.</returns>
		public virtual bool Contains(PointF point)
		{
			using (GraphicsPath path = CreateTransformedPath()) {
				return path.IsVisible(point);
			}
		}
		
		/// <summary>
		/// Draws the selection outline for the shape.
		/// </summary>
		/// <param name="grfx">Graphics surface used for drawing.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			if (IsSelected) {
				RectangleF selectionRectangle = GetSelectionRectangle();
				
				using (Pen selectionPen = new Pen(Color.Blue, 1f)) {
					selectionPen.DashStyle = DashStyle.Dash;
					grfx.DrawRectangle(selectionPen, selectionRectangle.X, selectionRectangle.Y, selectionRectangle.Width, selectionRectangle.Height);
				}
			}
		}
		
		/// <summary>
		/// Gets the transformed bounds of the shape.
		/// </summary>
		public virtual RectangleF GetBounds()
		{
			using (GraphicsPath path = CreateTransformedPath()) {
				return path.GetBounds();
			}
		}
		
		/// <summary>
		/// Applies an external rotation and scaling around a shared transformation center.
		/// </summary>
		public virtual void ApplyExternalTransform(PointF transformationCenter, float rotationDegrees, float scaleMultiplier)
		{
			PointF currentCenter = GetCenter();
			PointF transformedCenter = TransformPoint(currentCenter, transformationCenter, rotationDegrees, scaleMultiplier);
			float deltaX = transformedCenter.X - currentCenter.X;
			float deltaY = transformedCenter.Y - currentCenter.Y;
			
			Location = new PointF(Location.X + deltaX, Location.Y + deltaY);
			RotationAngle = NormalizeAngle(RotationAngle + rotationDegrees);
			ScaleFactor *= scaleMultiplier;
		}
		
		/// <summary>
		/// Creates the graphics path with the current rotation and scaling applied.
		/// </summary>
		internal GraphicsPath CreateTransformedPath()
		{
			GraphicsPath path = GetPath();
			
			using (Matrix transform = GetTransformMatrix()) {
				path.Transform(transform);
			}
			
			return path;
		}
		
		/// <summary>
		/// Returns the center point of the current bounding rectangle.
		/// </summary>
		protected PointF GetCenter()
		{
			return new PointF(Rectangle.X + Rectangle.Width / 2f, Rectangle.Y + Rectangle.Height / 2f);
		}
		
		/// <summary>
		/// Builds the transform matrix for scaling and rotation around the shape center.
		/// </summary>
		protected Matrix GetTransformMatrix()
		{
			PointF center = GetCenter();
			Matrix transform = new Matrix();
			
			transform.Translate(-center.X, -center.Y, MatrixOrder.Append);
			transform.Scale(ScaleFactor, ScaleFactor, MatrixOrder.Append);
			transform.Rotate(RotationAngle, MatrixOrder.Append);
			transform.Translate(center.X, center.Y, MatrixOrder.Append);
			
			return transform;
		}
		
		/// <summary>
		/// Calculates the dashed selection rectangle shown around selected shapes.
		/// </summary>
		protected RectangleF GetSelectionRectangle()
		{
			RectangleF selectionRectangle = GetBounds();
			selectionRectangle.Inflate(StrokeWidth / 2f + 3f, StrokeWidth / 2f + 3f);
			
			return selectionRectangle;
		}
		
		/// <summary>
		/// Rotates and scales a point around a shared center.
		/// </summary>
		protected static PointF TransformPoint(PointF point, PointF center, float rotationDegrees, float scaleMultiplier)
		{
			double radians = rotationDegrees * Math.PI / 180d;
			double scaledX = (point.X - center.X) * scaleMultiplier;
			double scaledY = (point.Y - center.Y) * scaleMultiplier;
			double rotatedX = scaledX * Math.Cos(radians) - scaledY * Math.Sin(radians);
			double rotatedY = scaledX * Math.Sin(radians) + scaledY * Math.Cos(radians);
			
			return new PointF(center.X + (float)rotatedX, center.Y + (float)rotatedY);
		}
		
		protected static float NormalizeAngle(float angle)
		{
			angle %= 360f;
			if (angle < 0f) {
				angle += 360f;
			}
			
			return angle;
		}
		
		/// <summary>
		/// Creates the untransformed path that defines the concrete shape geometry.
		/// </summary>
		protected abstract GraphicsPath GetPath();
	}
}
