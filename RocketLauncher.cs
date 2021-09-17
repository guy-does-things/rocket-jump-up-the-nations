using Godot;
using System;

public class RocketLauncher : Sprite
{
    
    private PackedScene explosion;
    public override void _Ready()
    {
        explosion = GD.Load<PackedScene>("res://Explosion.tscn");
    }

    public void AimAt(Vector2 AimPosition){
        LookAt(AimPosition);
    }
    public void AddExplosion(Vector2 ExplosionPosition)
    {
        Explosion exp = (Explosion)explosion.Instance();
        
        exp.GlobalPosition = ExplosionPosition;

        Globals sexo = (Globals)GetTree().Root.GetNode("Globals");
        
        sexo.TheWorld.AddChild( (Node)exp );
    

    }


}
