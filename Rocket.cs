using Godot;
using System;

public class Rocket : KinematicBody2D
{
    [Export]
    private float MoveSpeed = 500;

    private PackedScene Explosion;
    public override void _Ready()
    
    {
        Explosion = GD.Load<PackedScene>("res://Explosion.tscn");
        GetNode<AnimationPlayer>("AnimationPlayer").Play("flip");

    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 Velocity = Vector2.Right.Rotated(Rotation) * MoveSpeed;

        KinematicCollision2D Collision =  MoveAndCollide(Velocity * delta);

        if (Collision != null)
        {
            Explosion exp = Explosion.Instance() as Explosion;
            exp.GlobalPosition = GlobalPosition;
            
            GetTree().Root.AddChild(exp);
            QueueFree();
        }

    }   
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
