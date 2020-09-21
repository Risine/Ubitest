using UnityEngine;

[CreateAssetMenu(fileName = "SC_PlayerData.asset", menuName = "UbiTest/SC_PlayerData")]
public class SC_PlayerData : ScriptableObject
{
	[SerializeField] public string m_name								= "";

	// -----------------------------------------------------------
	// Reset.
	// -----------------------------------------------------------
	public void Reset()
	{
		m_name = "";
	}

	// -----------------------------------------------------------
	// isNameValid.
	// -----------------------------------------------------------
	public bool isNameValid()
	{
		if (m_name.Length > 16) return false;

		if (m_name.Contains(":")) return false;
		if (m_name.Contains("!")) return false;
		if (m_name.Contains(";")) return false;

		return true;
	}

	// -----------------------------------------------------------
	// isNameValid.
	// -----------------------------------------------------------
	public void SetName(string name)
	{
		m_name = name;
	}
}
