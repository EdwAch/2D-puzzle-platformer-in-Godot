using Godot;
using System;

public partial class Level : Node2D {

	private double _levelTime;
	private int _score;
	public override void _Process(double delta) {
		if (!GameManager.Instance.DidLevelEnd()) {
			_levelTime += delta;
			_score = GameManager.Instance.GetScore();
			UI.Instance.UpdateStats(_score, (int)_levelTime);
		} else {
			UI.Instance.UpdateEndStats(_score, (int)_levelTime);
		}
	}
}