using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    [SerializeField] GameObject SceneLoader;

    void OnTriggerEnter(Collider other)
    {
        SceneLoader.GetComponent<Death>().Score++;   
    }
}
