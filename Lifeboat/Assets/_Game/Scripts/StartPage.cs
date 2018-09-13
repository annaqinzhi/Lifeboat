using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour {

    public Canvas startPage;
    public Button playText;

    void Start () {

        startPage = GetComponent<Canvas>();
        playText = GetComponent<Button>();
		
	}

    public void PlayPress(){
        SceneManager.LoadScene("Scene");
        
    }

}
