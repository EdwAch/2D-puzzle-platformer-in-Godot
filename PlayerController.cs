using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export] private float speed = 300f; 
	[Export] private float gravity = 400f;
	[Export] private float jumpSpeed = -200f;

	public override void _PhysicsProcess(double delta) {
		Vector2 currentVelocity = Velocity;
		currentVelocity.Y += gravity * (float)delta;

		float direction = Input.GetAxis("Left", "Right");

		if (direction !=0) {
			currentVelocity.X = direction * speed;
		} else {
			currentVelocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
		}

		if (Input.IsActionJustPressed("Jump")) {
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
	
}
