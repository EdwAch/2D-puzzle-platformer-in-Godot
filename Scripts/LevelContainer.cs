using Godot;
using System;

public partial class LevelContainer : Node2D{
	public override void _Ready() {
		LevelManager.Instance.RegisterContainer(this);
	}
}
