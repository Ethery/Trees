using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameRules : ScriptableObject
{
	[Header("Trees Life")]

	#region LifeTime

	[Tooltip("When running out of water, this time is decremented and the tree start to die.")]
	public float MaxLifeForBigTree = 10f;

	private float lastMaxLifeComputed;
	private Dictionary<ETreeSize, float> m_MaxLife;

	public Dictionary<ETreeSize, float> MaxLife
	{
		get
		{
			if (lastMaxLifeComputed != MaxLifeForBigTree || m_MaxLife == null)
			{
				m_MaxLife = new Dictionary<ETreeSize, float>();
				m_MaxLife.Clear();
				for (ETreeSize i = ETreeSize.None + 1; i < ETreeSize.Count; i++)
				{
					m_MaxLife.Add(i, MaxLifeForBigTree / (float)i);
				}
			}
			return m_MaxLife;
		}
	}

	#endregion

	#region Watering

	[Tooltip("When running out of water, this time is decremented and the tree start to die.")]
	public float MaxWateringLevelForBigTree = 10f;

	private Dictionary<ETreeSize, float> m_MaxWateringLevel;
	private float lastMaxWateringLevelComputed;

	public Dictionary<ETreeSize, float> MaxWateringLevel
	{
		get
		{
			if (lastMaxWateringLevelComputed != MaxWateringLevelForBigTree || m_MaxWateringLevel == null)
			{
				m_MaxWateringLevel = new Dictionary<ETreeSize, float>();
				m_MaxWateringLevel.Clear();
				for (ETreeSize i = ETreeSize.None; i < ETreeSize.Count; i++)
				{
					m_MaxWateringLevel.Add(i, MaxWateringLevelForBigTree / (float)i);
				}
			}
			return m_MaxWateringLevel;
		}
	}

	public float WateringPerAction = 5f;

	#endregion

	[Tooltip("Time before the tree fall once it's dead.")]
	public float TimeBeforeFalling = 5f;

	#region Prefabs

	[HideInInspector]
	public List<TreeTile> m_Prefabs;

	#endregion
}