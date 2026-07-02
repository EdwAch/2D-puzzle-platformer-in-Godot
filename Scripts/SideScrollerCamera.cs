using Godot;
using System;

public partial class SideScrollerCamera : Camera2D {
	
	public static SideScrollerCamera Instance { get; private set; }
	[Export] private Marker2D _cameraEndpoint;
	[Export] private double _duration = 10;
	private bool _enabled = false;
	private Tween _tween;

	[Export] private MeshInstance2D _mesh;
	[Export] private CollisionShape2D _col;

    public override void _Ready() {
		Instance = this;
		_mesh.Hide();
		_col.CallDeferred("set_disabled", true);
    }

	public void ToggleScrolling(bool value) {
		_enabled = value;
		if (value) {
			_mesh.Show();
			_col.CallDeferred("set_disabled", false);
			_tween = CreateTween();
			_tween.TweenProperty(this, "position", _cameraEndpoint.Position, _duration);
		} else {
			_tween.Kill();
		}
	}
}