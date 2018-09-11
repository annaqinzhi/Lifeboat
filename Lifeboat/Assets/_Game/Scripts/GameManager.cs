﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject boat;
    public GameObject jumperBPrefab;
    public GameObject jumperAPrefab;
    public List<GameObject> SavedObjects = new List<GameObject>();
    public GameOverController gameOverController;
    public CliffPointController cliffPointController;
    public PointsController pointsController;
    public MissPointsController missPointsController;

    public bool continueGame;
    public float spawnDelay = 5.0f;
    public float moveDelay = 0.4f;
    public int numOfJumperA = 0;
    public int numOfJumperB = 0;
    public int numOfLivesInBoat = 0;

    public int points = 0;
    public int pointsM = 0;

    private void OnEnable()
    {
        InputController.RightClick+=InputController_RightClick;
    }

    private void OnDisable()
    {
        InputController.RightClick -= InputController_RightClick;
    }




    void Start () {

        StartCoroutine(JumperASpawner());
        Invoke("StartB", 1.5f);
        Invoke("StopGame", 36f);

    }

    IEnumerator JumperASpawner(){
        while(continueGame){
            NewJumperA(moveDelay-(0.02f*points));
            yield return new WaitForSeconds(spawnDelay-(0.1f*points));
        }
    }

    void NewJumperA(float delay){
        numOfJumperA++;
        GameObject newJumperA = Instantiate(jumperAPrefab);
        JumperAController jumperAController = newJumperA.GetComponentInChildren<JumperAController>();
        jumperAController.gameManager = this;
        jumperAController.moveDelay = delay;
        Debug.Log("JumperA created " + numOfJumperA);
       

    }

    void StartB(){

        StartCoroutine(JumperBSpawner());
    }

    IEnumerator JumperBSpawner()
    {
        while (continueGame)
        {
            NewJumperB(moveDelay - (0.03f * points));
            yield return new WaitForSeconds(spawnDelay - (0.3f * points));
        }
    }

    void NewJumperB(float delay)
    {
        numOfJumperB++;
        GameObject newJumperB = Instantiate(jumperBPrefab);
        JumperBController jumperBController = newJumperB.GetComponentInChildren<JumperBController>();
        jumperBController.gameManager = this;
        jumperBController.moveDelay = delay;
        Debug.Log("JumperB created " + numOfJumperB);

    }

    public bool Saved(GameObject jumper){
        LayerMask mask = LayerMask.GetMask("Boat");
        RaycastHit2D hit = Physics2D.Raycast(jumper.transform.position, Vector2.down, Mathf.Infinity, mask);

        if (hit.collider!=null){
            return true;
        } else{
            return false;
        }

    }

    void InputController_RightClick()
    {
        OneLifeLeaveBoat();
    }


    void OneLifeLeaveBoat()
    {
        if (GameObject.FindWithTag("BOAT").transform.position
            == GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
            .positions[2].transform.position && numOfLivesInBoat != 0)

        {
            Debug.Log("There are  " + numOfLivesInBoat+ "lives in boat!");


            SavedObjects[numOfLivesInBoat-1].transform.position
                                        =GameObject.FindWithTag("CliffPoint").transform.position;
            cliffPointController.busyPoint = true;
            Debug.Log("Number"+(numOfLivesInBoat - 1) + " go to CliffPoint!");



            GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                      .places[numOfLivesInBoat-1].GetComponent<BusyPlace>().busyPlace = false;
            Debug.Log("Place become free! ");


            numOfLivesInBoat--;
            Debug.Log(numOfLivesInBoat + "  Lives left in boat! ");

            if (cliffPointController.busyPoint==true){
                Invoke("DestroyOneLife", 0.2f);
            }
        }

    }

    void DestroyOneLife(){

        Destroy(SavedObjects[numOfLivesInBoat]);
        Debug.Log("Number"+(numOfLivesInBoat)+" destroy!");

        SavedObjects.RemoveAt(numOfLivesInBoat);
        Debug.Log("Number"+(numOfLivesInBoat) + " removed från SavedObjects list!");

        addPoints();

    }

    public void addPoints(){
        points++;
        pointsController.SetPoint(points);
    }

    public void addMissPoints(){
        pointsM++;
        missPointsController.SetPoint(pointsM);

    }

    void StopGame(){
        continueGame = false;
        gameOverController.SetText();
        Debug.Log("Game Over!");
    }
}

