using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(GameRules))]
public class GameRulesEditor : Editor
{
	private GameRules m_Self;

	private void Awake()
	{
		m_Self = (GameRules)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Prefab", EditorStyles.boldLabel);

		if (m_Self.m_Prefabs == null)
		{
			m_Self.m_Prefabs = new List<TreeTile>();
			m_Self.m_Prefabs.Clear();
		}
		for (int i = 0; i < (int)ETreeSize.Count; i++)
		{
			if (m_Self.m_Prefabs.Count <= (int)i)
			{
				m_Self.m_Prefabs.Add(null);
			}
			m_Self.m_Prefabs[i] = (TreeTile)EditorGUILayout.ObjectField(((ETreeSize)i).ToString(), m_Self.m_Prefabs[i], typeof(TreeTile), false);
		}
	}
}