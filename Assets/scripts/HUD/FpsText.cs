using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsText : MonoBehaviour 
{
	public Text textComponent;

	void Start () 
	{
		textComponent.text = "0";
	}
	
	void Update () 
	{
		textComponent.text = (1/Time.deltaTime).ToString().Split('.')[0];
	}
}
