using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Handles user-oriented operations such as selection, movement, grouping and file commands.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		public DialogProcessor()
		{
			random = new Random();
			selectedShapes = new List<Shape>();
		}
		
		private readonly Random random;
		private readonly List<Shape> selectedShapes;
		
		/// <summary>
		/// Primary selected element.
		/// </summary>
		private Shape selection;
		public Shape Selection {
			get { return selection; }
			set {
				ClearSelection();
				if (value != null) {
					AddToSelection(value);
				}
			}
		}
		
		/// <summary>
		/// Currently selected top-level shapes.
		/// </summary>
		public IList<Shape> SelectedShapes {
			get { return selectedShapes.AsReadOnly(); }
		}
		
		/// <summary>
		/// Indicates whether a drag operation is currently active.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Last mouse location during dragging.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		
		/// <summary>
		/// Adds a rectangle at a random location.
		/// </summary>
		public void AddRandomRectangle()
		{
			int x = random.Next(100, 1000);
			int y = random.Next(100, 600);
			
			RectangleShape rect = new RectangleShape(new Rectangle(x, y, 100, 200));
			rect.FillColor = Color.White;
			rect.StrokeColor = Color.Black;
			rect.StrokeWidth = 1f;
			
			ShapeList.Add(rect);
		}
		
		/// <summary>
		/// Adds an ellipse at a random location.
		/// </summary>
		public void AddRandomEllipse()
		{
			int x = random.Next(100, 1000);
			int y = random.Next(100, 600);
			
			EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 140, 100));
			ellipse.FillColor = Color.White;
			ellipse.StrokeColor = Color.Black;
			ellipse.StrokeWidth = 1f;
			
			ShapeList.Add(ellipse);
		}
		
		/// <summary>
		/// Adds a line at a random location.
		/// </summary>
		public void AddRandomLine()
		{
			PointF startPoint = new PointF(random.Next(100, 1000), random.Next(100, 600));
			PointF endPoint;
			
			do {
				endPoint = new PointF(startPoint.X + random.Next(-150, 151), startPoint.Y + random.Next(-150, 151));
			} while (startPoint == endPoint);
			
			LineShape line = new LineShape(startPoint, endPoint);
			line.StrokeColor = Color.Black;
			line.StrokeWidth = 2f;
			
			ShapeList.Add(line);
		}
		
		/// <summary>
		/// Adds a triangle at a random location.
		/// </summary>
		public void AddRandomTriangle()
		{
			int x = random.Next(100, 1000);
			int y = random.Next(100, 600);
			
			TriangleShape triangle = new TriangleShape(new Rectangle(x, y, 140, 120));
			triangle.FillColor = Color.White;
			triangle.StrokeColor = Color.Black;
			triangle.StrokeWidth = 1f;
			
			ShapeList.Add(triangle);
		}
		
		/// <summary>
		/// Returns the top-most shape under the specified point.
		/// </summary>
		/// <param name="point">Test point.</param>
		/// <returns>The hit shape or <c>null</c>.</returns>
		public Shape ContainsPoint(PointF point)
		{
			for (int i = ShapeList.Count - 1; i >= 0; i--) {
				if (ShapeList[i] != null && ShapeList[i].Contains(point)) {
					return ShapeList[i];
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Adds a top-level shape to the current selection.
		/// </summary>
		public void AddToSelection(Shape shape)
		{
			if (shape == null) {
				return;
			}
			
			if (!selectedShapes.Contains(shape)) {
				selectedShapes.Add(shape);
				shape.IsSelected = true;
			}
			
			selection = shape;
		}
		
		/// <summary>
		/// Clears the current selection.
		/// </summary>
		public void ClearSelection()
		{
			foreach (Shape shape in selectedShapes) {
				if (shape != null) {
					shape.IsSelected = false;
				}
			}
			
			selectedShapes.Clear();
			selection = null;
		}
		
		/// <summary>
		/// Checks whether a shape participates in the current selection.
		/// </summary>
		public bool IsShapeSelected(Shape shape)
		{
			return shape != null && selectedShapes.Contains(shape);
		}
		
		/// <summary>
		/// Groups the selected top-level shapes and preserves their display order position.
		/// </summary>
		public bool GroupSelected()
		{
			List<Shape> shapesToGroup = new List<Shape>();
			int insertIndex = -1;
			
			for (int i = 0; i < ShapeList.Count; i++) {
				Shape shape = ShapeList[i];
				if (shape != null && selectedShapes.Contains(shape)) {
					if (insertIndex < 0) {
						insertIndex = i;
					}
					
					shapesToGroup.Add(shape);
				}
			}
			
			if (shapesToGroup.Count < 2) {
				return false;
			}
			
			ClearSelection();
			
			foreach (Shape shape in shapesToGroup) {
				ShapeList.Remove(shape);
			}
			
			GroupShape group = new GroupShape(shapesToGroup);
			if (insertIndex >= 0 && insertIndex <= ShapeList.Count) {
				ShapeList.Insert(insertIndex, group);
			} else {
				ShapeList.Add(group);
			}
			
			AddToSelection(group);
			return true;
		}
		
		/// <summary>
		/// Ungroups the currently selected group shapes.
		/// </summary>
		public bool UngroupSelected()
		{
			List<GroupShape> groupsToUngroup = new List<GroupShape>();
			
			foreach (Shape shape in selectedShapes) {
				GroupShape group = shape as GroupShape;
				if (group != null) {
					groupsToUngroup.Add(group);
				}
			}
			
			if (groupsToUngroup.Count == 0) {
				return false;
			}
			
			ClearSelection();
			List<Shape> releasedShapes = new List<Shape>();
			
			foreach (GroupShape group in groupsToUngroup) {
				int index = ShapeList.IndexOf(group);
				if (index < 0) {
					continue;
				}
				
				List<Shape> children = group.ReleaseChildren();
				ShapeList.RemoveAt(index);
				ShapeList.InsertRange(index, children);
				releasedShapes.AddRange(children);
			}
			
			foreach (Shape shape in releasedShapes) {
				AddToSelection(shape);
			}
			
			return releasedShapes.Count > 0;
		}
		
		/// <summary>
		/// Deletes the selected shapes.
		/// </summary>
		public void DeleteSelected()
		{
			if (selectedShapes.Count == 0) {
				return;
			}
			
			List<Shape> shapesToDelete = new List<Shape>(selectedShapes);
			ClearSelection();
			
			foreach (Shape shape in shapesToDelete) {
				if (shape != null) {
					ShapeList.Remove(shape);
				}
			}
		}
		
		/// <summary>
		/// Saves the current drawing to a JSON file.
		/// </summary>
		public void SaveToFile(string filePath)
		{
			DrawingJsonSerializer.Save(filePath, ShapeList);
		}
		
		/// <summary>
		/// Loads a drawing from a JSON file and clears the active selection.
		/// </summary>
		public void LoadFromFile(string filePath)
		{
			List<Shape> loadedShapes = DrawingJsonSerializer.Load(filePath);
			
			ClearSelection();
			ShapeList.Clear();
			ShapeList.AddRange(loadedShapes);
			IsDragging = false;
			LastLocation = PointF.Empty;
		}
		
		/// <summary>
		/// Changes the fill color of all selected shapes.
		/// </summary>
		public void ChangeSelectedFillColor(Color color)
		{
			foreach (Shape shape in selectedShapes) {
				if (shape != null) {
					ApplyFillColor(shape, color);
				}
			}
		}
		
		/// <summary>
		/// Changes the stroke color of all selected shapes.
		/// </summary>
		public void ChangeSelectedStrokeColor(Color color)
		{
			foreach (Shape shape in selectedShapes) {
				if (shape != null) {
					ApplyStrokeColor(shape, color);
				}
			}
		}
		
		/// <summary>
		/// Rotates the selected shapes.
		/// </summary>
		public void RotateSelected(float degrees)
		{
			foreach (Shape shape in selectedShapes) {
				if (shape != null) {
					shape.RotationAngle = NormalizeAngle(shape.RotationAngle + degrees);
				}
			}
		}
		
		/// <summary>
		/// Scales the selected shapes.
		/// </summary>
		public void ScaleSelected(float factor)
		{
			if (factor <= 0f) {
				return;
			}
			
			foreach (Shape shape in selectedShapes) {
				if (shape != null) {
					shape.ScaleFactor *= factor;
				}
			}
		}
		
		/// <summary>
		/// Translates the selected shapes by the vector defined from the previous mouse location.
		/// </summary>
		/// <param name="p">Current mouse position.</param>
		public void TranslateTo(PointF p)
		{
			if (selectedShapes.Count > 0) {
				float deltaX = p.X - lastLocation.X;
				float deltaY = p.Y - lastLocation.Y;
				
				foreach (Shape shape in selectedShapes) {
					if (shape != null) {
						shape.Location = new PointF(shape.Location.X + deltaX, shape.Location.Y + deltaY);
					}
				}
				
				lastLocation = p;
			}
		}
		
		private void ApplyFillColor(Shape shape, Color color)
		{
			GroupShape group = shape as GroupShape;
			if (group != null) {
				foreach (Shape child in group.Children) {
					ApplyFillColor(child, color);
				}
				
				return;
			}
			
			shape.FillColor = color;
		}
		
		private void ApplyStrokeColor(Shape shape, Color color)
		{
			GroupShape group = shape as GroupShape;
			if (group != null) {
				foreach (Shape child in group.Children) {
					ApplyStrokeColor(child, color);
				}
				
				return;
			}
			
			shape.StrokeColor = color;
		}
		
		private static float NormalizeAngle(float angle)
		{
			angle %= 360f;
			if (angle < 0f) {
				angle += 360f;
			}
			
			return angle;
		}
	}
}
