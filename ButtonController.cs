using Godot;

public partial class ButtonController : Area2D {
    
    [Export] private PuzzleDoor[] puzzleDoor;
    [Export] private MeshInstance2D buttonMeshInstance;

    private Vector2 buttonMovementOnPress = new Vector2(0f, 2f);
    public override void _Ready() {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node2D body) {
        if (body is PlayerController playerController) {
            ButtonPressed();
        }
    }

    private void OnBodyExited(Node2D body) {
        if (body is PlayerController playerController) {
            ButtonReleased();
        }
    }

    private void ButtonPressed() {
        buttonMeshInstance.Translate(buttonMovementOnPress);
        foreach (PuzzleDoor puzzleDoor in puzzleDoor) {
            puzzleDoor.ToggleDoorState();    
        } 
    }

    private void ButtonReleased() {
        buttonMeshInstance.Translate(-buttonMovementOnPress);
    }
}