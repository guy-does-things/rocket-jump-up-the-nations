using Godot;
using System;

public class Globals : Node
{
    public Node TheWorld;

    private int TotalDeaths;

    public void Restart()
    {
        TotalDeaths = 0;
    }

    public void IncreaseDeaths()
    {
        TotalDeaths++;

    }
    
	public int TimesDead{
		get{return TotalDeaths;}
	}

}
