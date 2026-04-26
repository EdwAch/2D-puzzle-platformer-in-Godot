using Godot;
using System;

public partial class EndDoor : Area2D {

	public override void _Ready() {
		BodyEntered += OnBodyEntered;
	}
	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			playerController.HidePlayer();
			playerController.DisableMovement();
			playerController.MovePlayerToSafety();
			GameManager.Instance.LevelEnded();
			UI.Instance.ShowEndUI(true);
		}
	}
}