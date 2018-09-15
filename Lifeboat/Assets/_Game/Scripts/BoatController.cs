using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public List<Transform> positions = new List<Transform>();
    public int currentPosition = 1;
    public List<Transform> places = new List<Transform>();



    void OnEnable()
    {
        InputController.LeftClick += InputController_LeftClick;
        InputController.RightClick += InputController_RightClick;

    }

    void OnDisable()
    {

        InputController.LeftClick -= InputController_LeftClick;
        InputController.RightClick -= InputController_RightClick;

    }

    private void Start()
    {
        transform.position = positions[currentPosition].transform.position;
    }

    void InputController_LeftClick()
    {
        if (currentPosition > 0)
        {
            currentPosition--;
            transform.position = positions[currentPosition].transform.position;
   
        }


        }

        void InputController_RightClick()
        {
            if (currentPosition < positions.Count - 1)
            {
                currentPosition++;
                transform.position = positions[currentPosition].transform.position;
                
            }

        }



   
}
