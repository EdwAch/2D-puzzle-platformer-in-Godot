using Godot;
using System;

public partial class DisappearBackground : Area2D {

	[Export] private MeshInstance2D _background;
    public override void _Ready() {
        BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
    }

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			_background.Hide();
		}
	}
	private void OnBodyExited(Node2D body) {
		if (body is PlayerController playerController) {
			_background.Show();
		}
	}
}
