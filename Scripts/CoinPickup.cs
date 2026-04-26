using Godot;
using System;

public partial class CoinPickup : Area2D {

	public override void _Ready() {
        BodyEntered += OnBodyEntered;
    }

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController) {
			GameManager.Instance.AddScore(500);
			this.QueueFree();
		}
	}
}