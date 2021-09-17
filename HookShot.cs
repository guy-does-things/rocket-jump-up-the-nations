using Godot;
using System;

public class HookShot : RayCast2D
{
    private GrapplePoint Collision = null;

    public bool Hooked = false;

    private Line2D line;

    public override void _Ready()
    {
        line = GetNode<Line2D>("Node/Line2D");
    }

    public void UnHook()
    {
        Hooked = false;
        Collision = null;
        line.Points = new Vector2[2];
    
    }

    

    public void Hook(Vector2 HookPos )
    {

        

        if (!Hooked)
        {
            CastTo = new Vector2(5000, 0).Rotated(HookPos.Angle());

            Godot.Object TestCollider = GetCollider();

            if(TestCollider != null)
            {
                line.Points = new Vector2[2] {GlobalPosition,GetCollisionPoint()};
            }
            else
            {
                line.Points = new Vector2[2] {GlobalPosition, CastTo};
            }
            
            
            GrapplePoint Collider = GetCollider() as GrapplePoint;

            if (Collider != null)
            {
                Collision = Collider;
                Hooked = true;
            }

        }

    }


    public Vector2 GetMovementDir(Vector2 thingPosition, float MoveSpeed)
    {

        if(Collision != null)
        {
            CastTo = ToLocal(Collision.GlobalPosition);

            if (GetCollider() as GrapplePoint == Collision )
            {
                Vector2[] points = new Vector2[2] {thingPosition, GetCollisionPoint()};
                line.Points = points;
                return thingPosition.DirectionTo(Collision.GlobalPosition) * MoveSpeed;
            }
            else
            {
                line.Points = new Vector2[2];
                UnHook();
            }
        }   
    

        return Vector2.Zero;
    }



}