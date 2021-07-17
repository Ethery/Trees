using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TreeTile")]
public class TreeTile : CustomTile
{
	public ETreeSize TreeSize;
	public TreeLife TreeLife;

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		bool startUpBaseResult = base.StartUp(position, tilemap, go);
		if (go != null)
		{
			if (!go.TryGetComponent<TreeLife>(out TreeLife))
			{
				TreeLife = go.AddComponent<TreeLife>();
			}
			TreeLife.Init(this, TreeSize, position);
		}
		return startUpBaseResult;
	}
}