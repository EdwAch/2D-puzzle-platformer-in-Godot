using Godot;
using System;

public partial class AlternatingPlatforms : Node2D {
	
	[Export] private Timer _timer;
	[Export] private MeshInstance2D[] _blueMeshInstance2DList;
	[Export] private MeshInstance2D[] _purpleMeshInstance2DList;
	[Export] private CollisionShape2D[] _blueCollisionShape2DList;
	[Export] private CollisionShape2D[] _purpleCollisionShape2DList;
	private Tween _tween;
	private bool _blueHidden = false;
	public override void _Ready() {
		_timer.Timeout += OnTimerTimeout;
		_tween = CreateTween().SetLoops(2);
		foreach (MeshInstance2D meshInstance2D in _blueMeshInstance2DList) {
			_tween.TweenProperty(meshInstance2D, "modulate:a", 0.3, 1);
			_tween.TweenProperty(meshInstance2D, "modulate:a", 1, 1);
		}
		foreach (MeshInstance2D meshInstance2D in _purpleMeshInstance2DList) {
			meshInstance2D.Hide();
		}
		foreach (CollisionShape2D collisionShape2D in _purpleCollisionShape2DList) {
			collisionShape2D.CallDeferred("set_disabled", true);
		}
	}

	private void OnTimerTimeout() {
		_tween = CreateTween().SetLoops(2);
		if (_blueHidden) {
			foreach (MeshInstance2D meshInstance2D in _blueMeshInstance2DList) {
				meshInstance2D.Show();
				_tween.TweenProperty(meshInstance2D, "modulate:a", 0.3, 1);
				_tween.TweenProperty(meshInstance2D, "modulate:a", 1, 1);
			}
			foreach (CollisionShape2D collisionShape2D in _blueCollisionShape2DList) {
				collisionShape2D.CallDeferred("set_disabled", false);
			}
			foreach (MeshInstance2D meshInstance2D in _purpleMeshInstance2DList) {
				meshInstance2D.Hide();
			}
			foreach (CollisionShape2D collisionShape2D in _purpleCollisionShape2DList) {
				collisionShape2D.CallDeferred("set_disabled", true);
			}
			_blueHidden = false;
		} else {
			foreach (MeshInstance2D meshInstance2D in _purpleMeshInstance2DList) {
				meshInstance2D.Show();
				_tween.TweenProperty(meshInstance2D, "modulate:a", 0.3, 1);
				_tween.TweenProperty(meshInstance2D, "modulate:a", 1, 1);
			}
			foreach (CollisionShape2D collisionShape2D in _purpleCollisionShape2DList) {
				collisionShape2D.CallDeferred("set_disabled", false);
			}
			foreach (MeshInstance2D meshInstance2D in _blueMeshInstance2DList) {
				meshInstance2D.Hide();
			}
			foreach (CollisionShape2D collisionShape2D in _blueCollisionShape2DList) {
				collisionShape2D.CallDeferred("set_disabled", true);
			}
			_blueHidden = true;
		}
	}
}