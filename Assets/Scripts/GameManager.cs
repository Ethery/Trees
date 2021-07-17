using UnityEngine;
using UnityEngine.Tilemaps;

public enum ETreeSize
{
	None = 0,
	Large,
	Medium,
	Small,
}

public enum ESeason
{
	Spring = 0,
	Summer,
	Fall,
	Winter
}

public class GameManager : MonoBehaviour
{
	public static GameManager m_Instance;

	public static GameManager Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = FindObjectOfType<GameManager>();
				if (m_Instance == null)
					m_Instance = new GameObject("GameManager", typeof(GameManager)).GetComponent<GameManager>();
			}
			return m_Instance;
		}
	}

	public Tilemap Map;

	[SerializeField]
	private GameRules m_GameRules;
	public GameRules GameRules => m_GameRules;

	public float Season;

	private void Update()
	{
		Season = ((Time.time / 5) % 4);
	}
}