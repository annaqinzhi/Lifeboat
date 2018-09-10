using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAController : MonoBehaviour {
    [HideInInspector]
    public GameManager gameManager;
    public Transform positionsA;

    [HideInInspector]
    public int xPos;

    int currentPosition = 0;

    [HideInInspector]
    public float moveDelay = 0.1f;


    void Start()
    {
        transform.position = positionsA.GetChild(currentPosition).transform.position;
        Debug.Log("Start currentPosition " + currentPosition);

        StartCoroutine(Move());


    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveDelay);
            MoveToNext();

        }
    }

    void MoveToNext()
    {
        currentPosition++;
        Debug.Log("currentPosition A " + currentPosition);

        int freePlace = GetFreePlaceInBoat();

        if (currentPosition < positionsA.childCount)
        {
            transform.position = positionsA.GetChild(currentPosition).transform.position;
        }

        if (currentPosition == positionsA.childCount - 1)
        {
            if (gameManager.Saved(gameObject) && freePlace != -1)
            {
                //get the free place in the boat and attach this jumper to the object "BOAT"
                transform.position = GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                .places[freePlace].transform.position;

                Debug.Log("getFreePlace " + freePlace);

                GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                          .places[freePlace].GetComponent<BusyPlace>().busyPlace = true;

                gameManager.numOfLivesInBoat++;

                gameManager.SavedObjects.Add(gameObject);

                gameObject.transform.SetParent(GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                                              .places[freePlace].transform);


                xPos = freePlace;

            }
            else if (!gameManager.Saved(gameObject) || freePlace == -1)

            {
                gameManager.addMissPoints();

                Debug.Log("A Die!"+!gameManager.Saved(gameObject)+freePlace);
                Die();


            }
        }


    }

    public int GetFreePlaceInBoat()
    {

        for (int i = 0; i < 3; i++)
        {
            bool busyPos = GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                                     .places[i].GetComponent<BusyPlace>().busyPlace;

            if (!busyPos)
                return i;

        }
        return -1;
    }

    void Die()
    {
        Destroy(transform.parent.gameObject);


    }


}
