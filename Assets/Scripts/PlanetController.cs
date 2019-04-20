using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

    public static PlanetController instance;

	public void Awake()
	{
        instance = this;
	}

    public GameObject PlanetPrefab;
	public Transform Planet;
    public GravityAttractor MainAttractor;

    public float RotationSpeed;
    public float OrbitSpeed;

    public Quaternion LastQuaternion;
    public int Year = 0;

	// Use this for initialization
	void Start () {
        
        LastQuaternion = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Year++;
        if(Planet)
            Rotate();

        if(Input.GetKeyDown(KeyCode.R))
        {
            CreatePlanet();
        }
	}

    public void Rotate()
    {
        float xAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(xAxis) > 0.01f)
            Planet.Rotate(Vector3.forward * Time.deltaTime * RotationSpeed * -xAxis);
        else
            Planet.Rotate(Vector3.forward * Time.deltaTime * OrbitSpeed);
    }

    public void CreatePlanet()
    {
        OrbitSpeed = Random.Range(2, 7);
        int coin = Random.Range(0, 2);
        if (coin == 0)
            OrbitSpeed *= -1;
        if (Planet)
        {
            LastQuaternion = Planet.rotation;
            Destroy(Planet.gameObject);

        }
        GameObject clone = Instantiate<GameObject>(PlanetPrefab, transform.position, Quaternion.identity);
        Planet = clone.transform;
        Planet.rotation = LastQuaternion;
        MainAttractor = clone.GetComponent<GravityAttractor>();
    }
}
