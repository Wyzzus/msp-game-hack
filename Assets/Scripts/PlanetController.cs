using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

    public static PlanetController instance;

	public void Awake()
	{
        instance = this;
	}

	public Transform Planet;
    public GravityAttractor MainAttractor;

    public float RotationSpeed;
    public float OrbitSpeed;

	// Use this for initialization
	void Start () {
        OrbitSpeed = Random.Range(2, 7);
        int coin = Random.Range(0, 2);
        if (coin == 0)
            OrbitSpeed *= -1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Rotate();
	}

    public void Rotate()
    {
        float xAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(xAxis) > 0.01f)
            Planet.Rotate(Vector3.forward * Time.deltaTime * RotationSpeed * -xAxis);
        else
            Planet.Rotate(Vector3.forward * Time.deltaTime * OrbitSpeed);
    }
}
