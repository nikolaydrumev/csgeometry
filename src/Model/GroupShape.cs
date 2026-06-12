using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Composite shape that allows several child shapes to be manipulated together.
	/// </summary>
	public class GroupShape : Shape
	{
		public GroupShape()
		{
			Name = "Group";
			children = new List<Shape>();
		}
		
		public GroupShape(IEnumerable<Shape> shapes) : this()
		{
			if (shapes == null) {
				return;
			}
			
			foreach (Shape shape in shapes) {
				if (shape != null) {
					children.Add(shape);
				}
			}
		}
		
		private readonly List<Shape> children;
		public List<Shape> Children {
			get { return children; }
		}
		
		public override RectangleF Rectangle {
			get { return CalculateBounds(); }
			set { MoveBy(value.X - Rectangle.X, value.Y - Rectangle.Y); }
		}
		
		public override PointF Location {
			get { return Rectangle.Location; }
			set { Rectangle = new RectangleF(value, Rectangle.Size); }
		}
		
		public override bool Contains(PointF point)
		{
			if (Children.Count == 0) {
				return false;
			}
			
			PointF localPoint = TransformPoint(point, GetCenter(), -RotationAngle, 1f / ScaleFactor);
			
			for (int i = Children.Count - 1; i >= 0; i--) {
				if (Children[i] != null && Children[i].Contains(localPoint)) {
					return true;
				}
			}
			
			return false;
		}
		
		public override void DrawSelf(Graphics grfx)
		{
			GraphicsState state = grfx.Save();
			
			using (Matrix transform = GetTransformMatrix()) {
				grfx.MultiplyTransform(transform);
			}
			
			foreach (Shape child in Children) {
				if (child != null) {
					child.DrawSelf(grfx);
				}
			}
			
			grfx.Restore(state);
			base.DrawSelf(grfx);
		}
		
		public List<Shape> ReleaseChildren()
		{
			List<Shape> releasedChildren = new List<Shape>(Children);
			PointF groupCenter = GetCenter();
			float groupRotation = RotationAngle;
			float groupScale = ScaleFactor;
			
			foreach (Shape child in releasedChildren) {
				if (child != null) {
					child.ApplyExternalTransform(groupCenter, groupRotation, groupScale);
				}
			}
			
			Children.Clear();
			RotationAngle = 0f;
			ScaleFactor = 1f;
			
			return releasedChildren;
		}
		
		protected override GraphicsPath GetPath()
		{
			GraphicsPath path = new GraphicsPath();
			
			foreach (Shape child in Children) {
				if (child == null) {
					continue;
				}
				
				using (GraphicsPath childPath = child.CreateTransformedPath()) {
					path.AddPath(childPath, false);
				}
			}
			
			return path;
		}
		
		private RectangleF CalculateBounds()
		{
			bool hasBounds = false;
			RectangleF bounds = RectangleF.Empty;
			
			foreach (Shape child in Children) {
				if (child == null) {
					continue;
				}
				
				RectangleF childBounds = child.GetBounds();
				
				if (!hasBounds) {
					bounds = childBounds;
					hasBounds = true;
				} else {
					bounds = RectangleF.Union(bounds, childBounds);
				}
			}
			
			return hasBounds ? bounds : RectangleF.Empty;
		}
		
		private void MoveBy(float deltaX, float deltaY)
		{
			if (deltaX == 0f && deltaY == 0f) {
				return;
			}
			
			foreach (Shape child in Children) {
				if (child != null) {
					child.Location = new PointF(child.Location.X + deltaX, child.Location.Y + deltaY);
				}
			}
		}
	}
}
