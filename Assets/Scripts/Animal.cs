using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

    public AnimalGenerator myAG;
    public GameObject AnimalPrefab;

    public float LifeTime = 100;

    public float Health = 100;

    public float Power = 10;
    public float Speed = 0.5f;


    public int direction = 1;
    public Animal currentEnemy;
    public float delay = 0;
    public bool OnDeath = false;
	// Use this for initialization
	void Start () 
    {
        Setup();
	}

    public void Setup()
    {
        Speed = Random.Range(0.2f, 0.5f);

        if (Random.Range(0, (int)2) != 0)
            ChangeDirection();

        LifeTime = Random.Range(80, 120);

        Health = Random.Range(90, 110);
        Power = Random.Range(8, 10) / (100 / Health);
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
        else
        {
            Movement();
            LifeTime -= Time.deltaTime;
        }


        if (!OnDeath && LifeTime < 20)
            CreateChild(false);

        if (LifeTime < 0)
            Destroy(gameObject);

    }

    public void CreateChild(bool OnVictory)
    {
        GameObject clone = Instantiate<GameObject>(AnimalPrefab, transform.position + transform.right * -direction, Quaternion.identity);
        clone.GetComponent<AnimalGenerator>().Inherit(myAG);
        if (OnVictory)
            clone.GetComponent<Animal>().UpStats();
        else
            OnDeath = true;
        clone.transform.parent = PlanetController.instance.PlanetSettings.LifeParent;

        if(Random.Range(0, 100.0f) < 2f)
        {
            CreateChild(false);
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
            CreateChild(true);
            UpStats();
        }
    }

    public void UpStats()
    {
        LifeTime += 10;
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
        Animal entity = other.GetComponent<Animal>();
        //Debug.Log(entity.name);
        if(entity)
        {
            if(entity.myAG.color != myAG.color && !currentEnemy)
            {
                currentEnemy = entity;
            }
            if (entity.myAG.color == myAG.color)
            {
                ChangeDirection();
            }
        }
	}

	//public void 
}
