using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public GameObject SceneLoader;

    public bool PlayerCanDie;

    void Start(){
        SceneLoader = GameObject.Find("SceneLoader");
        
    }

    void Update() {
        PlayerCanDie = SceneLoader.GetComponent<Death>().PlayerCanDie;
    }

     void OnTriggerEnter(Collider other)
    {
        if(transform.name == "Terrain"){
            SceneLoader.GetComponent<Death>().DeathFunction();
        }
        else if(PlayerCanDie){
        SceneLoader.GetComponent<Death>().DeathFunction(); 
        }   
    }
}
