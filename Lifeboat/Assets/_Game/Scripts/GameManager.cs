using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject boat;
    public GameObject jumperBPrefab;

    public bool continueGame;
    public float spawnDelay = 5.0f;
    public float moveDelay = 0.4f;


    Collider2D BoatCollider;

    void Start () {

        BoatCollider = boat.GetComponentInChildren<Collider2D>();
        StartCoroutine(JumperBSpawner());

	}

    IEnumerator JumperBSpawner(){
        while(continueGame){
            NewJumperB(moveDelay);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void NewJumperB(float delay){
        GameObject newJumperB = Instantiate(jumperBPrefab);
        JumperBController jumperBController = newJumperB.GetComponentInChildren<JumperBController>();
        jumperBController.gameManager = this;
        jumperBController.moveDelay = delay;

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


}
