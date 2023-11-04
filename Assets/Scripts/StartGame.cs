using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    [SerializeField] GameObject GameModeCanvas;
    [SerializeField] GameObject GameOverCanvas;
    [SerializeField] GameObject GlowBall;
    [SerializeField] GameObject Counter;
    [SerializeField] GameObject InGameUI;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PrivacyPolicyCanvas;
    [SerializeField] GameObject MainMenu;

    [SerializeField] Vector3 PositionClassic;
    [SerializeField] Vector3 PositionOneHole;
    [SerializeField] Vector3 PositionTwoHole;

    public string GameMode;

    void Start(){
        Time.timeScale = 0;
        
        if(!Convert.ToBoolean(PlayerPrefs.GetInt("IsNotFirstTimePlaying"))){
            PrivacyPolicyCanvas.SetActive(true);
            MainMenu.SetActive(false);
        }
        else{
            PrivacyPolicyCanvas.SetActive(false);
            MainMenu.SetActive(true);
        }
        
    }

    public void StartGameClassicFunction(){
        StartGameFunction("Classic");
     }

    public void StartGameOneHoleFunction(){
        StartGameFunction("OneHole");
     }

      public void StartGameTwoHolesFunction(){
          StartGameFunction("TwoHoles");
     }

   
   public void StartGameFunction(string GameMode){
       GetComponent<Pause>().playerIsAlive = true;
       GameModeCanvas.SetActive(false);
       GameOverCanvas.SetActive(false);
       Time.timeScale = 1;
       Counter.SetActive(true);
       InGameUI.SetActive(true);
       GetComponent<Death>().Score = 0;
       GlowBall.GetComponent<PlayerMovement>().enabled = true;
       GlowBall.GetComponent<PlayerMovement>().isPlaying = true;
       GetComponent<Pause>().isPlaying = true;
    if(GameMode ==  "Classic"){
       GetComponent<MapGenerator>().GameMode = "Classic";
       GetComponent<ColumnRandomizator>().GameMode = "Classic";
       GetComponent<Death>().GameMode = "Classic";
       GlowBall.GetComponent<PlayerMovement>().GameMode = "Classic";
       GameMode = "Classic";
       
      
    } else if(GameMode ==  "OneHole"){
        GetComponent<MapGenerator>().GameMode = "OneHole";
        GetComponent<ColumnRandomizator>().GameMode = "OneHole";
        GetComponent<Death>().GameMode = "OneHole";
        GlowBall.GetComponent<PlayerMovement>().GameMode = "OneHole";
        GameMode = "OneHole";
        GlowBall.transform.localPosition = new Vector3(0,0,transform.localPosition.z);
     
        
    }
    else if(GameMode ==  "TwoHoles"){
        GetComponent<MapGenerator>().GameMode = "TwoHoles";
        GetComponent<ColumnRandomizator>().GameMode = "TwoHoles";
        GetComponent<Death>().GameMode = "TwoHoles";
        GlowBall.GetComponent<PlayerMovement>().GameMode = "TwoHoles";
        GameMode = "TwoHoles";
        GlowBall.transform.localPosition = new Vector3(0,0,transform.localPosition.z);
      
        
    }
    GetComponent<ColumnRandomizator>().Randomize();
   }
   
   public void MainMenuOn(){
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        GetComponent<AdsManager>().PlayAd();
   }

   public void Restart(){
    GlowBall.GetComponent<PlayerMovement>().enabled = false;
    GlowBall.GetComponent<PlayerMovement>().moveSpeed = 45;
    GetComponent<Death>().StartCountdown();
    GetComponent<Death>().Score = 0;
    GetComponent<MapGenerator>().timeUntilNextTerrainIsSpawned = 1;   
    GetComponent<MapGenerator>().timeUntilNextColumnIsSpawned = 0.5f;
    GetComponent<MapGenerator>().timeUntilNextColumnIsSpawnedHole = 1;  
    PauseMenu.SetActive(false);
    
    
      
   }

   public void AgreePrivacyPolicy(){
       PrivacyPolicyCanvas.SetActive(false);
       MainMenu.SetActive(true);
       PlayerPrefs.SetInt("IsNotFirstTimePlaying",1);
   }

   public void Quit(){
       Application.Quit();
   }
}
