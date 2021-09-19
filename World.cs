using Godot;
using System;

public class World : Node2D
{	

	public CanvasLayer endscreen;

	private TileMap tilemap;
	private int Deaths;

	public int TimesDead{
		get{return Deaths;}
	}

	public void IncreaseDeaths()
	{
		Deaths++;
	}

	public override void _Ready()
	{
		endscreen = GetNode<CanvasLayer>("CanvasLayer2");
		tilemap = GetNode<TileMap>("TileMap");
		Globals sexo = (Globals)GetTree().Root.GetNode("Globals"); //.TheWorld = this;

		sexo.TheWorld = this;
		
	}

	public bool TryKillingPlrWithTiles(Vector2 PlrPos){
		if (tilemap.GetCellv(tilemap.WorldToMap(PlrPos)) == 2)
		{
			return true;
		}
		return false;

	}

}
