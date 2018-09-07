using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBController : MonoBehaviour
{

    [HideInInspector]
    public GameManager gameManager;
    public Transform positionsB;

    int currentPosition = 0;

    [HideInInspector]
    public float moveDelay = 0.4f;

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

        if (currentPosition <= positionsB.childCount - 1)
        {
            transform.position = positionsB.GetChild(currentPosition).transform.position;
            if (positionsB.GetChild(currentPosition).GetComponent<DangerPosition>().dangerPosition)
            {
                Debug.Log("B Dangerous!");
                MoveInBoat();
            }
        }

        if (currentPosition > positionsB.childCount - 1)
        {
            currentPosition = 0;
        }

    }

    void MoveInBoat()
    {

        if (gameManager.Saved(gameObject) && getFreePosInBoat() != -1)
        {

            transform.position = GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                .places[getFreePosInBoat()].transform.position;

            Debug.Log("getFreePosition" + getFreePosInBoat());
        }
        else
        {
            Debug.Log("Die!");
            Die();
        }
    }

    public int getFreePosInBoat()
    {

        for (int i = 0; i < 3; i++)
        {
            bool busyPos = GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                .places[i].GetComponent<BusyPosition>().busyPosition;

            Debug.Log("getFreePosition" + i);

            if (!busyPos)
            {
                GameObject.FindWithTag("BOAT").GetComponent<BoatController>()
                .places[i].GetComponent<BusyPosition>().busyPosition = true;

                return i;
            }
        }
        return -1;
    }
    


    void Die(){
        Destroy(transform.parent.gameObject);
    }

}