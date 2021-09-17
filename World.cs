using Godot;
using System;

public class World : Node2D
{

    private TileMap tilemap;

    public override void _Ready()
    {
        tilemap = GetNode<TileMap>("TileMap");
        Globals sexo = (Globals)GetTree().Root.GetNode("Globals"); //.TheWorld = this;

        sexo.TheWorld = this;
        
    }

    public bool TryKillingPlrWithTiles(Vector2 PlrPos){
        if (tilemap.GetCellv(tilemap.WorldToMap(PlrPos)) == 1)
        {
            return true;
        }
        return false;

    }

}
