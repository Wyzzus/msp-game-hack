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


	// Use this for initialization
	void Start () 
    {
        CurrentBody = Bodies[Random.Range(0, Bodies.Length)];
        CurrentBody.BodySpot.SetActive(true);
        Instantiate<GameObject>(Heads[Random.Range(0, Heads.Length)], CurrentBody.HeadSpot);
        Instantiate<GameObject>(Tails[Random.Range(0, Tails.Length)], CurrentBody.TailSpot);

        color = Random.ColorHSV();

        foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = color;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
