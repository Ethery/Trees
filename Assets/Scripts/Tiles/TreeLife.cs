using UnityEngine;

[RequireComponent(typeof(TreeRefs))]
public class TreeLife : MonoBehaviour
{
	private float LifeTime;

	public float LifeTimeNormalized => ((LifeTime / GameManager.Instance.GameRules.MaxLife[TreeSize]));

	private float WateringLevel = 0;
	public float WateringLevelNormalized => ((WateringLevel / GameManager.Instance.GameRules.MaxWateringLevel[TreeSize]));

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

		LifeTime = GameManager.Instance.GameRules.MaxLife[TreeSize];
		WateringLevel = 0;
		TimeBeforeFalling = GameManager.Instance.GameRules.TimeBeforeFalling;

		m_Animator = GetComponent<Animator>();

		//Init Animations.
		m_Animator.SetFloat("LifeTime", LifeTimeNormalized);
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
				if (LifeTime < GameManager.Instance.GameRules.MaxLife[TreeSize])
				{
					LifeTime += Time.deltaTime;
				}
				else
				{
					LifeTime = GameManager.Instance.GameRules.MaxLife[TreeSize];
				}
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
			m_Animator.SetFloat("LifeTime", LifeTimeNormalized);
			m_Animator.SetFloat("Season", GameManager.Instance.Season);
		}
		GetComponent<TreeRefs>().LifeTimeSlider.value = LifeTimeNormalized;
		GetComponent<TreeRefs>().WateringLevelSlider.value = WateringLevelNormalized;
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