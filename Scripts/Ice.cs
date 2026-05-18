using Godot;
using System;

public partial class Ice : Area2D {
	
	public override void _Ready() {
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			playerController.ChangeFrictionModifier();
			playerController.ChangeAcceleration();
		}
	}

	private void OnBodyExited(Node2D body) {
		if (body is PlayerController playerController) {
			playerController.ChangeFrictionModifier();
			playerController.ChangeAcceleration();
		}
	}
}
