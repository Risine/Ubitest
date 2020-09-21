using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MB_Menu : MonoBehaviour
{
	[Header("Serialized")]

	[SerializeField] private string m_saveFilePath						= "Saves";
	[SerializeField] private string m_saveFileName						= "Save01.xml";

	[SerializeField] private SC_PlayerData m_playerData					= null;

	[SerializeField] private TextMeshProUGUI m_text						= null;
	[SerializeField] private TMP_InputField m_input						= null;
	[SerializeField] private TextMeshProUGUI m_warning					= null;

	[SerializeField] private Animator m_warningCtrl						= null;

	[SerializeField] private Button m_loadButton						= null;
	[SerializeField] private Button m_saveButton						= null;
	[SerializeField] private Button m_resetButton						= null;

	#region Main

	// -----------------------------------------------------------
	// Start.
	// -----------------------------------------------------------
	private void Start()
	{
		LoadFile(GetSaveFilePath(), m_saveFileName);
		UIUpdate();
	}

	// -----------------------------------------------------------
	// GetSaveFilePath.
	// -----------------------------------------------------------
	private string GetSaveFilePath()
	{
		return $"{Application.persistentDataPath}/{m_saveFilePath}";
	}

	// -----------------------------------------------------------
	// LoadFile.
	// -----------------------------------------------------------
	private void LoadFile(string path, string name)
	{
		string wholeName = $"{path}/{name}";

		if (Directory.Exists(path) && File.Exists(wholeName))
		{
			string inter = File.ReadAllText(wholeName);
			JsonUtility.FromJsonOverwrite(inter, m_playerData);
			ChangePlayerName(m_playerData.m_name);
		}
		else
		{
			ResetPlayerData();
		}
	}

	// -----------------------------------------------------------
	// SaveFile.
	// -----------------------------------------------------------
	private void SaveFile(string path, string name)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		File.WriteAllText($"{path}/{name}", JsonUtility.ToJson(m_playerData));
	}

	// -----------------------------------------------------------
	// ResetPlayerData.
	// -----------------------------------------------------------
	private void ResetPlayerData()
	{
		m_playerData.Reset();
		UIUpdate();
		m_input.text = "";
	}

	// -----------------------------------------------------------
	// UILoadSaveActivate.
	// -----------------------------------------------------------
	private void UILoadSaveActivate(bool enable)
	{
		m_loadButton.interactable = enable;
		m_saveButton.interactable = enable;
	}

	// -----------------------------------------------------------
	// UIUpdate.
	// -----------------------------------------------------------
	private void UIUpdate()
	{
		m_text.text = m_playerData.m_name;

		UILoadSaveActivate(m_playerData.isNameValid());
	}

	// -----------------------------------------------------------
	// ChangePlayerName.
	// -----------------------------------------------------------
	public void ChangePlayerName(string name)
	{
		m_playerData.SetName(name);

		bool isValid = m_playerData.isNameValid();

		UILoadSaveActivate(isValid);
		m_warningCtrl.SetBool("Display", !isValid);
	}

	#endregion

	#region Events

	// -----------------------------------------------------------
	// OnClickLoad.
	// -----------------------------------------------------------
	public void OnClickLoad()
	{
		ResetPlayerData();
		LoadFile(GetSaveFilePath(), m_saveFileName);
		UIUpdate();
	}

	// -----------------------------------------------------------
	// OnClickSave.
	// -----------------------------------------------------------
	public void OnClickSave()
	{
		SaveFile(GetSaveFilePath(), m_saveFileName);
		UIUpdate();
	}

	// -----------------------------------------------------------
	// OnClickReset.
	// -----------------------------------------------------------
	public void OnClickReset()
	{
		ResetPlayerData();
		UIUpdate();
	}

	// -----------------------------------------------------------
	// OnInputUpdate.
	// -----------------------------------------------------------
	public void OnInputUpdate()
	{
		ChangePlayerName(m_input.text);
	}

	#endregion
}
