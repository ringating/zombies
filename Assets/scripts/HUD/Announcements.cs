using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class TextAnnouncement
{
	public string name;
	public Text textScript;
	public float timer;
	public float fadeTimer;
	public float fadeDuration;

	private Color defaultColor = new Color(1,1,1,1);

	public TextAnnouncement(string name, Text text)
	{
		this.init(name, text, 0f, 0f, defaultColor);
	}

	private void init(string name, Text textScript, float timer, float fadeTimer, Color color)
	{
		this.name = name;
		this.textScript = textScript;
		this.timer = timer;
		this.fadeTimer = fadeTimer;
		this.fadeDuration = fadeTimer;
		this.textScript.color = color;

		this.textScript.text = "";
	}

	public TextAnnouncement setText(string text)
	{
		this.textScript.text = text;
		return this;
	}

	public TextAnnouncement setTimer(float duration)
	{
		this.timer = duration;
		return this;
	}

	public TextAnnouncement setFadeTimer(float fadeDuration)
	{
		this.fadeTimer = fadeDuration;
		this.fadeDuration = fadeDuration;
		return this;
	}

	public TextAnnouncement setAlpha(float alpha)
	{
		this.textScript.color = new Color(this.textScript.color.r, this.textScript.color.g, this.textScript.color.b, alpha);
		return this;
	}

	public TextAnnouncement setColor(Color color)
	{
		this.textScript.color = color;
		return this;
	}
}

public class Announcements : MonoBehaviour 
{
	public Text[] textAnnouncementLocations;
	public string[] textAnnouncementLocationNames;

	private List<TextAnnouncement> ta = new List<TextAnnouncement>();
	
	void Start() 
	{
		for(int i = 0; i < textAnnouncementLocations.Length; ++i)
		{
			ta.Add(new TextAnnouncement(textAnnouncementLocationNames[i], textAnnouncementLocations[i]));
		}
	}

	void Update()
	{
		for(int i = 0; i < ta.Count; ++i)
		{
			// everything to do with durations
			if(ta[i].timer > 0)
			{
                ta[i].timer -= Time.deltaTime;
				ta[i].setAlpha(1);
				if(ta[i].timer < 0)
				{ 
					ta[i].timer = 0; 
					if(ta[i].fadeTimer <= 0){ ta[i].setAlpha(0); }
				}
			}
			else if(ta[i].fadeTimer > 0)
			{
                ta[i].fadeTimer -= Time.deltaTime;
				ta[i].setAlpha(Mathf.Lerp(0, 1, ta[i].fadeTimer/ta[i].fadeDuration));
				if(ta[i].fadeTimer < 0){ ta[i].fadeTimer = 0; }
			}
		}
	}
	
	private void Announce(string message, int announcementIndex, float duration, float fadeDuration)
	{
		ta[announcementIndex].setText(message).setTimer(duration).setFadeTimer(fadeDuration);
	}

	public void Announce(string message, string announcementLocationName, float duration, float fadeDuration)
	{
		Announce(message, GetAnnLocIndexByName(announcementLocationName), duration, fadeDuration);
	}

    public void SetAnnouncementColor(string announcementLocationName, Color color)
    {
        ta[GetAnnLocIndexByName(announcementLocationName)].setColor(color);
    }

	private int GetAnnLocIndexByName(string name)
	{
		int index = 0;
		for(; index < ta.Count; ++index)
		{
			if(ta[index].name == name)
			{
				break;
			}
		}
		if(index >= ta.Count){index = -1;}
		return index;
	}

	public void SetColorByName(string name, Color color)
	{

	}
}
