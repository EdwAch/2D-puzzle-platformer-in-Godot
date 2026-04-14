using Godot;
using System;

public partial class LevelManager : Node {

	public void LoadFirstLevel(PackedScene firstLevelPath) {
		var firstLevel = firstLevelPath.Instantiate();
		var currentScene = GetTree().CurrentScene;
		var container = currentScene.GetNode<Node2D>("LevelContainer");
		var player = currentScene.GetNode<CharacterBody2D>("CharacterBody2D");
		var spawn = firstLevel.GetNode<Marker2D>("Spawnpoint");
		
		container.AddChild(firstLevel);
		if (spawn != null) {
			player.GlobalPosition = spawn.GlobalPosition;
		}
	}
	public void SwitchLevel(PackedScene levelPath) {
		var currentScene = GetTree().CurrentScene;
		var container = currentScene.GetNode<Node2D>("LevelContainer");
		var player = currentScene.GetNode<CharacterBody2D>("CharacterBody2D");
		var newLevel = levelPath.Instantiate();
		var spawn = newLevel.GetNode<Marker2D>("Spawnpoint");
		
		foreach (Node child in container.GetChildren()) {
			child.QueueFree();
		}
		container.AddChild(newLevel);
		if (spawn != null) {
			player.GlobalPosition = spawn.GlobalPosition;
		}
	}
}