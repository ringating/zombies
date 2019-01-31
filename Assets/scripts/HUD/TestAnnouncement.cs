using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnnouncement : MonoBehaviour 
{	
	public Announcements announcementManager;

	public string announcementName;
	public string message;
	public float duration;
	public float fadeDuration;


	public bool testAnnouncement;

	
	void Update () 
	{
		if(testAnnouncement)
		{
			testAnnouncement = false;
			announcementManager.Announce(message, announcementName, duration, fadeDuration);
		}
	}
}
