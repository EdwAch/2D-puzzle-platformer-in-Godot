using Godot;
using System;

public partial class MovingDeadlyPlatform : Node2D {
	
	[Export] private Marker2D _startingLocation;
	[Export] private Marker2D _endingLocation;
	[Export] private Timer _timer;
	[Export] private Area2D _deadlyPlatform;
	private float _timerTime;
	private bool _atStart;
	private Tween _tween;
	public override void _Ready() {
		_timerTime = (float)_timer.WaitTime;
		Tween tween = CreateTween();
		tween.TweenProperty(_deadlyPlatform, "position", _endingLocation.Position, _timerTime);
		_timer.Timeout += OnTimerTimeout;
	}
	
	private void OnTimerTimeout() {
		if (_atStart) {
			_tween?.Kill();
			_tween = CreateTween();
			_tween.TweenProperty(_deadlyPlatform, "position", _endingLocation.Position, _timerTime);
			_atStart = false;
		} else {
			_tween?.Kill();
			_tween = CreateTween();
			_tween.TweenProperty(_deadlyPlatform, "position", _startingLocation.Position, _timerTime);
			_atStart = true;
		}
	}
}