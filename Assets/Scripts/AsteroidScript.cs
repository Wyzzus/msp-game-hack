using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {

	public GameObject CraterPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log(other.collider.name);
		if(other.collider.tag == "Planet")
		{
			Debug.Log(1);
			CreateCrater();
			Destroy(gameObject);
		}
	}

	public void CreateCrater()
	{
		GameObject clone = Instantiate<GameObject>(CraterPrefab, transform.position, transform.rotation);
		clone.transform.parent = PlanetController.instance.PlanetSettings.CliffsParent;
		Destroy(clone, 5f);
	}
}
