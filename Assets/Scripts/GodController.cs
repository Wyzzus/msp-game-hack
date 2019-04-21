using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodController : MonoBehaviour {
	
	public Text Current;
	public Text CreateButton;

	public Transform[] points;
	public GameObject AsteroidPrefab;

	public float tS;


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

		PlanetController.instance.PlanetTimeScale = tS;
		Current.text = tS.ToString("0.0");


		if(PlanetController.instance.PlanetSettings == null)
		{
			CreateButton.text = "Создать мир";
		}
		else
		{
			CreateButton.text = "Уничтожить мир";
		}

	}

	public void SetTime(float t)
	{
		tS = t;
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

	public void CreateAsteroid()
	{
		Instantiate<GameObject>(AsteroidPrefab, points[Random.Range(0, points.Length)].position, Quaternion.identity);
	}

	public void CreatePlanet()
	{
		PlanetController.instance.CreatePlanet();
	}
}
