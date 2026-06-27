using Godot;
using System;

public partial class JumpingAlternatingPlatforms : Node2D {

	[Export] private Timer _timer;
	[Export] private MeshInstance2D[] _orangeMeshInstance2DList;
	[Export] private MeshInstance2D[] _yellowMeshInstance2DList;
	[Export] private CollisionShape2D[] _orangeCollisionShape2DList;
	[Export] private CollisionShape2D[] _yellowCollisionShape2DList;
	private float _timerTime;
	private bool _orangeHidden = false;
	private Tween _tween;
	public override void _Ready() {
		_timer.Timeout += OnTimerTimeout;
		_timerTime = (float)_timer.WaitTime;
		_tween = CreateTween();
		ChangePlatformVisibility();
		foreach (CollisionShape2D collision in _orangeCollisionShape2DList) {
			collision.CallDeferred("set_disabled", true);
		}
		_orangeHidden = true;
	}

    public override void _PhysicsProcess(double delta) {
        if (Input.IsActionJustPressed("Jump") && PlayerController.Instance.WasGrounded()) {
			if (_orangeHidden) {
				_timer.Start();
				foreach (CollisionShape2D collision in _orangeCollisionShape2DList) {
					collision.CallDeferred("set_disabled", false);
				}
				ChangePlatformVisibility();
			} else {
				_timer.Start();
				foreach (CollisionShape2D collision in _yellowCollisionShape2DList) {
					collision.CallDeferred("set_disabled", false);
				}
				ChangePlatformVisibility();
			}
		}
    }

	private void OnTimerTimeout() {
		if (_orangeHidden) {
			foreach (CollisionShape2D collision in _yellowCollisionShape2DList) {
				collision.CallDeferred("set_disabled", true);
			}
			_orangeHidden = false;
		} else {
			foreach (CollisionShape2D collision in _orangeCollisionShape2DList) {
				collision.CallDeferred("set_disabled", true);
			}
			_orangeHidden = true;
		}
	}

	private void ChangePlatformVisibility() {
		_tween?.Kill();
		_tween = CreateTween().SetParallel();
		if (_orangeHidden) {
			foreach (MeshInstance2D mesh in _orangeMeshInstance2DList) {
				_tween.TweenProperty(mesh, "modulate:a", 1, _timerTime);
			}
			foreach (MeshInstance2D mesh in _yellowMeshInstance2DList) {
				_tween.TweenProperty(mesh, "modulate:a", 0, _timerTime);
			}
		} else {
			foreach (MeshInstance2D mesh in _yellowMeshInstance2DList) {
				_tween.TweenProperty(mesh, "modulate:a", 1, _timerTime);
			}
			foreach (MeshInstance2D mesh in _orangeMeshInstance2DList) {
				_tween.TweenProperty(mesh, "modulate:a", 0, _timerTime);
			}
		}
	}
}