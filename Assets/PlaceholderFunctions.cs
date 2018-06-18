using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class PlaceholderFunctions : MonoBehaviour {

    public int width = 2, height = 2;

    public float radius = 1f;

    public GameObject DataContext;

    public List<Collider2D> Colliders;

    public bool canAdd = false;

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

        #region for placing objects
        //raycast in all directions, find first objects that intersect, see if i'm right next to any of them
        Colliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Building")).ToList();

        int i = 0;

        var list = Colliders.Select(x => x.gameObject).ToList();

        for (i = 0; i < list.Count(); i++)
        {
            if (name != list[i].name && list.Count < 3)
            {
                //Debug.Log(transform.position.x + " || " + (ject.transform.position.x + ject.GetComponent<SpriteRenderer>().bounds.size.x) + " || " + (1 + ject.GetComponent<SpriteRenderer>().bounds.size.x));

                // if on left side
                if (Mathf.Abs(transform.position.x - (list[i].transform.position.x + list[i].GetComponent<SpriteRenderer>().bounds.size.x)) == (1 + list[i].GetComponent<SpriteRenderer>().bounds.size.x) && Mathf.Abs(transform.position.y - list[i].transform.position.y) == 0)
                {
                    Debug.Log("right next to each other, left side");
                    continue;
                }
                // if on right side
                else if (Mathf.Abs(transform.position.x - (list[i].transform.position.x + list[i].GetComponent<SpriteRenderer>().bounds.size.x)) == 0 && Mathf.Abs(transform.position.y - list[i].transform.position.y) == 0)
                {
                    Debug.Log("right next to each other, right side");
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        if (i != list.Count())
            canAdd = false;
        else
            canAdd = true;

        #endregion

        if (Input.GetMouseButtonDown((int)PointerEventData.InputButton.Left) && canAdd == true)
        {
            Debug.Log("doof");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnMouseDown()
    {
        Instantiate(DataContext, transform.position, transform.rotation);
    }
}
