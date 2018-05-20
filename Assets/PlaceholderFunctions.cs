using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderFunctions : MonoBehaviour {

    int width = 2, height = 2;

    public GameObject DataContext;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // find the mouse
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        // get the nearest grid position
        mouseX = Mathf.Floor(mouseX / width) * width;
        mouseY = Mathf.Floor(mouseY / height) * height;

        // change my position to that
        gameObject.transform.position = new Vector3(mouseX, mouseY);


    }

    private void OnMouseDown()
    {
        //var overlapping = 
        Instantiate(DataContext, transform.position, transform.rotation);
    }
}
