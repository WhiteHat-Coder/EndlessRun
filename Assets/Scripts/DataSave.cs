using System;
using UnityEngine;

public class DataSave : Singleton<DataSave>
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			DeleteData(Constants.TotalScore);
			DeleteData(Constants.BestScore);
		}
	}

	public void SetIntData(string name, int score)
	{
		PlayerPrefs.SetInt(name, score);
	}
	
	public int GetIntData(string name)
	{
		return PlayerPrefs.GetInt(name);
	}
	
	public void SetFloatData(string name, float score)
	{
		PlayerPrefs.SetFloat(name, score);
	}
	
	public float GetFloatData(string name)
	{
		return PlayerPrefs.GetFloat(name);
	}
	
	private void DeleteData(string name)
	{
		PlayerPrefs.DeleteKey(name);
	}
}
