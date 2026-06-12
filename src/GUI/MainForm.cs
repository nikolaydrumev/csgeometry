using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Main application form that hosts the drawing viewport and editor commands.
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Central processor that exposes the drawing operations used by the form.
		/// </summary>
		private readonly DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			InitializeComponent();
			UpdateStatus("Редакторът е готов.");
		}

		/// <summary>
		/// Closes the application.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog()) {
				saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
				saveFileDialog.DefaultExt = "json";
				saveFileDialog.AddExtension = true;
				saveFileDialog.Title = "Запис на проект";
				
				if (saveFileDialog.ShowDialog() != DialogResult.OK) {
					UpdateStatusAndRefresh("Записът на проекта е отказан.");
					return;
				}
				
				try {
					dialogProcessor.SaveToFile(saveFileDialog.FileName);
					UpdateStatusAndRefresh("Проектът е записан във файл " + Path.GetFileName(saveFileDialog.FileName) + ".");
				} catch (Exception exception) {
					ShowOperationError("Грешка при запис", "Проектът не можа да бъде записан.", exception);
				}
			}
		}
		
		void LoadToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
				openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
				openFileDialog.Title = "Зареждане на проект";
				
				if (openFileDialog.ShowDialog() != DialogResult.OK) {
					UpdateStatusAndRefresh("Зареждането на проекта е отказано.");
					return;
				}
				
				try {
					dialogProcessor.LoadFromFile(openFileDialog.FileName);
					UpdateStatusAndRefresh("Проектът е зареден от файл " + Path.GetFileName(openFileDialog.FileName) + ".");
				} catch (Exception exception) {
					ShowOperationError("Грешка при зареждане", "Проектът не можа да бъде зареден.", exception);
				}
			}
		}
		
		/// <summary>
		/// Repaints the viewport after model changes.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			AddRectangle();
		}
		
		void AddRectangleToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddRectangle();
		}
		
		void AddEllipseToolStripMenuItemClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomEllipse();
			UpdateStatusAndRefresh("Добавена е елипса.");
		}
		
		void AddLineToolStripMenuItemClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomLine();
			UpdateStatusAndRefresh("Добавена е линия.");
		}
		
		void DeleteSelectedToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!EnsureSelection("изтриване")) {
				return;
			}
			
			dialogProcessor.DeleteSelected();
			UpdateStatusAndRefresh("Избраните фигури са изтрити.");
		}
		
		void ChangeFillColorToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!EnsureSelection("смяна на цвят на запълване")) {
				return;
			}
			
			using (ColorDialog colorDialog = new ColorDialog()) {
				if (dialogProcessor.Selection != null) {
					colorDialog.Color = dialogProcessor.Selection.FillColor;
				}
				
				if (colorDialog.ShowDialog() == DialogResult.OK) {
					dialogProcessor.ChangeSelectedFillColor(colorDialog.Color);
					UpdateStatusAndRefresh("Променен е цветът на запълване.");
				} else {
					UpdateStatusAndRefresh("Смяната на цвят на запълване е отказана.");
				}
			}
		}
		
		void ChangeStrokeColorToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!EnsureSelection("смяна на цвят на контур")) {
				return;
			}
			
			using (ColorDialog colorDialog = new ColorDialog()) {
				if (dialogProcessor.Selection != null) {
					colorDialog.Color = dialogProcessor.Selection.StrokeColor;
				}
				
				if (colorDialog.ShowDialog() == DialogResult.OK) {
					dialogProcessor.ChangeSelectedStrokeColor(colorDialog.Color);
					UpdateStatusAndRefresh("Променен е цветът на контура.");
				} else {
					UpdateStatusAndRefresh("Смяната на цвят на контура е отказана.");
				}
			}
		}
		
		void RotateLeftToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!EnsureSelection("завъртане наляво")) {
				return;
			}
			
			dialogProcessor.RotateSelected(-15f);
			UpdateStatusAndRefresh("Избраните фигури са завъртени наляво.");
		}
		
		void RotateRightToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!EnsureSelection("завъртане надясно")) {
				return;
			}
			
			dialogProcessor.RotateSelected(15f);
			UpdateStatusAndRefresh("Избраните фигури са завъртени надясно.");
		}
		
		void ScaleUpToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!EnsureSelection("увеличаване")) {
				return;
			}
			
			dialogProcessor.ScaleSelected(1.1f);
			UpdateStatusAndRefresh("Мащабът на избраните фигури е увеличен.");
		}
		
		void ScaleDownToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!EnsureSelection("намаляване")) {
				return;
			}
			
			dialogProcessor.ScaleSelected(0.9f);
			UpdateStatusAndRefresh("Мащабът на избраните фигури е намален.");
		}
		
		void GroupSelectedToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (dialogProcessor.SelectedShapes.Count < 2) {
				UpdateStatusAndRefresh("Необходими са поне две фигури за групиране.");
				return;
			}
			
			if (dialogProcessor.GroupSelected()) {
				UpdateStatusAndRefresh("Избраните фигури са групирани.");
			} else {
				UpdateStatusAndRefresh("Групирането не беше изпълнено.");
			}
		}
		
		void UngroupSelectedToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (dialogProcessor.UngroupSelected()) {
				UpdateStatusAndRefresh("Избраните групи са разгрупирани.");
			} else {
				UpdateStatusAndRefresh("Няма избрана група за разгрупиране.");
			}
		}

		/// <summary>
		/// Handles mouse selection and starts drag operations when needed.
		/// </summary>
		void ViewPortMouseDown(object sender, MouseEventArgs e)
		{
			if (!pickUpSpeedButton.Checked) {
				return;
			}
			
			bool isControlPressed = (ModifierKeys & Keys.Control) == Keys.Control;
			Shape hitShape = dialogProcessor.ContainsPoint(e.Location);
			
			if (isControlPressed) {
				dialogProcessor.IsDragging = false;
				
				if (hitShape != null) {
					dialogProcessor.AddToSelection(hitShape);
					UpdateSelectionStatus();
					viewPort.Invalidate();
				}
				
				return;
			}
			
			if (hitShape != null) {
				if (dialogProcessor.SelectedShapes.Count > 1 && dialogProcessor.IsShapeSelected(hitShape)) {
					dialogProcessor.AddToSelection(hitShape);
					UpdateSelectionStatus();
				} else {
					dialogProcessor.Selection = hitShape;
					UpdateSelectionStatus();
				}
				
				dialogProcessor.IsDragging = true;
				dialogProcessor.LastLocation = e.Location;
			} else {
				bool hadSelection = dialogProcessor.SelectedShapes.Count > 0;
				dialogProcessor.ClearSelection();
				dialogProcessor.IsDragging = false;
				
				if (hadSelection) {
					UpdateStatus("Селекцията е изчистена.");
				}
			}
			
			viewPort.Invalidate();
		}

		/// <summary>
		/// Moves the selected shape or shapes while dragging.
		/// </summary>
		void ViewPortMouseMove(object sender, MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.SelectedShapes.Count > 1) {
					UpdateStatus("Преместване на селекция.");
				} else if (dialogProcessor.Selection != null) {
					UpdateStatus("Преместване на фигура.");
				}
				
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Finishes the current drag operation.
		/// </summary>
		void ViewPortMouseUp(object sender, MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}
		
		private void AddRectangle()
		{
			dialogProcessor.AddRandomRectangle();
			UpdateStatusAndRefresh("Добавен е правоъгълник.");
		}
		
		private bool EnsureSelection(string actionDescription)
		{
			if (dialogProcessor.SelectedShapes.Count > 0) {
				return true;
			}
			
			UpdateStatusAndRefresh("Няма избрани фигури за " + actionDescription + ".");
			return false;
		}
		
		private void ShowOperationError(string title, string statusMessage, Exception exception)
		{
			MessageBox.Show(
				statusMessage + Environment.NewLine + exception.Message,
				title,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
			
			UpdateStatusAndRefresh(statusMessage);
		}
		
		private void UpdateSelectionStatus()
		{
			int selectedCount = dialogProcessor.SelectedShapes.Count;
			
			if (selectedCount <= 0) {
				UpdateStatus("Няма избрани фигури.");
			} else if (selectedCount == 1) {
				UpdateStatus("Избрана е 1 фигура.");
			} else {
				UpdateStatus(string.Format("Избрани са {0} фигури.", selectedCount));
			}
		}
		
		private void UpdateStatus(string statusText)
		{
			currentStatusLabel.Text = "Последно действие: " + statusText;
		}
		
		private void UpdateStatusAndRefresh(string statusText)
		{
			UpdateStatus(statusText);
			viewPort.Invalidate();
		}
	}
}
