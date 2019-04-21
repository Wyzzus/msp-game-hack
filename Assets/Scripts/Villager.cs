using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour {

	public HumanGenerator myHG;
	public TribeScript Home;

	public float LifeTime = 100;

	public float Health = 100;

	public float Power = 10;
	public float Speed = 0.5f;


	public int direction = 1;
	public Villager currentEnemy;
	public TribeScript currentTribe;
	public float delay = 0;
	public bool OnDeath = false;
	// Use this for initialization
	void Start () 
	{
		Setup();
	}

	public void Setup()
	{
		Speed = Random.Range(0.4f, 0.45f);

		if (Random.Range(0, (int)2) != 0)
			ChangeDirection();

		LifeTime = Random.Range(40, 80);

		Health = Random.Range(90, 110);
		Power = Random.Range(4, 8) / (100 / Health);
	}

	public void ChangeDirection()
	{
		direction *= -1;
	}

	// Update is called once per frame
	void Update()
	{

		transform.localScale = new Vector3(direction, 1, 1);

		if (currentEnemy)
		{
			delay += Time.deltaTime;
			if (delay > 1)
				Fight();
		}
		else if(currentTribe)
		{
			delay += Time.deltaTime;
			if(delay > 1)
				CaptureTribe();
		}
		else
		{
			Movement();
			LifeTime -= Time.deltaTime;
			currentEnemy = null;
			currentTribe = null;
		}

		if (LifeTime < 0)
		{
			
			Destroy(gameObject);
		}

	}

	public void Movement()
	{
		transform.position += transform.right * direction * Speed * Time.deltaTime;
	}

	public void Fight()
	{
		delay = 0;
		if(currentEnemy.Health > 0)
		{
			currentEnemy.Health -= Power;
		}
		else
		{
			Destroy(currentEnemy.gameObject);
			ChangeDirection();
			UpStats();
		}
	}

	public void CaptureTribe()
	{
		delay = 0;
		if(currentTribe.Health > 0)
		{
			currentTribe.Health -= Power;
		}
		else
		{
			currentTribe.Setup(myHG.color);
		}
	}

	public void UpStats()
	{
		LifeTime += 10;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		Villager entity = other.GetComponent<Villager>();
		//Debug.Log(entity.name);
		if(entity)
		{
			if(entity.myHG.color != myHG.color && !currentEnemy)
			{
				currentEnemy = entity;
			}
			if (entity.myHG.color == myHG.color)
			{
				ChangeDirection();
			}
		}

		TribeScript _entity = other.GetComponent<TribeScript>();

		if(_entity)
		{
			if(_entity.color != myHG.color && !currentTribe)
			{
				currentTribe = _entity;
			}
			if(_entity.color == myHG.color)
			{
				ChangeDirection();
			}
		}

	}
}
