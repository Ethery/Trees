using UnityEngine;

[CreateAssetMenu]
public class GameRules : ScriptableObject
{
	[Header("Trees Life")]
	[Tooltip("When running out of water, this time is decremented and the tree start to die.")]
	public float m_MaxLife = 100f;

	[Tooltip("Time before the tree fall once it's dead.")]
	public float TimeBeforeFalling = 5f;
}