using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissPointsController : MonoBehaviour {

    public TextMeshPro textMeshM;

    private void Start()
    {
        textMeshM = GetComponent<TextMeshPro>();
        textMeshM.SetText("0");

        if (textMeshM == null)
        {
            Debug.LogError("Textmesh component not found!");
        }
    }

    public void SetPoint(int points)
    {
        textMeshM.SetText(points.ToString());
    }

}
