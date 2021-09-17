using Godot;
using System;

public class GunPickup : Area2D
{
    public override void _Ready()
    {
        
    }

    
    public void OnBodyEntered(Godot.Object Body)
    {
        Player plr = Body as Player;

        if(plr != null)
        {
            RocketLauncher launcher = GetNode<RocketLauncher>("RocketLauncher");

            RemoveChild(launcher);

            plr.AddGun(launcher);

            QueueFree();


        }

    }

}
