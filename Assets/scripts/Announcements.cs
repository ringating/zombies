using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Announcements : MonoBehaviour 
{
	public Text majorText;
	public Text minorText;

	private Text[] texts;

	private float[] timer = {0f, 0f};
	private float[] fadeTimer = {0f, 0f};
	
	void Start() 
	{
		texts[0] = majorText;
		texts[1] = minorText;
		texts[0].text = "";
		texts[1].text = "";
	}

	void Update()
	{
		for(int i = 0; i < texts.Length; ++i)
		{
			// everything to do with durations
			if(timer[i] > 0)
			{
				timer[i] -= Time.deltaTime;
				//TODO?
			}
			else if(fadeTimer[i] > 0)
			{
				fadeTimer[i] -= Time.deltaTime;
				//TODO
			}
		}
	}
	
	public void Announce(string text, bool major, float duration, float fadeDuration)
	{
		int textIndex;
		if(major)
		{
			textIndex = 0;
		}
		else
		{
			textIndex = 1;
		}

		texts[textIndex].text = text;

	}
}
