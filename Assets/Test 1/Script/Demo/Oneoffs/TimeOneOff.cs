using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vision;

public class TimeOneOff : MonoBehaviour {
    WorldTime time;

    // Use this for initialization
    void Start () {
        time = new WorldTime();
	}
	
	// Update is called once per frame
	void Update () {
        time.Update();
        GetComponent<Text>().text = time.Year + "  " + time.Quarter + "  " + time.Month + "  " + time.Day + "  " + String.Empty;
	}
}
