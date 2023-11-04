using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Terrain;
    public GameObject ScoreDetectorColumn;

    [SerializeField] GameObject ColumnLower;
    [SerializeField] GameObject ColumnUpper;

    [SerializeField] GameObject ColumnOneHole;
    [SerializeField] GameObject ColumnTwoHoles;

    [SerializeField] GameObject ClassicMode;

    [SerializeField] GameObject OneHoleMode;
    [SerializeField] GameObject ScoreDetectorColumnOneHole;
    [SerializeField] GameObject OneHoleBlocksTerrainDown;
    [SerializeField] GameObject OneHoleBlocksTerrainUp;
    [SerializeField] GameObject OneHoleBlocksTerrainLeft;
    [SerializeField] GameObject OneHoleBlocksTerrainRight;

    [SerializeField] GameObject TwoHolesMode;
    [SerializeField] GameObject ScoreDetectorColumnTwoHoles;
    [SerializeField] GameObject OneHoleBlocksTerrainDownTwoHoles;
    [SerializeField] GameObject OneHoleBlocksTerrainUpTwoHoles;
    [SerializeField] GameObject OneHoleBlocksTerrainLeftTwoHoles;
    [SerializeField] GameObject OneHoleBlocksTerrainRightTwoHoles;

    [SerializeField] GameObject Coin;
    [SerializeField] GameObject Map;
    [SerializeField] GameObject GlowBall;

    public List<GameObject> Items = new List<GameObject>();
    
    public float timeUntilNextTerrainIsSpawned = 1;
    float timeUntilNextTerrainIsSpawnedDeceleration = 0.001f;

    public float timeUntilNextColumnIsSpawned = 0.5f;
    float timeUntilNextColumnIsSpawnedDeceleration = 0.001f;

    public float timeUntilNextColumnIsSpawnedHole = 1;
    float timeUntilNextColumnIsSpawnedHoleDeceleration = 0.001f;

    float PositionZTerrain = 297;
    float PositionZTerrainHole = 0;
    public float PositionZColumn = 300;
    float PositionZColumnOneHole = 420;
    float PositionZColumnTwoHoles = 420;
    float PositionYColumn1;
    float PositionYColumn2;
    float DistanceBetweenColumnsOnY;

    float timer1 = 0;
    float timer2 = 0;
    float timer3 = 0;
    float timer4 = 0;
    float timer5 = 0;
    float timer6 = 0;

    int IndexOfObjectToDeactivate;
    int ColumnCounter;
    int ColumnCounterOneHole;
    int ColumnCounterTwoHoles;
    int i1;
    int i2;

    public int getRanNum1; 
    public int getRanNum2; 

    public string GameMode;

    [SerializeField] Material ColumnMaterial1;
    [SerializeField] Material ColumnMaterial2;
    [SerializeField] Material ColumnMaterial3;
    [SerializeField] Material ColumnMaterial4;

    [SerializeField] Material ColumnMaterial1Up;
    [SerializeField] Material ColumnMaterial2Up;

    string MaterialColor = "Blue";

    public bool isPlaying;


    void Update(){

        isPlaying = GetComponent<Pause>().isPlaying;

         if(isPlaying){
        timeUntilNextTerrainIsSpawned -= timeUntilNextTerrainIsSpawnedDeceleration;
        timeUntilNextColumnIsSpawned -= timeUntilNextColumnIsSpawnedDeceleration;
        timeUntilNextColumnIsSpawnedHole -= timeUntilNextColumnIsSpawnedHoleDeceleration;
        }

        if(GameMode != "Classic"){
        if(isPlaying){
        if(Items[0]){
        if(GlowBall.transform.localPosition.z-23>Items[0].transform.localPosition.z){
            Items.Remove(Items[0]);
            Items[0].SetActive(false);
          
        }
        }
        }
        }

        if(GameMode == "Classic"){
            ClassicMode.SetActive(true);
            OneHoleMode.SetActive(false);
            TwoHolesMode.SetActive(false);

           
            timer1 -= Time.deltaTime;
            if (timer1 <= 0)
            {
               timer1 = timeUntilNextTerrainIsSpawned;
               StartCoroutine(TerrainGenerator());
            }

            
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                timer2 = timeUntilNextColumnIsSpawned;
                StartCoroutine(ColumnGenerator());
            }
        }
        else if(GameMode == "OneHole"){
            ClassicMode.SetActive(false);
            OneHoleMode.SetActive(true);
            TwoHolesMode.SetActive(false);

            timer3 -= Time.deltaTime;
            if (timer3 <= 0)
            {
                timer3 = timeUntilNextColumnIsSpawnedHole;
                StartCoroutine(ColumnGeneratorOneHole());
            }

             timer4 -= Time.deltaTime;
            if (timer4 <= 0)
            {
                timer4 = timeUntilNextTerrainIsSpawned;
                StartCoroutine(TerrainGeneratorOneHole());
            }
        }

           else if(GameMode == "TwoHoles"){
            ClassicMode.SetActive(false);
            OneHoleMode.SetActive(false);
            TwoHolesMode.SetActive(true);

            timer5 -= Time.deltaTime;
            if (timer5 <= 0)
            {
                timer5 = timeUntilNextColumnIsSpawnedHole;
                StartCoroutine(ColumnGeneratorTwoHoles());
            }

            timer6 -= Time.deltaTime;
            if (timer6 <= 0)
            {
                timer6 = timeUntilNextTerrainIsSpawned;
                StartCoroutine(TerrainGeneratorTwoHoles());
            }
        }
    }

    public IEnumerator TerrainGenerator(){
        yield return new WaitForSeconds(0);
        GameObject NewTerrain = Instantiate(Terrain);
        PositionZTerrain += 27;
        NewTerrain.transform.position = new Vector3(0,-5,PositionZTerrain);
        NewTerrain.transform.SetParent(Map.transform);
        NewTerrain.name = "Terrain";
    }

      public IEnumerator ColumnGenerator(){
        ColumnCounter++;
        yield return new WaitForSeconds(0);
        PositionYColumn1 = Random.Range(-550, 550);
        DistanceBetweenColumnsOnY = Random.Range(60, 70);
        GameObject NewColumn1 = Instantiate(ColumnLower);
        GameObject NewColumn2 = Instantiate(ColumnUpper);
        GameObject NewScoreDetectorColumn = Instantiate(ScoreDetectorColumn);
        NewColumn1.transform.SetParent(Map.transform);
        NewColumn2.transform.SetParent(Map.transform);
        NewScoreDetectorColumn.transform.SetParent(Map.transform);
        PositionZColumn += 20;
        NewColumn1.transform.localPosition = new Vector3(NewColumn1.transform.position.x,PositionYColumn1/100,PositionZColumn);
        NewColumn2.transform.localPosition = new Vector3(NewColumn2.transform.position.x,(PositionYColumn1/100)+DistanceBetweenColumnsOnY,PositionZColumn);
        NewScoreDetectorColumn.transform.localPosition = new Vector3(NewColumn2.transform.position.x,0,PositionZColumn+5);
      

          if(MaterialColor == "Blue"){
            NewColumn1.transform.GetComponent<MeshRenderer>().material = ColumnMaterial1;
            NewColumn1.transform.Find("Quad").GetComponent<MeshRenderer>().material = ColumnMaterial1Up;
            NewColumn2.transform.GetComponent<MeshRenderer>().material = ColumnMaterial1;
            NewColumn2.transform.Find("Quad").GetComponent<MeshRenderer>().material = ColumnMaterial1Up;
            MaterialColor = "Red";

        }
        else{
            NewColumn1.transform.GetComponent<MeshRenderer>().material = ColumnMaterial2;
            NewColumn1.transform.Find("Quad").GetComponent<MeshRenderer>().material = ColumnMaterial2Up;
            NewColumn2.transform.GetComponent<MeshRenderer>().material = ColumnMaterial2;
            NewColumn2.transform.Find("Quad").GetComponent<MeshRenderer>().material = ColumnMaterial2Up;
            MaterialColor = "Blue";
           
        }
    
          if(ColumnCounter%3==0){
              if(Coin){
            GameObject NewCoin = Instantiate(Coin);
            NewCoin.transform.SetParent(Map.transform);
            NewCoin.transform.localPosition = new Vector3(NewCoin.transform.localPosition.x, (NewColumn1.transform.localPosition.y+NewColumn2.transform.localPosition.y)/2, PositionZColumn);

              }
            
        
        }
    
    }

    public IEnumerator ColumnGeneratorOneHole(){
        ColumnCounterOneHole++;
        yield return new WaitForSeconds(0);
        PositionZColumnOneHole += 20;
        GameObject NewColumnOneHole = Instantiate(ColumnOneHole);
        GameObject NewScoreDetectorColumnOneHole = Instantiate(ScoreDetectorColumnOneHole);
        NewColumnOneHole.transform.SetParent(OneHoleMode.transform);
        NewScoreDetectorColumnOneHole.transform.SetParent(OneHoleMode.transform);
        IndexOfObjectToDeactivate = Random.Range(1, 9);
        NewColumnOneHole.transform.localPosition = new Vector3(NewColumnOneHole.transform.position.x,NewColumnOneHole.transform.position.y,PositionZColumnOneHole);
        GameObject ObjectToDeactivate = (NewColumnOneHole.transform.Find("Block"+ IndexOfObjectToDeactivate.ToString())).gameObject;
        ObjectToDeactivate.gameObject.SetActive(false);
        NewScoreDetectorColumnOneHole.transform.localPosition = new Vector3(NewColumnOneHole.transform.position.x,3,PositionZColumnOneHole+1);
        Items.Add(NewColumnOneHole);
        i2++;

         if(i2%2==0){
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

          if(ColumnCounterOneHole%3==0){
            GameObject NewCoin = Instantiate(Coin);
            NewCoin.transform.SetParent(OneHoleMode.transform);
            NewCoin.transform.localPosition = ObjectToDeactivate.transform.position;
            NewCoin.transform.localScale = new Vector3(1,1,1);
        
        }

    }

    public IEnumerator TerrainGeneratorOneHole(){
        yield return new WaitForSeconds(0);
        GameObject NewOneHoleBlocksTerrainDown = Instantiate(OneHoleBlocksTerrainDown);
        GameObject NewOneHoleBlocksTerrainUp = Instantiate(OneHoleBlocksTerrainUp);
        GameObject NewOneHoleBlocksTerrainLeft = Instantiate(OneHoleBlocksTerrainLeft);
        GameObject NewOneHoleBlocksTerrainRight = Instantiate(OneHoleBlocksTerrainRight);
        NewOneHoleBlocksTerrainDown.transform.SetParent(OneHoleMode.transform);
        NewOneHoleBlocksTerrainUp.transform.SetParent(OneHoleMode.transform);
        NewOneHoleBlocksTerrainLeft.transform.SetParent(OneHoleMode.transform);
        NewOneHoleBlocksTerrainRight.transform.SetParent(OneHoleMode.transform);
        PositionZTerrainHole += 27;
        NewOneHoleBlocksTerrainDown.transform.localPosition = new Vector3(NewOneHoleBlocksTerrainDown.transform.localPosition.x,NewOneHoleBlocksTerrainDown.transform.localPosition.y,PositionZTerrainHole);
        NewOneHoleBlocksTerrainUp.transform.localPosition = new Vector3(NewOneHoleBlocksTerrainUp.transform.localPosition.x,NewOneHoleBlocksTerrainUp.transform.localPosition.y,PositionZTerrainHole);
        NewOneHoleBlocksTerrainLeft.transform.localPosition = new Vector3(NewOneHoleBlocksTerrainLeft.transform.localPosition.x,NewOneHoleBlocksTerrainLeft.transform.localPosition.y,PositionZTerrainHole);
        NewOneHoleBlocksTerrainRight.transform.localPosition = new Vector3(NewOneHoleBlocksTerrainRight.transform.localPosition.x,NewOneHoleBlocksTerrainRight.transform.localPosition.y,PositionZTerrainHole);
    }


     public IEnumerator ColumnGeneratorTwoHoles(){
        ColumnCounterTwoHoles++;
        yield return new WaitForSeconds(0);
        PositionZColumnTwoHoles += 20;
        GameObject NewColumnTwoHoles = Instantiate(ColumnTwoHoles);
        GameObject NewScoreDetectorColumnOneHole = Instantiate(ScoreDetectorColumnTwoHoles);
        GameObject NewScoreDetectorColumnTwoHoles = Instantiate(ScoreDetectorColumnTwoHoles);
        NewColumnTwoHoles.transform.SetParent(TwoHolesMode.transform);
        NewScoreDetectorColumnOneHole.transform.SetParent(TwoHolesMode.transform);
        NewScoreDetectorColumnTwoHoles.transform.SetParent(TwoHolesMode.transform);
        NewScoreDetectorColumnTwoHoles.transform.localPosition = new Vector3(NewColumnTwoHoles.transform.position.x,3,PositionZColumnTwoHoles+1);
        Items.Add(NewColumnTwoHoles);
        getRanNum1 = new System.Random().Next(1,10);
        getRanNum2 = new System.Random().Next(1,10);
        while(getRanNum2 == getRanNum1)
        getRanNum2 = new System.Random().Next(1,10);
        i1++;
        
        NewColumnTwoHoles.transform.localPosition = new Vector3(NewColumnTwoHoles.transform.position.x,NewColumnTwoHoles.transform.position.y,PositionZColumnTwoHoles);
        GameObject ObjetOneToDeactivate = (NewColumnTwoHoles.transform.Find("Block"+ getRanNum1.ToString())).gameObject;
        GameObject ObjetTwoToDeactivate = (NewColumnTwoHoles.transform.Find("Block"+ getRanNum2.ToString())).gameObject;
        ObjetOneToDeactivate.SetActive(false);
        ObjetTwoToDeactivate.SetActive(false);
    
            if(i1%2==0){
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

          if(ColumnCounterTwoHoles%3==0){
            GameObject NewCoin = Instantiate(Coin);
            NewCoin.transform.localPosition = ObjetOneToDeactivate.transform.position;
            NewCoin.transform.localScale = new Vector3(1,1,1);
        }

    }


       public IEnumerator TerrainGeneratorTwoHoles(){
        yield return new WaitForSeconds(0);
        GameObject NewOneHoleBlocksTerrainDownTwoHoles = Instantiate(OneHoleBlocksTerrainDownTwoHoles);
        GameObject NewOneHoleBlocksTerrainUpTwoHoles = Instantiate(OneHoleBlocksTerrainUpTwoHoles);
        GameObject NewOneHoleBlocksTerrainLeftTwoHoles = Instantiate(OneHoleBlocksTerrainLeftTwoHoles);
        GameObject NewOneHoleBlocksTerrainRightTwoHoles = Instantiate(OneHoleBlocksTerrainRightTwoHoles);
        NewOneHoleBlocksTerrainDownTwoHoles.transform.SetParent(TwoHolesMode.transform);
        NewOneHoleBlocksTerrainUpTwoHoles.transform.SetParent(TwoHolesMode.transform);
        NewOneHoleBlocksTerrainLeftTwoHoles.transform.SetParent(TwoHolesMode.transform);
        NewOneHoleBlocksTerrainRightTwoHoles.transform.SetParent(TwoHolesMode.transform);
        PositionZTerrainHole += 27;
        NewOneHoleBlocksTerrainDownTwoHoles.transform.localPosition = new Vector3(NewOneHoleBlocksTerrainDownTwoHoles.transform.localPosition.x,NewOneHoleBlocksTerrainDownTwoHoles.transform.localPosition.y,PositionZTerrainHole);
        NewOneHoleBlocksTerrainUpTwoHoles.transform.localPosition = new Vector3(NewOneHoleBlocksTerrainUpTwoHoles.transform.localPosition.x,NewOneHoleBlocksTerrainUpTwoHoles.transform.localPosition.y,PositionZTerrainHole);
        NewOneHoleBlocksTerrainLeftTwoHoles.transform.localPosition = new Vector3(NewOneHoleBlocksTerrainLeftTwoHoles.transform.localPosition.x,NewOneHoleBlocksTerrainLeftTwoHoles.transform.localPosition.y,PositionZTerrainHole);
        NewOneHoleBlocksTerrainRightTwoHoles.transform.localPosition = new Vector3(NewOneHoleBlocksTerrainRightTwoHoles.transform.localPosition.x,NewOneHoleBlocksTerrainRightTwoHoles.transform.localPosition.y,PositionZTerrainHole);
    }
    
}
