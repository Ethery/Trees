﻿using UnityEngine;
using UnityEngine.Tilemaps;

public enum ETreeSize
{
	None = -1, //Should be always first
	Large,
	Medium,
	Small,
	Count //should be always last.
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

	[Header("Tilemaps")]
	public Tilemap Map;
	public Tilemap Interactions;

	[SerializeField]
	private GameRules m_GameRules;
	public GameRules GameRules => m_GameRules;

	public float Season;

	private void Update()
	{
		Season = ((Time.time / 5) % 4);
	}
}