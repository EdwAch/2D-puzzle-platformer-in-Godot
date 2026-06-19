using Godot;
using System;

public partial class SideScrollerCamera : Camera2D {
	
	public static SideScrollerCamera Instance { get; private set; }
	[Export] private Marker2D _cameraEndpoint;
	private bool _enabled = false;
	private Tween _tween;

    public override void _Ready() {
		Instance = this;
    }

	public void ToggleScrolling(bool value) {
		_enabled = value;
		if (value) {
			_tween = CreateTween();
			_tween.TweenProperty(this, "position", _cameraEndpoint.Position, 10);
		} else {
			_tween.Kill();
		}
	}
}