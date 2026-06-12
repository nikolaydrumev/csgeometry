# Draw WinForms Editor

## Project title

Draw WinForms Editor

## Short description

This project is a Windows Forms drawing editor written in C#. It demonstrates object-oriented modeling of graphical primitives, user interaction through WinForms, basic geometric transformations, grouping, and JSON save/load of the drawing state.

## Implemented features

- Add random rectangle, ellipse, and line shapes
- Single selection and multi-selection with `Ctrl + click`
- Drag and drop translation of selected shapes
- Fill color and stroke color changes
- Stroke width support
- Blue dashed selection visualization
- Rotation and scaling of selected shapes
- Grouping and ungrouping of shapes
- Save project to JSON
- Load project from JSON

## Architecture overview

The project keeps a small and defense-friendly three-layer structure:

- `Model`
  Stores the shape hierarchy and transformation logic.
- `Processors`
  Handles rendering, selection, drag operations, grouping, and JSON serialization.
- `GUI`
  Contains the Windows Forms interface, menu actions, dialogs, status bar, and viewport events.

This is intentionally not a large framework-based architecture. The goal is to keep the code easy to follow during a university presentation.

## How to run the project

1. Open `Draw.sln` or `Draw.csproj` in Visual Studio.
2. Build the project.
3. Start the application with `F5` or `Ctrl + F5`.

The project targets Windows Forms and is intended to run on Windows.

## How to use the editor

- `Image -> Add Rectangle` adds a rectangle.
- `Image -> Add Ellipse` adds an ellipse.
- `Image -> Add Line` adds a line.
- Activate the selection tool from the toolbar and click a shape to select it.
- Hold `Ctrl` and click multiple shapes for multi-selection.
- Drag selected shapes with the mouse to move them.
- `Edit -> Change Fill Color` changes the fill color of the selected shapes.
- `Edit -> Change Stroke Color` changes the border or line color of the selected shapes.
- `Edit -> Rotate Left/Right` rotates the selected shapes.
- `Edit -> Scale Up/Down` scales the selected shapes.
- `Edit -> Group Selected` groups the selected shapes.
- `Edit -> Ungroup Selected` releases a selected group back into child shapes.
- `Edit -> Delete Selected` removes the selected shapes.
- `File -> Save` stores the drawing as a JSON file.
- `File -> Load` restores a previously saved JSON drawing.

## Project structure

### GUI

- `MainForm.cs`
- `MainForm.Designer.cs`
- `DoubleBufferedPanel.cs`
- `Program.cs`

This layer contains the Windows Forms user interface, menu handlers, status bar updates, file dialogs, and viewport mouse logic.

### Model

- `Shape.cs`
- `RectangleShape.cs`
- `EllipseShape.cs`
- `LineShape.cs`
- `GroupShape.cs`

This layer contains the base shape abstraction and the concrete drawable shapes.

### Processors

- `DisplayProcessor.cs`
- `DialogProcessor.cs`
- `DrawingJsonSerializer.cs`

This layer contains rendering, interaction logic, grouping behavior, and JSON persistence.

## Shapes and transformations

### Shapes

- `RectangleShape` draws a filled rectangle with a stroke.
- `EllipseShape` draws a filled ellipse with a stroke.
- `LineShape` draws a line between two points.
- `GroupShape` stores child shapes and allows them to be manipulated together.

### Transformations

Each shape stores:

- position and size
- fill color
- stroke color
- stroke width
- name
- selection flag
- rotation angle
- scale factor

Rectangle and ellipse rotate and scale around their center. Line also rotates and scales around its center. Translation is performed through mouse dragging.

## Grouping

Multi-selection allows several shapes to be selected at once. When grouped, the selected shapes are wrapped in `GroupShape`. The group can then be moved, rotated, scaled, colored, saved, and loaded as a single top-level object. Ungrouping returns the child shapes back into the main shape list while preserving their visible transformation.

## Save and load

Save/load is implemented with a simple DTO-based JSON serializer. This keeps the code understandable for a university defense and avoids complicated polymorphic serializer configuration.

The saved JSON contains:

- shape type
- rectangle, location, and size data
- fill color
- stroke color
- stroke width
- name
- rotation angle
- scale factor
- line start and end points
- group children

Selection state is not saved as selected. After loading, the current selection is cleared.

## Review notes

- The architecture is simple and suitable for a course project.
- `DialogProcessor` is the main behavior class and is the best place to explain interaction flow during defense.
- Some explicit drawing code is duplicated between rectangle and ellipse on purpose so the implementation stays easy to read.
- `GroupShape` demonstrates the composite pattern in a lightweight way.
- JSON persistence uses DTO classes because it is easier to explain than advanced polymorphic serialization with `Color` and multiple runtime types.

## Known limitations

- Shapes are created at random positions instead of being drawn directly with the mouse.
- `Ctrl + click` adds shapes to the selection but does not toggle them off individually.
- There are no automated unit tests yet; verification is manual.
- Save/load persists the drawing model, not temporary UI state such as the active selection tool.

## Suggested exam presentation explanation in Bulgarian

Това приложение е прост графичен редактор, реализиран с Windows Forms и обектно-ориентиран модел на примитиви. Базовият клас `Shape` описва общите свойства на всички фигури: позиция, размер, цветове, контур, селекция, ротация и мащаб. От него наследяват `RectangleShape`, `EllipseShape`, `LineShape` и `GroupShape`.

Потребителят може да добавя фигури, да ги избира с мишката, да мести избраните елементи чрез drag and drop, да променя цветовете им, както и да прилага ротация и мащабиране. Реализирана е множествена селекция с `Ctrl + click`, което позволява няколко фигури да се групират и после да се обработват като един обект.

Записът и зареждането са реализирани в JSON формат чрез отделен сериализатор с DTO класове. Така се запазват всички важни свойства на фигурите и групите, включително вложени деца в `GroupShape`, без да се усложнява архитектурата. Решението е достатъчно просто за защита и в същото време покрива основните изисквания за графичен редактор.
