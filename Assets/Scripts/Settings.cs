using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
   
    [SerializeField] GameObject CheckMark;

    [SerializeField] bool Sound;

    void Start(){
        
        if(!Convert.ToBoolean(PlayerPrefs.GetInt("IsNotFirstTimePlaying"))){
            PlayerPrefs.SetInt("Sound",1);
        }

        Sound = Convert.ToBoolean(PlayerPrefs.GetInt("Sound"));
        if (Sound)
		{
			
            AudioListener.volume = 1;
            CheckMark.SetActive(true);
		}
        else{
           
            AudioListener.volume = 0;
            CheckMark.SetActive(false);
        }
    }
    
    public void Mute(){
        if(Sound){
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("Sound",0);
        Sound = false;
        CheckMark.SetActive(false);
        }
        else{
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Sound",1);
            Sound = true;
            CheckMark.SetActive(true);
        }
    }

}
