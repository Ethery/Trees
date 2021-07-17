using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterController : MonoBehaviour
{
	public Tilemap Interactions;

	[Header("Aim")]
	public TileBase AimTile;
	public Vector3Int AimPos;
	public Vector3Int SelectionPos;

	public Vector2 LastInputCamera;
	public Plane m_Plane = new Plane(Vector3.up, -1);

	public void OnCursor()
	{
		Interactions.SetTile(AimPos, null);

		AimPos = GameManager.Instance.Map.LocalToCell(GetMousePosOnPlane());

		Interactions.SetTile(AimPos, AimTile);
	}

	public void OnMouse()
	{
		if (Mouse.current.leftButton.isPressed)
		{
			SelectionPos = GameManager.Instance.Map.LocalToCell(GetMousePosOnPlane());

			Interactions.SetTile(SelectionPos, null);
			CustomTile SelectedTile = GameManager.Instance.Map.GetTile<CustomTile>(SelectionPos);
			if (SelectedTile != null && SelectedTile.isTargetable)
			{
				Interactions.SetTile(SelectionPos, AimTile);
				SelectedTile.Selected = true;
				if (SelectedTile is TreeTile)
				{
					(SelectedTile as TreeTile).TreeLife.WaterTree(3f);
				}
			}
		}
	}

	public Vector3 GetMousePosOnPlane()
	{
		Vector2 mousePos = Mouse.current.position.ReadValue();
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