using Godot;
using System;

public partial class WindArea : Area2D {
	
	[Export] private int _windForce = -900;
	[Export] private float _maxSpeedFromWind = 0f;
	public override void _Ready() {
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			playerController.ChangeWindForce(_windForce, _maxSpeedFromWind);
		}
	}

	private void OnBodyExited(Node2D body) {
		if (body is PlayerController playerController) {
			playerController.ChangeWindForce(0, 0);
		}
	}
}