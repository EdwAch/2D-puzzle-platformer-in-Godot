using Godot;
using System;

public partial class GameManager : Node2D {

	public static GameManager Instance { get; private set; }

	[Export] private PackedScene[] levelList;
	private static int levelNumber = 0;
	public override void _Ready() {
		Instance = this;
		LevelManager.Instance.LoadLevel(levelList[0]);
	}

	public void GoToNextLevel() {
		foreach (PackedScene level in levelList) {
			int index = Array.IndexOf(levelList, level);
			if (index > levelNumber) {
				LevelManager.Instance.CallDeferred(nameof(LevelManager.LoadLevel), level);
				levelNumber++;
			}
		}
	}

	public void ReloadLevel() {
		LevelManager.Instance.CallDeferred(nameof(LevelManager.LoadLevel), levelList[levelNumber]);
	}
}