using Godot;
using System;



public class InputComponent{
    public float GetDir(){
        return Input.GetActionStrength("right") - Input.GetActionStrength("left"); 
    }

    public void ApplyVelocity(Player plr, float Speed){
        plr.Velocity.x += GetDir() * Speed;

    }


}


public class GravityComponent
{
    public bool GravEnabled = true;
    float GravityForce = 20f;
    public void ApplyGravity(Player plr){
        if (GravEnabled){plr.Velocity.y += GravityForce;}
        
    }



}

public class ShootComponent
{
    private RocketLauncher Gun;

    public void TryFiring(Vector2 ShootPos)
    {
        if(Input.IsActionJustPressed("shoot") && Gun != null)
        {
            Gun.AddExplosion(ShootPos);
        }
    }

    public void SetLauncher(RocketLauncher launcher)
    {
        Gun = launcher;
    }

    public void Update(Player plr)
    {
        if (Gun != null)
        {
            Gun.AimAt(plr.GetGlobalMousePosition());

        }
    }
}

public class JumpComponent
{

    public float JumpHeight = -600;
    public bool Jump(Player plr, bool canJump ){
        if (canJump)
        {
            plr.Velocity.y += JumpHeight;
            return true;
        }
        return false;
    }


}

public class FrictionComponent
{
    public float friction;

    public void checkIfOnFloor(bool OnFloor){
        if (OnFloor){
            friction = 1.1f;
        }
        else{
            friction = 1.045f;
        }

    }

    public void ApplyFriction(Player plr){
        plr.Velocity.x /= friction ;
    }


}

public class Player : KinematicBody2D
{



    bool endmylife = false;
    public Vector2 Velocity = new Vector2();

    private float friction;

    public bool IsAbleToJump = true;
    
    private RayCast2D Jumpcast;
    private Camera2D cam;
    private JumpComponent jmp = new JumpComponent(); 
    private InputComponent input = new InputComponent();
    private GravityComponent grav = new GravityComponent();
    private FrictionComponent fric = new FrictionComponent();

    private HookShot hook;
    private ShootComponent gun = new ShootComponent();
    private Vector2 lastCheckPoint;

    public AnimationPlayer anim; 
    
    public World theworld;
    public StateMachine statemacine;
    public Timer CoyoteTimer;
    private int deathcounter;

    public int DeathCounter
    {
        get{return deathcounter;}
    }
    public void SetCheckPoint(Vector2 CheckPointPos)
    {
        GD.Print("WHY NOT WORK");
        lastCheckPoint = CheckPointPos;
    }

    public void Respawn()
    {
        anim.Stop();
        RotationDegrees = 0f;
        Rotation = 0;
        GlobalPosition = lastCheckPoint;
        statemacine.SetState(StateMachine.STATES.IDLE);

    }

    public void Die()
    {
        anim.Play("death");
        statemacine.SetState(StateMachine.STATES.DEAD);
        deathcounter += 1;
        
    }


    

    public bool canJump()
    {
        return hook.Hooked || IsOnFloor() ||Jumpcast.IsColliding() ||!CoyoteTimer.IsStopped()|| IsAbleToJump;
    }
    public override void _Ready()
    {
        anim = GetNode<AnimationPlayer>("AnimationPlayer");
        theworld = GetParent() as World; 
        statemacine = GetNode<StateMachine>("StateMachine");
        CoyoteTimer = GetNode<Timer>("Timer");
        SetCheckPoint(GlobalPosition);
        Jumpcast = GetNode<RayCast2D>("RayCast2D");
        hook = GetNode<HookShot>("HookShot");
        cam = GetNode<Camera2D>("Camera2D");
    }

    public void AddGun(RocketLauncher rocketlaunch)
    {
        gun.SetLauncher(rocketlaunch);

        AddChild(rocketlaunch);
    }

    public override void _PhysicsProcess(float delta)
    {
        
        grav.ApplyGravity(this);
        fric.checkIfOnFloor(IsOnFloor());
        
        if (statemacine.GetState() != StateMachine.STATES.DEAD)
        {
            gun.Update(this);
            input.ApplyVelocity(this, 50f);
            gun.TryFiring(GetGlobalMousePosition());

            if (Input.IsActionJustPressed("jump"))
            {
                if(jmp.Jump(this, canJump()))
                {
                    IsAbleToJump = false;
                }
            }


            if(Input.IsActionPressed("hook"))
            {
                hook.Hook(GetLocalMousePosition());
            }

            if(Input.IsActionJustReleased("hook"))
            {
                hook.UnHook();
            }

            if (theworld.TryKillingPlrWithTiles(GlobalPosition))
            {
                Die();
            }

        }
       
        fric.ApplyFriction(this);
        Velocity = MoveAndSlide(Velocity, Vector2.Up);
        
        

        Velocity += hook.GetMovementDir(GlobalPosition, 50f);
   

        //cam.Position = GetLocalMousePosition() / 4;
        

    }
}



