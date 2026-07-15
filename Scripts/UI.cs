using Godot;
using System;

public partial class UI : CanvasLayer {
	public static UI Instance { get; private set; }
	[Export] private RichTextLabel statsText;
	[Export] private MarginContainer endUI;
	[Export] private Button endButton;
	[Export] private Button lobbyButton;
	[Export] private RichTextLabel endTypeText;
	[Export] private RichTextLabel endStatsText;
	[Export] private MarginContainer HUD;

	private bool _restartLevel;
	private int _score;

    public override void _Ready() {
        Instance = this;
		endButton.Pressed += ButtonPressed;
		lobbyButton.Pressed += LobbyButtonPressed;
		HideEndUI();
    }
	public void UpdateStats(int score, int time) {
		statsText.Text = $"{score}\n{time}";
	}

	public void UpdateEndStats(int score, int time) {
		endStatsText.Text = $"{score}\n{time}";
	}

	private void ButtonPressed() {
		if (_restartLevel) {
			GameManager.Instance.ReloadLevel();
		} else {
			GameManager.Instance.GoToNextLevel();
		}
		HideEndUI();
	}

	private void LobbyButtonPressed() {
		GameManager.Instance.GoToLevel(0);
		HideEndUI();
	}
	
	public void ShowEndUI(bool survived) {
		lobbyButton.Text ="Return to Lobby";
		if (survived) {
			endButton.Text = "Next Level";
			endTypeText.Text = "[b]LEVEL COMPLETE!";
			_restartLevel = false;
		} else {
			endTypeText.Text = "LEVEL FAILED!";
			endButton.Text = "Retry";
			_restartLevel = true;
		}
		endUI.Show();
	}

	public void HideEndUI() {
		endUI.Hide();
	}

	public void ShowHUD() {
		HUD.Show();
	}

	public void HideHUD() {
		HUD.Hide();
	}
}