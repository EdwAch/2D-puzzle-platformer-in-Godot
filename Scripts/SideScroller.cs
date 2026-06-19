using Godot;
using System;

public partial class SideScroller : Area2D {
	
	[Export] private Camera2D _camera;
	public override void _Ready() {
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			_camera.Enabled = true;
			playerController.ToggleCamera();
			SideScrollerCamera.Instance.ToggleScrolling(true);
		}
	}

	private void OnBodyExited(Node2D body) {
		if (body is PlayerController playerController) {
			_camera.Enabled = false;
			playerController.ToggleCamera();
			SideScrollerCamera.Instance.ToggleScrolling(false);
		}
	}
}