using Godot;
using System;

public partial class WindArea : Area2D {
	
	[Export] private int _windForce = -900;
	public override void _Ready() {
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			playerController.ChangeWindForce(_windForce);
		}
	}

	private void OnBodyExited(Node2D body) {
		if (body is PlayerController playerController) {
			playerController.ChangeWindForce(0);
		}
	}
}