using Godot;
using System;

public partial class GravityChanger : Area2D {
	
	private bool _isRotating = false;
	private bool _isInside = false;
	public override void _Ready() {
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			if (_isRotating || _isInside) return;
			_isInside = true;
			_isRotating = true;
			playerController.InvertGravity();

			SetDeferred("monitoring", false);
			Tween tween = CreateTween();
			tween.TweenProperty(body, "rotation", body.Rotation + Mathf.Pi, 0.3f);
			tween.TweenCallback(Callable.From(() => {
				_isRotating = false;
				SetDeferred("monitoring", true);
			}));
		}
	}
	private void OnBodyExited(Node2D body) {
		if (body is PlayerController playerController) {
			if (_isRotating || !_isInside) return;
			_isInside = false;
			_isRotating = true;
			playerController.InvertGravity();

			SetDeferred("monitoring", false);
			Tween tween = CreateTween();
			tween.TweenProperty(body, "rotation", body.Rotation + Mathf.Pi, 0.3f);
			tween.TweenCallback(Callable.From(() => {
				_isRotating = false;
				SetDeferred("monitoring", true);
			}));
		}
	}
}