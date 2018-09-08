using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject boat;
    public GameObject jumperBPrefab;
    public List<GameObject> SavedObjects = new List<GameObject>();
    public GameObject shorePoint;

    public bool continueGame;
    public float spawnDelay = 5.0f;
    public float moveDelay = 0.4f;
    public int numOfJumperB = 0;
    public int numOfLivesInBoat = 0;

    private void OnEnable()
    {
        InputController.RightClick+=InputController_RightClick;
    }

    private void OnDisable()
    {
        InputController.RightClick -= InputController_RightClick;
    }




    void Start () {

        StartCoroutine(JumperBSpawner());

	}

    IEnumerator JumperBSpawner(){
        while(continueGame){
            NewJumperB(moveDelay);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void NewJumperB(float delay){
        numOfJumperB++;
        GameObject newJumperB = Instantiate(jumperBPrefab);
        JumperBController jumperBController = newJumperB.GetComponentInChildren<JumperBController>();
        jumperBController.gameManager = this;
        jumperBController.moveDelay = delay;
        Debug.Log("Jumper created" + numOfJumperB);
       

    }

    public bool Saved(GameObject jumper){
        LayerMask mask = LayerMask.GetMask("Boat");
        RaycastHit2D hit = Physics2D.Raycast(jumper.transform.position, Vector2.down, Mathf.Infinity, mask);

        if(hit.collider!=null){
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
                                        =GameObject.FindWithTag("ShorePoint").transform.position;

         

            SavedObjects.RemoveAt(numOfLivesInBoat - 1);

            Debug.Log(numOfLivesInBoat+ "  Lives left in boat! ");

            GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                      .places[numOfLivesInBoat - 1].GetComponent<BusyPlace>().busyPlace = false;

            Debug.Log(" one take off boat!");

            numOfLivesInBoat--;
           

        }

    }
}

