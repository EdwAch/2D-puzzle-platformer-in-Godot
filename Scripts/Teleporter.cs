using Godot;
using System;

public partial class Teleporter : Area2D {
	
	[Export] private Marker2D _location;
	public override void _Ready() {
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			playerController.MovePlayerToLocation(_location);
		}
	}
}