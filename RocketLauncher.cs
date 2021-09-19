using Godot;
using System;

public class RocketLauncher : Sprite
{
    
    private PackedScene explosion;
    public override void _Ready()
    {
        explosion = GD.Load<PackedScene>("res://Rocket.tscn");
    }

    public void AimAt(Vector2 AimPosition){
        LookAt(AimPosition);
    }
    public void AddExplosion(Vector2 ExplosionPosition)
    {
        Rocket exp = (Rocket)explosion.Instance();
        
        exp.GlobalPosition = GlobalPosition;

        exp.Rotation = GlobalRotation;

        Globals sexo = (Globals)GetTree().Root.GetNode("Globals");
        
        sexo.TheWorld.AddChild( (Node)exp );
    

    }


}
