using System;
using UnityEngine.SceneManagement;

public class SwitchSceneHandler:IEventHandler
{
	private String sceneName;
	public SwitchSceneHandler (String sceneName)
	{
		this.sceneName = sceneName;
	}

	public void onEvent(Event evt, Object e) {
		SceneManager.LoadScene(this.sceneName, LoadSceneMode.Single);
	}
}




