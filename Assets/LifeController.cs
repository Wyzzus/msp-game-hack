using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {

    public GameObject AnimalPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create(GameObject creature, Vector2 cords)
    {
        GameObject clone = Instantiate<GameObject>(creature, cords, Quaternion.identity);
        clone.transform.parent = PlanetController.instance.PlanetSettings.LifeParent;
    }
}
