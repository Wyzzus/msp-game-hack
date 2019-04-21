using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodController : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			DeleteObject();
		}
	}

	public void DeleteObject()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if(hit.collider != null)
		{
			if(hit.collider.tag == "Deleteable")
			{
				Destroy(hit.collider.gameObject);
			}
		}
	}
}
