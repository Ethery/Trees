using UnityEngine;

public class TreeLife : MonoBehaviour
{
	private float LifeTime;
	private float WateringLevel = 0;
	private float TimeBeforeFalling;

	private Vector3Int TreePosition;
	private ETreeSize TreeSize;
	private TreeTile TileData;

	private GameObject Selection;
	private Animator m_Animator;

	public void Init(TreeTile tileData, ETreeSize size, Vector3Int position)
	{
		TileData = tileData;
		TreeSize = size;
		TreePosition = position;

		LifeTime = GameManager.Instance.GameRules.m_MaxLife / (float)TreeSize;
		WateringLevel = 0;
		TimeBeforeFalling = GameManager.Instance.GameRules.TimeBeforeFalling;

		m_Animator = GetComponent<Animator>();

		//Init Animations.
		float percentage = ((LifeTime / GameManager.Instance.GameRules.m_MaxLife));
		m_Animator.SetFloat("LifeTime", percentage);
	}

	public void Update()
	{
		if (LifeTime > 0f)
		{
			if (WateringLevel <= 0)
			{
				LifeTime -= Time.deltaTime;
			}
			else
			{
				WateringLevel -= Time.deltaTime;
				LifeTime += Time.deltaTime;
			}
		}
		else
		{
			LifeTime = 0;
			Debug.Log("Dead Tree", this);
			if (TimeBeforeFalling > 0)
			{
				TimeBeforeFalling -= Time.deltaTime;
			}
			else
			{
				FallTree();
			}
		}

		if (LifeTime > 0)
		{
			float percentage = ((LifeTime / GameManager.Instance.GameRules.m_MaxLife));
			m_Animator.SetFloat("LifeTime", percentage);
			m_Animator.SetFloat("Season", GameManager.Instance.Season);
		}
	}

	public void WaterTree(float waterAmount)
	{
		WateringLevel += waterAmount;
	}

	public void FallTree()
	{
		GameManager.Instance.Map.SetTile(TreePosition, null);
	}
}