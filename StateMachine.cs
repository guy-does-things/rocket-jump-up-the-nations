using Godot;
using System;

public class StateMachine : Node
{
    public enum STATES
    {
        FALL,
        JUMP,
        IDLE,
        WALK,
        DEAD,

        NONE
        
    }

    [Export]
    STATES CurrentState;

    Player Parent;
    STATES PreviousState;

    public override void _Ready()
    {
        Parent = GetParent() as Player;

    }

    public override void _PhysicsProcess(float delta)
    {
        if (CurrentState != STATES.NONE)
        {
            StateLogic();
            STATES transition = _get_transition();

            if (transition != STATES.NONE)
            {
                SetState(transition);
            }
            


        }

    }


    private void EnterState(STATES PreviousState, STATES NewState)
    {
        if (PreviousState != STATES.JUMP)
        {
            Parent.CoyoteTimer.Start();
        }
    }

    private void ExitState(STATES PreviousState, STATES NewState)
    {
        
    }

    public void SetState(STATES NewState)
    {
        PreviousState = CurrentState;

        CurrentState = NewState;

        if (PreviousState != STATES.NONE)
        {
            ExitState(PreviousState, NewState);
        }

        if (NewState != STATES.NONE)
        {
            EnterState(PreviousState, NewState);

        }


    }

    public STATES GetState()
    {
        return CurrentState;
    }

    private void StateLogic()
    {
        
    }


    private STATES _get_transition()
    {
        switch (CurrentState)
        {
            case STATES.IDLE:
                if (Parent.Velocity.y > 0)
                {
                    return STATES.FALL;
                }
                else if (Parent.Velocity.y < 0)
                {
                    return STATES.JUMP;
                }

                if (Mathf.Abs(Parent.Velocity.x) > 10)
                {
                    return STATES.WALK;
                }
                
                break;

            case STATES.WALK:
                if (Parent.Velocity.y > 0)
                {
                    return STATES.FALL;
                }
                else if (Parent.Velocity.y < 0)
                {
                    return STATES.JUMP;
                }

                if (Mathf.Abs(Parent.Velocity.x) < 10)
                {
                    return STATES.IDLE;
                }
                
                break;         


            case STATES.FALL:
                if (Parent.Velocity.y < 0)
                {
                    return STATES.JUMP;
                }

                if (Parent.Velocity.y == 0)
                {
                    return STATES.IDLE;
                }            
                break;    

            case STATES.JUMP:
                if (Parent.Velocity.y > 0)
                {
                    return STATES.FALL;
                }

                if (Parent.Velocity.y == 0)
                {
                    return STATES.IDLE;
                }
                
                break;


        }
            
        return STATES.NONE;


    }









}
