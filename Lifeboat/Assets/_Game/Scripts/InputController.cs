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

        //#if UNITY_EDITOR

        //        //for mouse
        //        if (Input.GetMouseButtonDown(0))
        //        {

        //            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //            if (LeftClick != null && pos.x < -ClickMargin)
        //            {
        //                LeftClick();
        //                Debug.Log("LeftClicked! " + pos.x);
        //            }

        //            else if (RightClick != null && pos.x > ClickMargin)
        //            {
        //                RightClick();
        //                Debug.Log("RightClicked! " + pos.x);
        //            }

        //        }


        //#endif

#if UNITY_IOS

        //for touch
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                if (LeftClick != null && pos.x < -ClickMargin)
                {
                    LeftClick();
                    Debug.Log("LeftTouch in IOS " + pos.x);
                }
                else if (RightClick != null && pos.x > ClickMargin)
                {
                    RightClick();
                    Debug.Log("RightTouch in IOS " + pos.x);
                }
            }
        }

#endif
    }
}
