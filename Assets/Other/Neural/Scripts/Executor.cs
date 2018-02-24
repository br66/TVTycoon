using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int[] layers = new int[] { 1, 10, 10, 1 };

        NeuralNetwork network = new NeuralNetwork(layers);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
