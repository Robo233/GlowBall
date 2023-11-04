using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Death : MonoBehaviour
{
    public int Score;
    public int Best;
    public int BestOneHole;
    public int BestTwoHoles;
    int CountDownNumber=3;

    float timer;
    float timer2 = 4;

    public GameObject Counter;
    public GameObject GameOverBest;
    public GameObject GameOverCanvas;
    public GameObject Sphere;
    public GameObject ScoreText;
    public GameObject RestartIn;
    public GameObject Countdown;

    [SerializeField] GameObject HighscoreClassic;
    [SerializeField] GameObject HighscoreOneHole;
    [SerializeField] GameObject HighscoreTwoHoles;

    [SerializeField] AudioSource DeathSound;

    bool isCountDownStarted;
    bool PlayerWasRevived;
    public bool PlayerCanDie = true;

    public string GameMode;

    void Start(){
        Best = PlayerPrefs.GetInt("Best");
        BestOneHole = PlayerPrefs.GetInt("BestOneHole");
        BestTwoHoles = PlayerPrefs.GetInt("BestTwoHoles");

        HighscoreClassic.GetComponent<TextMeshProUGUI>().text = "Classic: " + Best.ToString();
        HighscoreOneHole.GetComponent<TextMeshProUGUI>().text = "One hole: " + BestOneHole.ToString();
        HighscoreTwoHoles.GetComponent<TextMeshProUGUI>().text = "Two holes: " + BestTwoHoles.ToString();
       
    }



    void Update(){

        if(Score<10){
            Counter.transform.localPosition = new Vector3(550,Counter.transform.localPosition.y,0);
        }
        else if(Score>=10 && Score<100){
            Counter.transform.localPosition = new Vector3(430,Counter.transform.localPosition.y,0);
        }
        else if(Score>=100){
            Counter.transform.localPosition = new Vector3(305,Counter.transform.localPosition.y,0);
        }
         Counter.GetComponent<TextMeshProUGUI>().text = Score.ToString();

         if(GameMode == "Classic"){
            GameOverBest.GetComponent<TextMeshProUGUI>().text = "Best: " + Best;
         }
         else if(GameMode == "OneHole"){
           
            GameOverBest.GetComponent<TextMeshProUGUI>().text = "Best: " + BestOneHole;
         }
         else if(GameMode == "TwoHoles"){
            GameOverBest.GetComponent<TextMeshProUGUI>().text = "Best: " + BestTwoHoles;
         }

        if(Score>Best){
            if(GameMode == "Classic"){
            Best = Score;
            PlayerPrefs.SetInt("Best",Best);
            }
        }
        if(Score>BestOneHole){
            if(GameMode == "OneHole"){
            BestOneHole = Score;
            PlayerPrefs.SetInt("BestOneHole",BestOneHole);
            }
        }
        if(Score>BestTwoHoles){
            if(GameMode == "TwoHoles"){
            BestTwoHoles = Score;
            PlayerPrefs.SetInt("BestTwoHoles",BestTwoHoles);
            }
        }
        

        if(isCountDownStarted){
              timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 1;
                StartCoroutine(CountDownFunction());
            }
            if(CountDownNumber == 0){
                Revive();
                isCountDownStarted = false;
                CountDownNumber = 3;
            }
        }

        if(PlayerWasRevived){
            timer2 -= Time.deltaTime;
            if(timer2<=0){
                PlayerCanDie = true;
                PlayerWasRevived = false;
                timer2 = 4;
            }
        
            if( (timer2<4.75&&timer2>4.5) || (timer2<4.25&&timer2>4) || (timer2<3.75&&timer2>3) || (timer2<3.25&&timer2>3) || (timer2<2.75&&timer2>2.5) || (timer2<2.25&&timer2>2) || (timer2<1.75&&timer2>1.5) || (timer2<1.25&&timer2>1) || (timer2<0.75&&timer2>0.5) || (timer2<0.25&&timer2>0) ){
                Sphere.GetComponent<MeshRenderer>().enabled = false;
            }
            else{
                Sphere.GetComponent<MeshRenderer>().enabled = true;
            }
        }

    }

    public void DeathFunction(){
       PlayerPrefs.SetInt("CoinAmount", GetComponent<Shop>().CoinAmount);
       GameOverCanvas.SetActive(true);
       Sphere.GetComponent<PlayerMovement>().enabled = false;
       Time.timeScale = 0;
       ScoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + Score;
       DeathSound.Play();
       Countdown.GetComponent<TextMeshProUGUI>().text = "3";
       GetComponent<Pause>().playerIsAlive = false;
        
    }

    public void StartCountdown(){
        GameOverCanvas.SetActive(false);
        Time.timeScale = 1;
        if(GameMode=="Classic"){
        Sphere.transform.position = new Vector3(Sphere.transform.position.x,25,Sphere.transform.position.z);
        }
        RestartIn.SetActive(true);
        Countdown.SetActive(true);
        isCountDownStarted = true;
        PlayerCanDie = false;
        
    }

    public IEnumerator CountDownFunction(){
        yield return new WaitForSeconds(0);
        CountDownNumber--;
        Countdown.GetComponent<TextMeshProUGUI>().text = CountDownNumber.ToString();

    }

    public void Revive(){
        RestartIn.SetActive(false);
        Countdown.SetActive(false);
        Sphere.GetComponent<PlayerMovement>().enabled = true;
        Sphere.GetComponent<PlayerMovement>().isPlaying = true;
        PlayerWasRevived = true;
        Debug.Log("Revive");
        Countdown.GetComponent<TextMeshProUGUI>().text = "3";
    }

}
