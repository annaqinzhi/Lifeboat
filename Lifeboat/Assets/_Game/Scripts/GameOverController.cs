using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour {

    public TextMeshPro textMeshOver;

    private void Start()
    {
        textMeshOver = GetComponent<TextMeshPro>();
        textMeshOver.SetText("");

        if (textMeshOver == null)
        {
            Debug.LogError("Textmesh component not found!");
        }
    }

    public void SetText()
    {
        textMeshOver.SetText("Game Stopped!");
    }

}
