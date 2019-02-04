using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnButton : MonoBehaviour 
{
	public string buttonName;
	
	void Update()
	{
		if(Input.GetButtonDown(buttonName))
		{
			SceneManager.LoadScene("test");
		}
	}
}
