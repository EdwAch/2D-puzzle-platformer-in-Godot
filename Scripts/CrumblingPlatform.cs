using Godot;
using System;

public partial class CrumblingPlatform : Area2D {

	[Export] private Timer _timer;
	[Export] private MeshInstance2D _platformMeshInstance2D;
	[Export] private CollisionShape2D _platformCollisionShape2D;
	private Vector2 _originalPosition;
	private Tween _shakeTween;
	private bool _hidden = false;
	public override void _Ready() {
		_originalPosition = _platformMeshInstance2D.Position;
		BodyEntered += OnBodyEntered;
		_timer.Timeout += OnTimerTimeout;
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController playerController && _timer.IsStopped()) {
			_timer.Start();
			_shakeTween = CreateTween().SetLoops();
			_shakeTween.TweenProperty(_platformMeshInstance2D, "position", Position + new Vector2(3, 0), 0.05);
			_shakeTween.TweenProperty(_platformMeshInstance2D, "position", Position - new Vector2(3, 0), 0.05);
		}
	}

	private void OnTimerTimeout() {
		if (_hidden == false) {
			_shakeTween?.Kill();
			Tween reset = CreateTween();
			reset.TweenProperty(_platformMeshInstance2D, "position", _originalPosition, 0.05);
			_platformMeshInstance2D.Hide();
			_platformCollisionShape2D.CallDeferred("set_disabled", true);
			_hidden = true;
			_timer.Start();
		} else {
			_platformMeshInstance2D.Show();
			_platformCollisionShape2D.CallDeferred("set_disabled", false);
			_hidden = false;
		}
	}
}