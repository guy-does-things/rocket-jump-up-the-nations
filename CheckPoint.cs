using Godot;
using System;

public class CheckPoint : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("body_entered", this, "plrentered");
	}

	public void plrentered(Godot.Object body)
	{
		Player plr = body as Player;

		if (plr != null)
		{
			plr.SetCheckPoint(GlobalPosition);
		}


	}


}
