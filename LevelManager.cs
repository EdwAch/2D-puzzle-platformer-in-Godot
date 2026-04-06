using Godot;
using System;

public partial class LevelManager : Node {
	public void SwitchLevel(PackedScene levelPath) {
		GetTree().ChangeSceneToPacked(levelPath);
	}
}