using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBController : MonoBehaviour
{

    [HideInInspector]
    public GameManager gameManager;
    public Transform positionsB;
    public int xPos;
    public int xJumper;

    int currentPosition = 0;

    [HideInInspector]
    public float moveDelay = 0.1f;


    void Start()
    {
        transform.position = positionsB.GetChild(currentPosition).transform.position;

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
        int freePlace = GetFreePlaceInBoat();

        if (currentPosition < positionsB.childCount)
        {
            transform.position = positionsB.GetChild(currentPosition).transform.position;
        }

        if (currentPosition == positionsB.childCount - 1)
        {
            if (gameManager.Saved(gameObject) && freePlace != -1)
            {

                transform.position = GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                    .places[freePlace].transform.position;


                Debug.Log("getFreePlace " + freePlace);

                GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                          .places[freePlace].GetComponent<BusyPlace>().busyPlace = true;

                xPos = freePlace;
                gameManager.SavedObjects.Add(gameObject);
                gameManager.numOfLivesInBoat++;

            }
            else if (!gameManager.Saved(gameObject) || freePlace == -1 )

            {
                Debug.Log("Die!");
                Die();
            }
        }

        if (currentPosition > positionsB.childCount)
        {

            if (transform.position == GameObject.FindWithTag("ShorePoint").transform.position)
            {
                Die();
                Debug.Log("One jumper have left to shorepoint");
            }

            transform.position = GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                .places[xPos].transform.position;
           

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





 


