using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColumnRandomizator : MonoBehaviour
{
    [SerializeField] GameObject[] LowerColumns;
    [SerializeField] GameObject[] UpperColumns;
    
    [SerializeField] GameObject ColumnOneHole;
    [SerializeField] GameObject ScoreDetectorColumnOneHole;

    [SerializeField] GameObject ColumnTwoHoles;
    [SerializeField] GameObject ScoreDetectorColumnTwoHoles;
    [SerializeField] GameObject Coin;

    float PositionYColumnLower;
    float PositionYColumnUpper;
    float PositionZColumnOneHole;
    float PositionZColumnTwoHoles;
    float PositionYColumn1;
    float DistanceBetweenColumnsOnY;
    public float positionZ;

    int IndexOfObjectToDeactivate;

    public int getRanNum1; 
    public int getRanNum2; 

    public string GameMode;
    string MaterialColor = "Blue";

    [SerializeField] Material ColumnMaterial1;
    [SerializeField] Material ColumnMaterial2;
    [SerializeField] Material ColumnMaterial3;
    [SerializeField] Material ColumnMaterial4;

    [SerializeField] Material ColumnMaterial1Up;
    [SerializeField] Material ColumnMaterial2Up;

    
    public void Randomize()
    {
        if(GameMode == "Classic"){
        for(int i=0;i<LowerColumns.Length;i++){
        PositionYColumn1 = UnityEngine.Random.Range(-550, 550);
        DistanceBetweenColumnsOnY = UnityEngine.Random.Range(60, 70);
        LowerColumns[i].transform.localPosition = new Vector3(LowerColumns[i].transform.position.x,PositionYColumn1/100,LowerColumns[i].transform.position.z);
        UpperColumns[i].transform.localPosition = new Vector3(UpperColumns[i].transform.position.x,(PositionYColumn1/100)+DistanceBetweenColumnsOnY,UpperColumns[i].transform.position.z);
        GetComponent<MapGenerator>().Items.Add(LowerColumns[i]); 
        GetComponent<MapGenerator>().Items.Add(UpperColumns[i]); 

          if(MaterialColor == "Blue"){
            LowerColumns[i].transform.GetComponent<MeshRenderer>().material = ColumnMaterial1;
            LowerColumns[i].transform.Find("Quad").GetComponent<MeshRenderer>().material = ColumnMaterial1Up;
            UpperColumns[i].transform.GetComponent<MeshRenderer>().material = ColumnMaterial1;
            UpperColumns[i].transform.Find("Quad").GetComponent<MeshRenderer>().material = ColumnMaterial1Up;
            MaterialColor = "Red";

        }
        else{
            LowerColumns[i].transform.GetComponent<MeshRenderer>().material = ColumnMaterial2;
            LowerColumns[i].transform.Find("Quad").GetComponent<MeshRenderer>().material = ColumnMaterial2Up;
            UpperColumns[i].transform.GetComponent<MeshRenderer>().material = ColumnMaterial2;
            UpperColumns[i].transform.Find("Quad").GetComponent<MeshRenderer>().material = ColumnMaterial2Up;
            MaterialColor = "Blue";
           
        }

        positionZ += 20;

        if(i%3==0){
            GameObject NewCoin = Instantiate(Coin);
            NewCoin.transform.localPosition = new Vector3(NewCoin.transform.localPosition.x, LowerColumns[i].transform.localPosition.y+12, positionZ);
        
        
        }
        }
        }
        else if(GameMode == "OneHole"){
            ColumnOneHole.GetComponent<MeshRenderer>().enabled = true;
            for(int i=0;i<20;i++){
              
        PositionZColumnOneHole += 20;
        GameObject NewColumnOneHole = Instantiate(ColumnOneHole);
        GameObject NewScoreDetectorColumnOneHole = Instantiate(ScoreDetectorColumnOneHole);
        NewScoreDetectorColumnOneHole.transform.localPosition = new Vector3(NewColumnOneHole.transform.position.x,3,PositionZColumnOneHole+1);
        IndexOfObjectToDeactivate = UnityEngine.Random.Range(1, 9);
        NewColumnOneHole.transform.localPosition = new Vector3(NewColumnOneHole.transform.position.x,NewColumnOneHole.transform.position.y,PositionZColumnOneHole);
        GameObject ObjectToDeactivate = (NewColumnOneHole.transform.Find("Block"+ IndexOfObjectToDeactivate.ToString())).gameObject;
        ObjectToDeactivate.SetActive(false);
        GetComponent<MapGenerator>().Items.Add(NewColumnOneHole);  
         
        if(i%2==0){
        for(int j=1;j<10;j++){
        if(j%2==0){
            (NewColumnOneHole.transform.Find("Block"+ j.ToString())).gameObject.GetComponent<MeshRenderer>().material = ColumnMaterial2;
        }
        else{
            (NewColumnOneHole.transform.Find("Block"+ j.ToString())).gameObject.GetComponent<MeshRenderer>().material = ColumnMaterial1;
        }
        }
        }
        else{
            for(int j=1;j<10;j++){
            if(j%2==0){
                (NewColumnOneHole.transform.Find("Block"+ j.ToString())).gameObject.GetComponent<MeshRenderer>().material = ColumnMaterial3;
            }
            else{
                (NewColumnOneHole.transform.Find("Block"+ j.ToString())).gameObject.GetComponent<MeshRenderer>().material = ColumnMaterial4;
        }
        }
        }

        if(i%3==0){
            GameObject NewCoin = Instantiate(Coin);
            NewCoin.transform.localPosition = ObjectToDeactivate.transform.position;
            NewCoin.transform.localScale = new Vector3(1,1,1);
        }

        }
        }
         else if(GameMode == "TwoHoles"){
            for(int i=0;i<20;i++){
        PositionZColumnTwoHoles += 20;
        GameObject NewColumnTwoHoles = Instantiate(ColumnTwoHoles);
        GameObject NewScoreDetectorColumnOneHole = Instantiate(ScoreDetectorColumnTwoHoles);
        NewScoreDetectorColumnOneHole.transform.localPosition = new Vector3(NewColumnTwoHoles.transform.position.x,3,PositionZColumnTwoHoles+1);
        
        getRanNum1 = new System.Random().Next(1,10);
        getRanNum2 = new System.Random().Next(1,10);
        while(getRanNum2 == getRanNum1)
        getRanNum2 = new System.Random().Next(1,10);


        NewColumnTwoHoles.transform.localPosition = new Vector3(NewColumnTwoHoles.transform.position.x,NewColumnTwoHoles.transform.position.y,PositionZColumnTwoHoles);
        GameObject ObjetOneToDeactivate = (NewColumnTwoHoles.transform.Find("Block"+ getRanNum1.ToString())).gameObject;
        GameObject ObjetTwoToDeactivate = (NewColumnTwoHoles.transform.Find("Block"+ getRanNum2.ToString())).gameObject;
        ObjetOneToDeactivate.SetActive(false);
        ObjetTwoToDeactivate.SetActive(false);
        GetComponent<MapGenerator>().Items.Add(NewColumnTwoHoles);  

            if(i%2==0){
        for(int j=1;j<10;j++){
        if(j%2==0){
            (NewColumnTwoHoles.transform.Find("Block"+ j.ToString())).gameObject.GetComponent<MeshRenderer>().material = ColumnMaterial2;
        }
        else{
            (NewColumnTwoHoles.transform.Find("Block"+ j.ToString())).gameObject.GetComponent<MeshRenderer>().material = ColumnMaterial1;
        }
        }
        }
        else{
            for(int j=1;j<10;j++){
            if(j%2==0){
                (NewColumnTwoHoles.transform.Find("Block"+ j.ToString())).gameObject.GetComponent<MeshRenderer>().material = ColumnMaterial3;
            }
            else{
                (NewColumnTwoHoles.transform.Find("Block"+ j.ToString())).gameObject.GetComponent<MeshRenderer>().material = ColumnMaterial4;
        }
        }
        }

         if(i%3==0){
            GameObject NewCoin = Instantiate(Coin);
            NewCoin.transform.localPosition = ObjetOneToDeactivate.transform.position;
            NewCoin.transform.localScale = new Vector3(1,1,1);
        }

            }
        }
    }

    

   
}
