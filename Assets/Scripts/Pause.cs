using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPlaying;
    public bool playerIsAlive;

    public GameObject PauseMenu;

    [SerializeField] GameObject GlowBall;

    public void PauseFunction(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        GlowBall.GetComponent<PlayerMovement>().isPlaying = false;
        isPlaying = false;
    }

    public void ResumeFunction(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        GlowBall.GetComponent<PlayerMovement>().isPlaying = true;
        isPlaying = true;
    }

    private void OnApplicationPause(bool pause)
	{
		if (pause && isPlaying && playerIsAlive)
		{
			PauseFunction();
		}
	}

    private void OnApplicationFocus(bool pause)
	{
		if (!pause && isPlaying && playerIsAlive)
		{
			PauseFunction();
		}
	}
}
