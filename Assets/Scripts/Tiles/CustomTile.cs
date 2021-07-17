using UnityEngine.Tilemaps;

public class CustomTile : Tile
{
	public bool isTargetable = true;
	public bool Selected { get; set; }
}