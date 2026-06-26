using Godot;
using System;

public partial class AlternatingPlatforms : Node2D {
	
	[Export] private Timer _timer;
	[Export] private MeshInstance2D[] _greenMeshInstance2DList;
	[Export] private MeshInstance2D[] _purpleMeshInstance2DList;
	[Export] private CollisionShape2D[] _greenCollisionShape2DList;
	[Export] private CollisionShape2D[] _purpleCollisionShape2DList;
	private float _timerTime;
	private int _loops;
	private bool _greenHidden = false;
	public override void _Ready() {
		_timer.Timeout += OnTimerTimeout;
		
		StartBlink(_greenMeshInstance2DList);
		foreach (MeshInstance2D mesh in _purpleMeshInstance2DList) mesh.Hide();
		foreach (CollisionShape2D col in _purpleCollisionShape2DList) col.CallDeferred("set_disabled", true);
	}

	private void StartBlink(MeshInstance2D[] list) {
		Tween tween = CreateTween().SetLoops();
		tween.SetParallel(true);
		foreach (MeshInstance2D mesh in list) {
			tween.TweenProperty(mesh, "modulate:a", 0.3f, 1);
		}
		tween.SetParallel(false);
		tween.TweenInterval(0);
		tween.SetParallel(true);
		foreach (MeshInstance2D mesh in list) {
			tween.TweenProperty(mesh, "modulate:a", 1f, 1);
		}
		tween.SetParallel(false);
		tween.TweenInterval(0);
	}

	private void SetGroupVisible(MeshInstance2D[] meshes, CollisionShape2D[] cols, bool visible) {
		foreach (MeshInstance2D mesh in meshes) {
			if (visible) {
				mesh.Show();
			} else {
				mesh.Hide();
			}
		}
		foreach (CollisionShape2D col in cols) {
			col.CallDeferred("set_disabled", !visible);
		}
	}

	private void OnTimerTimeout() {
		if (_greenHidden) {
			SetGroupVisible(_greenMeshInstance2DList, _greenCollisionShape2DList, true);
			SetGroupVisible(_purpleMeshInstance2DList, _purpleCollisionShape2DList, false);
			StartBlink(_greenMeshInstance2DList);
			_greenHidden = false;
		} else {
			SetGroupVisible(_purpleMeshInstance2DList, _purpleCollisionShape2DList, true);
			SetGroupVisible(_greenMeshInstance2DList, _greenCollisionShape2DList, false);
			StartBlink(_purpleMeshInstance2DList);
			_greenHidden = true;
		}
	}
}