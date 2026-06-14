using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Draw
{
	/// <summary>
	/// Serializes and deserializes drawings through simple DTO classes in JSON format.
	/// </summary>
	public static class DrawingJsonSerializer
	{
		/// <summary>
		/// Saves the provided top-level shapes to a JSON file.
		/// </summary>
		public static void Save(string filePath, IList<Shape> shapes)
		{
			ValidateFilePath(filePath);
			
			if (shapes == null) {
				throw new ArgumentNullException("shapes");
			}
			
			DrawingProjectDto project = new DrawingProjectDto();
			project.Shapes = new List<ShapeDto>();
			
			foreach (Shape shape in shapes) {
				project.Shapes.Add(ToDto(shape));
			}
			
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DrawingProjectDto));
			using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
				serializer.WriteObject(stream, project);
			}
		}
		
		/// <summary>
		/// Loads top-level shapes from a JSON file.
		/// </summary>
		public static List<Shape> Load(string filePath)
		{
			ValidateFilePath(filePath);
			
			if (!File.Exists(filePath)) {
				throw new FileNotFoundException("The selected file does not exist.", filePath);
			}
			
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DrawingProjectDto));
			
			try {
				using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
					DrawingProjectDto project = serializer.ReadObject(stream) as DrawingProjectDto;
					List<Shape> shapes = new List<Shape>();
					
					if (project == null) {
						throw new InvalidOperationException("The file does not contain a valid drawing project.");
					}
					
					if (project.Shapes != null) {
						foreach (ShapeDto shapeDto in project.Shapes) {
							shapes.Add(FromDto(shapeDto));
						}
					}
					
					return shapes;
				}
			} catch (SerializationException exception) {
				throw new InvalidOperationException("The file is not a valid Draw project in JSON format.", exception);
			}
		}
		
		private static ShapeDto ToDto(Shape shape)
		{
			if (shape == null) {
				throw new InvalidOperationException("The project contains an empty top-level shape.");
			}
			
			ShapeDto dto = new ShapeDto();
			dto.Type = GetShapeTypeName(shape);
			dto.Name = shape.Name;
			dto.FillColorArgb = shape.FillColor.ToArgb();
			dto.StrokeColorArgb = shape.StrokeColor.ToArgb();
			dto.StrokeWidth = shape.StrokeWidth;
			dto.RotationAngle = shape.RotationAngle;
			dto.ScaleFactor = shape.ScaleFactor;
			dto.RectangleX = shape.Rectangle.X;
			dto.RectangleY = shape.Rectangle.Y;
			dto.RectangleWidth = shape.Rectangle.Width;
			dto.RectangleHeight = shape.Rectangle.Height;
			
			LineShape line = shape as LineShape;
			if (line != null) {
				dto.LineStartX = line.StartPoint.X;
				dto.LineStartY = line.StartPoint.Y;
				dto.LineEndX = line.EndPoint.X;
				dto.LineEndY = line.EndPoint.Y;
			}
			
			GroupShape group = shape as GroupShape;
			if (group != null) {
				dto.Children = new List<ShapeDto>();
				foreach (Shape child in group.Children) {
					if (child == null) {
						throw new InvalidOperationException("A group contains an empty child shape.");
					}
					
					dto.Children.Add(ToDto(child));
				}
			}
			
			return dto;
		}
		
		private static Shape FromDto(ShapeDto dto)
		{
			if (dto == null) {
				throw new InvalidOperationException("Missing shape data in the project file.");
			}
			
			if (string.IsNullOrEmpty(dto.Type)) {
				throw new InvalidOperationException("Missing shape type in the project file.");
			}
			
			Shape shape;
			switch (dto.Type) {
				case "RectangleShape":
				case "Rectangle":
					shape = new RectangleShape(new RectangleF(dto.RectangleX, dto.RectangleY, dto.RectangleWidth, dto.RectangleHeight));
					break;
				case "EllipseShape":
				case "Ellipse":
					shape = new EllipseShape(new RectangleF(dto.RectangleX, dto.RectangleY, dto.RectangleWidth, dto.RectangleHeight));
					break;
				case "LineShape":
				case "Line":
					shape = new LineShape(
						new PointF(dto.LineStartX, dto.LineStartY),
						new PointF(dto.LineEndX, dto.LineEndY));
					break;
				case "TriangleShape":
				case "Triangle":
					shape = new TriangleShape(new RectangleF(dto.RectangleX, dto.RectangleY, dto.RectangleWidth, dto.RectangleHeight));
					break;
				case "GroupShape":
				case "Group":
					List<Shape> children = new List<Shape>();
					if (dto.Children != null) {
						foreach (ShapeDto childDto in dto.Children) {
							children.Add(FromDto(childDto));
						}
					}
					
					shape = new GroupShape(children);
					break;
				default:
					throw new InvalidOperationException("Unsupported shape type in project file: " + dto.Type);
			}
			
			shape.Name = dto.Name ?? string.Empty;
			shape.FillColor = Color.FromArgb(dto.FillColorArgb);
			shape.StrokeColor = Color.FromArgb(dto.StrokeColorArgb);
			shape.StrokeWidth = dto.StrokeWidth;
			shape.RotationAngle = dto.RotationAngle;
			shape.ScaleFactor = dto.ScaleFactor <= 0f ? 1f : dto.ScaleFactor;
			shape.IsSelected = false;
			
			return shape;
		}
		
		private static string GetShapeTypeName(Shape shape)
		{
			if (shape is TriangleShape) {
				return "Triangle";
			}
			
			return shape.GetType().Name;
		}
		
		private static void ValidateFilePath(string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) {
				throw new ArgumentException("File path cannot be empty.", "filePath");
			}
		}
		
		[DataContract]
		private class DrawingProjectDto
		{
			[DataMember(Order = 1)]
			public List<ShapeDto> Shapes { get; set; }
		}
		
		[DataContract]
		private class ShapeDto
		{
			[DataMember(Order = 1)]
			public string Type { get; set; }
			
			[DataMember(Order = 2)]
			public string Name { get; set; }
			
			[DataMember(Order = 3)]
			public float RectangleX { get; set; }
			
			[DataMember(Order = 4)]
			public float RectangleY { get; set; }
			
			[DataMember(Order = 5)]
			public float RectangleWidth { get; set; }
			
			[DataMember(Order = 6)]
			public float RectangleHeight { get; set; }
			
			[DataMember(Order = 7)]
			public int FillColorArgb { get; set; }
			
			[DataMember(Order = 8)]
			public int StrokeColorArgb { get; set; }
			
			[DataMember(Order = 9)]
			public float StrokeWidth { get; set; }
			
			[DataMember(Order = 10)]
			public float RotationAngle { get; set; }
			
			[DataMember(Order = 11)]
			public float ScaleFactor { get; set; }
			
			[DataMember(Order = 12)]
			public float LineStartX { get; set; }
			
			[DataMember(Order = 13)]
			public float LineStartY { get; set; }
			
			[DataMember(Order = 14)]
			public float LineEndX { get; set; }
			
			[DataMember(Order = 15)]
			public float LineEndY { get; set; }
			
			[DataMember(Order = 16)]
			public List<ShapeDto> Children { get; set; }
		}
	}
}
