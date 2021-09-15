using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterController : MonoBehaviour
{
	public Tilemap Interactions => GameManager.m_Instance.Interactions;
	public Tilemap Map => GameManager.m_Instance.Map;

	[Header("Aim")]
	public TileBase AimTile;
	public Vector3Int AimPos;

	public Vector2 LastInputCamera;
	public Plane m_Plane = new Plane(Vector3.up, -1);

	[Header("Debug")]
	public Transform DebugObject;

	public void OnCursor()
	{
		Interactions.SetTile(AimPos, null);

		Vector3 pos = DebugObject.position = GetMousePosOnPlane();
		AimPos = GameManager.Instance.Map.LocalToCell(pos);

		Interactions.SetTile(AimPos, AimTile);
	}

	public void OnMouse()
	{
		Interactions.SetTile(AimPos, null);
		if (Mouse.current.leftButton.isPressed)
		{
			Interactions.SetTile(AimPos, AimTile);
			TreeTile SelectedTile = Map.GetTile<TreeTile>(AimPos);
			if (SelectedTile != null && SelectedTile.isTargetable)
			{
				SelectedTile.TreeLife.WaterTree(GameManager.Instance.GameRules.WateringPerAction);
			}
			else
			{
				Map.SetTile(AimPos, GameManager.Instance.GameRules.m_Prefabs[(int)ETreeSize.Medium]);
			}
		}
	}

	public Vector3 GetMousePosOnPlane()
	{
		Vector2 mousePos = Mouse.current.position.ReadUnprocessedValue();
		Ray mouseRay = GetComponent<PlayerInput>().camera.ScreenPointToRay(mousePos);
		float enter;
		if (m_Plane.Raycast(mouseRay, out enter))
		{
			return mouseRay.GetPoint(enter);
		}
		return new Vector3();
	}

	private void Update()
	{
		Camera cam = GetComponent<PlayerInput>().camera;
		Vector2 ScaledTimeDir = LastInputCamera * Time.timeScale * 0.05f;
		Vector3 Dir = new Vector3(ScaledTimeDir.x, ScaledTimeDir.y, 0);
		cam.transform.position += Dir;
	}

	public void OnCamera(InputValue Input)
	{
		LastInputCamera = (Vector2)Input.Get();
	}
}