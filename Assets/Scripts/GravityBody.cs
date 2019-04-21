using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour {

    public GravityAttractor attractor;

	// Use this for initialization
	void Start () 
    {
        attractor = PlanetController.instance.MainAttractor;
	}
	
	// Update is called once per frame
	void Update () 
    {
        attractor.Attract(transform);
	}
}
