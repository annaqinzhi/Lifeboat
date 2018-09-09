using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsController : MonoBehaviour {

    public TextMeshPro textMesh;

	private void Start () {
        textMesh = GetComponent<TextMeshPro>();
        textMesh.SetText("0");

        if(textMesh==null){
            Debug.LogError("Textmesh component not found!");
        }
	}

    public void SetPoint(int points){
        textMesh.SetText(points.ToString());
    }

}
