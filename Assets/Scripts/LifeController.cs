using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {

    public GameObject AnimalPrefab;
	public GameObject TribePrefab;
	public List<Animal> Animals = new List<Animal>();
	public List<TribeScript> Tribez = new List<TribeScript>();
	public int Epoch = 0;
	PlanetGenerator pg;
	// Use this for initialization
	void Start () {
		pg = PlanetController.instance.PlanetSettings;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(Epoch)
		{
			case 0:
				Animals = new List<Animal>(GetComponentsInChildren<Animal>());
			if(Animals.Count > 0 && CanSetTribez())
			{
				ChangeEpochToTribez(Animals);
			}
			break;
			case 1:
			//CreateTribez();
			break;
		}
	}

    public void CreateAnimal(Vector2 cords)
    {
		GameObject clone = Instantiate<GameObject>(AnimalPrefab, cords, Quaternion.identity);
		clone.transform.parent = transform;
    }

	public void ChangeEpochToTribez(List<Animal> creatures)
	{
		foreach(Animal creature in creatures)
		{
			Destroy(creature.gameObject);
		}
		creatures.Clear();
		Epoch ++;
		CreateTribez();
	}

	public void CreateTribez()
	{
		List<Zone> zns = pg.Zones;
		for(int i = 0; i < zns.Count; i++)
		{
			if(zns[i].id == 3)
			{
				GameObject clone = Instantiate<GameObject>(TribePrefab, zns[i].cords, Quaternion.identity);
				clone.transform.parent = transform;
				//Vector3 direction = (clone.transform.position - Vector3.zero).normalized;
				//Quaternion rot = Quaternion.LookRotation(new Vector3(0, 0, direction.z));
				clone.transform.rotation = zns[i].obj.rotation;
				clone.transform.localPosition = zns[i].cords;
			}
		}
		Debug.Log("TRIBEZZZZ");
	}

	public bool CanSetTribez()
	{
		Color mainColor = Animals[0].myAG.color;
		for(int i = 1; i < Animals.Count; i++)
		{
			if(Animals[i].myAG.color != mainColor)
				return false;
		}
		return true;
	}
}
