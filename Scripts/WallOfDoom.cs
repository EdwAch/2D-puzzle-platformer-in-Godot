using Godot;
using System;

public partial class WallOfDoom : Area2D {
	
	[Export] private int _wallSpeed;
	[Export] private bool _horizontal;
	private Vector2 _movementVector = Vector2.Zero;
	private bool _start = false;
	public override void _Ready() {
		BodyEntered += OnBodyEntered;
		if (_horizontal) {
			_movementVector.X = _wallSpeed;
		} else {
			_movementVector.Y = _wallSpeed;
		}
	}

	public override void _Process(double delta) {
		if (_start) {
			Tween tween = CreateTween();
			tween.TweenProperty(this, "position", Position + _movementVector, 1);
		}
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController) {
			_start = true;
		}
	}
}