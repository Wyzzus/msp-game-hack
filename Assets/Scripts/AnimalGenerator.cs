using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyStruct
{
    public Transform HeadSpot;
    public Transform TailSpot;
    public GameObject BodySpot;
}

public class AnimalGenerator : MonoBehaviour {

    public Color color;

    public BodyStruct[] Bodies;

    public BodyStruct CurrentBody;

    public GameObject[] Heads;
    public GameObject[] Tails;

    public int Body;
    public int Head;
    public int Tail;

    public int Generation = 0;


	// Use this for initialization
	void Start () 
    {
        if(Generation == 0)
        {
            this.color = Random.ColorHSV();
            this.Body = Random.Range(0, Bodies.Length);
            this.Head = Random.Range(0, Heads.Length);
            this.Tail = Random.Range(0, Tails.Length);
        }

        Create();
	}

    public void Create()
    {
        CurrentBody = Bodies[Body];
        CurrentBody.BodySpot.SetActive(true);
        Instantiate<GameObject>(Heads[Head], CurrentBody.HeadSpot);
        Instantiate<GameObject>(Tails[Tail], CurrentBody.TailSpot);

        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
			if(sr.name != "battlestart")
			{
				sr.color = color;
			}
        }
    }

    public void Inherit(AnimalGenerator parent)
    {
        this.color = parent.color;
        this.Body = parent.Body;
        this.Head = parent.Head;
        this.Tail = parent.Tail;
        Generation = parent.Generation + 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
