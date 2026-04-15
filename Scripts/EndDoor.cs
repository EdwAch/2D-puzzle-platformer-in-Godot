using Godot;
using System;

public partial class EndDoor : Area2D {

	[Export] private PackedScene nextLevel;

	public override void _Ready() {
		BodyEntered += OnBodyEntered;
	}
	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			LevelManager.Instance.LoadLevel(nextLevel);
		}
	}
}