using Godot;
using System;

public partial class GameManager : Node2D {

	public static GameManager Instance { get; private set; }

	[Export] private PackedScene[] levelList;
	private static int _levelNumber = 0;

	private int _score;
	private bool _levelEnded;
	public override void _Ready() {
		Instance = this;
		LevelManager.Instance.LoadLevel(levelList[0]);
	}

	public void GoToNextLevel() {
		if (_levelNumber + 1 >= levelList.Length) {
			_levelNumber = 0;
			LevelManager.Instance.CallDeferred(nameof(LevelManager.LoadLevel), levelList[_levelNumber]);
			LevelStarted();
			_score = 0;
			//Delete this if but leave the else when all levels are actually done
		} else {
			_levelNumber++;
			LevelManager.Instance.CallDeferred(nameof(LevelManager.LoadLevel), levelList[_levelNumber]);
			LevelStarted();
			_score = 0;
		}
	}

	public void GoToLevel(int value) {
		_levelNumber = value;
		LevelManager.Instance.CallDeferred(nameof(LevelManager.LoadLevel), levelList[_levelNumber]);
		LevelStarted();
		_score = 0;
	}

	public void ReloadLevel() {
		LevelManager.Instance.CallDeferred(nameof(LevelManager.LoadLevel), levelList[_levelNumber]);
		LevelStarted();
		_score = 0;
	}

	public void AddScore(int addScoreAmount) {
		_score += addScoreAmount;
	}

	public int GetScore() {
		return _score;
	}

	public void LevelEnded() {
		_levelEnded = true;
	}

	public void LevelStarted() {
		_levelEnded = false;
	}

	public bool DidLevelEnd() {
		if (_levelEnded) {
			return true;
		} else {
			return false;
		}
	}
}