using Godot;
using System;

public class Explosion : Area2D
{

    private Player TheEntity;
    private CPUParticles2D particle_fx; 


    public override void _Ready()
    {
        
        particle_fx = GetNode<CPUParticles2D>("CPUParticles2D");
        particle_fx.Emitting = true;
        Connect("body_exited", this, "PlrExited");
    }


    private Vector2 KnockBack(Vector2 ThingPosition){
        CircleShape2D hitbox = GetNode<CollisionShape2D>("CollisionShape2D").Shape as CircleShape2D;


        float KnockbackDistance = GlobalPosition.DistanceTo(ThingPosition) + hitbox.Radius;

        Vector2 KnockbackDirection = GlobalPosition.DirectionTo(ThingPosition).Normalized();

        return KnockbackDirection * KnockbackDistance;
    }

    public override void _Process(float delta)
    {
        foreach (Godot.Object body  in GetOverlappingBodies())
        {
            OnBodyEntered(body as PhysicsBody2D);
        }

        if (!particle_fx.Emitting)
        {
            QueueFree();
        }
    }

    public void PlrExited(Godot.Object body)
    {
        Player TryToMatchPlr = body as Player;
       
        if ( TryToMatchPlr != null )
        {
            if (TryToMatchPlr == TheEntity)
            {  
                TryToMatchPlr.IsAbleToJump = false;
            }
            
        }
    }
    public void OnBodyEntered(PhysicsBody2D something){
        
        TheEntity = something as Player;

        

        if(TheEntity != null){
            TheEntity.IsAbleToJump = true;

            Vector2 KnockbackSpeed = KnockBack(TheEntity.GlobalPosition);
            Vector2 oldvel = TheEntity.Velocity;
            TheEntity.Velocity += KnockbackSpeed;

            
        }
    
    }


}
