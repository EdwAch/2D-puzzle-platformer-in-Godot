using Godot;
using System;

public partial class PlayerController : CharacterBody2D {

	public static PlayerController Instance { get; private set; }
	[Export] private float _speed = 300f; 
	[Export] private float _gravity = 400f;
	[Export] private float _jumpSpeed = -200f;
	[Export] private ShapeCast2D _groundChecker;
	[Export] private Camera2D _playerCamera;
	private bool _canMove = true;
	private bool _doubleJump = false;
	private float _friction = 1200f;
    // 35 is good for normal ground, less is more time to reach 0
	private float _frictionModifier = 1f;
	private float _acceleration = 1500f;
	private float _windForce = 0f;
	// 600 wind force adds +10 to velocity
	private float _maxSpeedFromWind = 0f;
	// 0 means on ground/no limit to speed change from wind, vertical not affected
	private float _currentWindVelocityX = 0f;
	private float _currentWindVelocityY = 0f;
	private bool _verticalWind = false;
	private bool _wasGrounded;

    public override void _Ready() {
        LevelManager.Instance.RegisterPlayer(this);
		Instance = this;
    }
	public override void _PhysicsProcess(double delta) {
		Vector2 currentVelocity = Velocity;
		_wasGrounded = IsGrounded();

		if (!IsGrounded() || _gravity < 0) {
			currentVelocity.Y += _gravity * (float)delta;	
		} else if (currentVelocity.Y > 0) {
			currentVelocity.Y = 0;
		}


		float direction = Input.GetAxis("Left", "Right");
		bool leftDirection = Input.IsActionPressed("Left");
		bool rightDirection = Input.IsActionPressed("Right");

		if (_canMove) {
			float maxVelocityX = direction * _speed;
			if (direction !=0) {
				currentVelocity.X = Mathf.MoveToward(Velocity.X, maxVelocityX, _acceleration * (float)delta);
			} else if (IsGrounded() && currentVelocity.X != 0) {
				currentVelocity.X = Mathf.MoveToward(Velocity.X, 0, _friction * _frictionModifier * (float)delta);
			} else if (!IsGrounded() && leftDirection && rightDirection && currentVelocity.X != 0) {
				currentVelocity.X = Mathf.MoveToward(Velocity.X, maxVelocityX, _acceleration);
			}
			/*else if (!IsGrounded() && currentVelocity.X != 0) {
				currentVelocity.X = Mathf.MoveToward(Velocity.X, 0, 10000f);
			} Uncomment this if dont want movement when no movement key is pressed and midair
			*/
			if (_windForce != 0 && !_verticalWind && _frictionModifier == 1) {
				_currentWindVelocityX = _windForce;
			} else {
				_currentWindVelocityX = 0;
			} 

			if (_windForce != 0 && _verticalWind) {
				_currentWindVelocityY = _windForce;
			} else {
				_currentWindVelocityY = 0;
			}

			if (IsGrounded() && Input.IsActionJustPressed("Jump")) {
				currentVelocity.Y = _jumpSpeed;
				_doubleJump = !_doubleJump;
			}
			if (Input.IsActionJustReleased("Jump") && _jumpSpeed < 0 && currentVelocity.Y < 0) {
					currentVelocity.Y = currentVelocity.Y * 0.5f;
			}
			if (!IsGrounded() && Input.IsActionJustPressed("Jump") && _doubleJump) {
				currentVelocity.Y = _jumpSpeed;
				_doubleJump = !_doubleJump;
			}
		} else {
			currentVelocity.X = 0;
			_currentWindVelocityX = 0;
		}
		
		if (_maxSpeedFromWind != 0f && !IsGrounded() && Mathf.Abs(_currentWindVelocityX) > _maxSpeedFromWind) {
			if (leftDirection || rightDirection) {
				currentVelocity.X += Mathf.Sign(_currentWindVelocityX) * _maxSpeedFromWind;
			} else {
				_currentWindVelocityX = Mathf.Sign(_currentWindVelocityX) * _maxSpeedFromWind;
				currentVelocity.X += _currentWindVelocityX * (float)delta;
			}
		} else {
			currentVelocity.X += _currentWindVelocityX * (float)delta;
		}
		currentVelocity.Y += _currentWindVelocityY * (float)delta;

		Velocity = currentVelocity;
		MoveAndSlide();
	}

	private bool IsGrounded() {
		_groundChecker.ForceShapecastUpdate();
		return _groundChecker.IsColliding();
	}

	public bool WasGrounded() {
		return _wasGrounded;
	}

	public void ChangeFrictionModifier() {
		if (_frictionModifier != 1) {
			_frictionModifier = 1f;
		} else {
			_frictionModifier = 0.15f;
		}
	}

	public void ChangeAcceleration() {
		if (_acceleration != 1500) {
			_acceleration = 1500f;
		} else {
			_acceleration = 250f;
		}
	}

	public void ChangeWindForce(int value, float value2) {
		_windForce = value;
		_maxSpeedFromWind = value2;
	}
	
	public void ChangeWindDirection() {
		_verticalWind = !_verticalWind;
	}

	public void MovePlayerToSafety() {
		GlobalPosition = Vector2.Zero;
		_gravity = 0f;
	}

	public void MovePlayerToLocation(Marker2D location) {
		GlobalPosition = location.GlobalPosition;
	}

	public void InvertGravity() {
		_gravity = -_gravity;
		_jumpSpeed = -_jumpSpeed;
	}

	public void ToggleCamera() {
		if (_playerCamera.Enabled) {
			_playerCamera.Enabled = false;
		} else {
			_playerCamera.Enabled = true;
		}
	}
	
	public void HidePlayer() {
		this.Hide();
	}

	public void ShowPlayer() {
		this.Show();
	}

	public void EnableMovement() {
		_canMove = true;
		_gravity = 500f;
	}

	public void DisableMovement() {
		_canMove = false;
	}
}