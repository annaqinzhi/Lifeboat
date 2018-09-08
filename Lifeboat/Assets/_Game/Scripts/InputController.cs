using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public delegate void ClickAction();
    public static event ClickAction LeftClick;
    public static event ClickAction RightClick;

    public float ClickMargin = 1;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

//for mouse
        if (Input.GetMouseButtonDown(0)){

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (LeftClick != null && pos.x < -ClickMargin)
                LeftClick();

            else if ( RightClick !=null && pos.x > ClickMargin ){
                RightClick();
            }
                    
                }

        //for touch
        foreach (Touch touch in Input.touches){

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.acceleration);
                if (LeftClick != null & pos.x < -ClickMargin)
                    LeftClick();
                else if (RightClick != null && pos.x > ClickMargin)
                {
                    RightClick();
                }
            }
        }


#else



#endif


    }
}
