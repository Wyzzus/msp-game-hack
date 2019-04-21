using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeScript : MonoBehaviour {

	public SpriteRenderer Flag;
	public Color color;

	public GameObject VillagerPrefab;

	public List<Villager> MyVillagers = new List<Villager>();
	public bool Empty = false;

	float dir = 0;

	public float delay = 0;
	public float SpawnTime = 10;

	public float Health;

	// Use this for initialization
	void Start () 
	{
		Setup(Random.ColorHSV());
	}

	public void Setup(Color color)
	{
		Health = 200;
		this.color = color;
		this.color.a = 1;
		Flag.color = color;
		SpawnVillager();
	}
	
	// Update is called once per frame
	void Update ()
	{
		delay += Time.deltaTime;
		if(Health < 200)
		{
			Health += Time.deltaTime * 0.1f;
		}
		if(delay > SpawnTime)
		{
			delay = 0;
			SpawnVillager();
		}

		if(MyVillagers.Count < 1)
		{
			Empty = true;
		}
	}

	public void OnDestroy()
	{
		foreach(Villager vil in MyVillagers)
		{
			if(vil != null)
				Destroy(vil.gameObject);
		}
	}

	public void SpawnVillager()
	{
		dir = Random.Range(-0.1f, 0.1f);
		GameObject clone = Instantiate<GameObject>(VillagerPrefab, transform.position + Vector3.right * dir, Quaternion.identity);

		Villager vil = clone.GetComponent<Villager>();
		MyVillagers.Add(vil);
		vil.myHG.color = color;
		vil.Home = this;
		clone.transform.parent = PlanetController.instance.PlanetSettings.LifeParent;
	}
}
