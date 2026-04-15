using Godot;
using System;

public partial class GameManager : Node2D {

	[Export] private PackedScene firstLevel;
	public override void _Ready() {
		LevelManager.Instance.LoadLevel(firstLevel);
	}
}