using Godot;

public partial class ButtonController : Area2D {
    
    [Export] private PuzzleDoor puzzleDoor;
    public override void _Ready() {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body) {
        if (body is PlayerController playerController) {
            ButtonPressed();
        }
    }

    private void ButtonPressed() {
        puzzleDoor.ToggleDoorState();
    }
}