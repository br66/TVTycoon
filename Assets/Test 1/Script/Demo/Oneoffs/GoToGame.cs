using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Vision;

public class GoToGame : MonoBehaviour {

    public void GoToGameRoom()
    {
        GameObject nameBox = transform.parent.gameObject;
        Vision.WorldTime time = new Vision.WorldTime();
        if (nameBox.name == "Channel Prompt")
        {
            nameBox = nameBox.transform.Find("Name").gameObject;
            if (!String.IsNullOrEmpty(nameBox.GetComponentsInChildren<Text>().First(x => x.transform.gameObject.name == "Text").text))
            {
                GameObject sloganBox = nameBox.transform.parent.transform.Find("Slogan").gameObject;
                if (!String.IsNullOrEmpty(sloganBox.GetComponentsInChildren<Text>().First(x => x.transform.gameObject.name == "Text").text))
                {
                    SceneManager.LoadScene("Game");
                    //SceneManager.UnloadSceneAsync("demo"); // if needed
                    transform.parent.gameObject.SetActive(false);
                    return;
                }
            }
        }

        Debug.Log("fill the boxes");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
