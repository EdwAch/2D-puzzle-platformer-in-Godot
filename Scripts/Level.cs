using Godot;
using System;

public partial class Level : Node2D {

	[Export] private bool _isLobby;
	private double _levelTime;
	private int _score;

    public override void _Ready() {
        if (_isLobby) {
			UI.Instance.HideHUD();
		}
    }
	public override void _Process(double delta) {
		if (!_isLobby) {
			if (!GameManager.Instance.DidLevelEnd()) {
				_levelTime += delta;
				_score = GameManager.Instance.GetScore();
				UI.Instance.UpdateStats(_score, (int)_levelTime);
			} else {
				UI.Instance.UpdateEndStats(_score, (int)_levelTime);
			}
		}
	}
}