using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour {

    public Canvas startGame;
    public Button playText;

    void Start () {
        startGame = GetComponent<Canvas>();
        playText = GetComponent<Button>();
		
	}

    public void PlayPress(){
        SceneManager.LoadScene("Scene");
        
    }

}
