using Godot;
using System;

public partial class LobbyDoor : Area2D {

	[Export] private int _levelNumber;
	[Export] private RichTextLabel _textLabel;
	private Tween _tween;
	private bool _inside = false;
	
	public override void _Ready() {
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
		_textLabel.Text = $"Press E to enter level {_levelNumber}";
	}

	private void OnBodyEntered(Node2D body) {
		if (body is PlayerController) {
			_inside = true;
			_textLabel.Visible = true;
			_textLabel.Modulate = new Color(1, 1, 1, 0);
			_tween?.Kill();
			_tween = CreateTween();
			_tween.TweenProperty(_textLabel, "modulate:a", 1, 0.3);
		}
	}

	private void OnBodyExited(Node2D body) {
		if (body is PlayerController) {
			_inside = false;
			_tween?.Kill();
			_tween = CreateTween();
			_tween.TweenProperty(_textLabel, "modulate:a", 0, 0.3);
			_tween.TweenCallback(Callable.From(() => _textLabel.Visible = false));
		}
	}

    public override void _PhysicsProcess(double delta) {
        if (_inside && Input.IsActionPressed("Interact")) {
			GameManager.Instance.GoToLevel(_levelNumber);
			UI.Instance.ShowHUD();
		}
    }
}