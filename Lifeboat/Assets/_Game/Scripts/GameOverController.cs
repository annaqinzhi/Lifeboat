using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour {

    public TextMeshProUGUI textMeshOver;

    private void Start()
    {
        textMeshOver = GetComponent<TextMeshProUGUI>();
        textMeshOver.SetText("");

        if (textMeshOver == null)
        {
            Debug.LogError("Textmesh component not found!");
        }
    }

    public void SetText()
    {
        textMeshOver.SetText("Game Over!");
    }

}
