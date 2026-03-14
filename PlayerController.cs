using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export] private float speed = 300f; 
	[Export] private float gravity = 400f;
	[Export] private float jumpSpeed = -200f;
	[Export] private ShapeCast2D groundChecker;

	private float friction = 35f;
	// 35 is good for normal ground, less is more time to reach 0

	public override void _PhysicsProcess(double delta) {
		Vector2 currentVelocity = Velocity;
		
		if (!IsGrounded()) {
			currentVelocity.Y += gravity * (float)delta;	
		} else if (currentVelocity.Y > 0) {
			currentVelocity.Y = 0;
		}


		float direction = Input.GetAxis("Left", "Right");

		if (direction !=0) {
			currentVelocity.X = direction * speed;
		} else if (IsGrounded()) {
			currentVelocity.X = Mathf.MoveToward(Velocity.X, 0, friction);
		}

		if (IsGrounded() && Input.IsActionJustPressed("Jump")) {
			currentVelocity.Y = jumpSpeed;
		}

		if (Input.IsActionJustReleased("Jump")) {
			if (currentVelocity.Y < 0) {
				currentVelocity.Y = currentVelocity.Y * 0.5f;
			}
		}
		
		Velocity = currentVelocity;

		MoveAndSlide();
	}

	private bool IsGrounded() {
		groundChecker.ForceShapecastUpdate();
		return groundChecker.IsColliding();
	}
	
}
