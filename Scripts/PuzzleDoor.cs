using Godot;

public partial class PuzzleDoor : StaticBody2D {
    
    [Export] private bool startsActive = true;
    [Export] private CollisionShape2D doorCollisionShape2D;

    public override void _Ready() {
        if (startsActive) {
            DoorDisabled();
        } else {
            DoorEnabled();
        }
    }

    public void ToggleDoorState() {
        if (this.IsVisibleInTree()) {
            DoorEnabled();
        } else {
            DoorDisabled();
        }
    }

    private void DoorEnabled() {
        this.Hide();
        doorCollisionShape2D.CallDeferred("set_disabled", true);
    }

    private void DoorDisabled() {
        this.Show();
        doorCollisionShape2D.CallDeferred("set_disabled", false);
    }
}