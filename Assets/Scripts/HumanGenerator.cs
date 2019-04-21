using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGenerator : MonoBehaviour {
    public Color color;
    public SpriteRenderer[] PartsToColor;
    public GameObject[] Genders;
	// Use this for initialization
	void Start () 
    {
        foreach(SpriteRenderer sr in PartsToColor)
        {
            sr.color = color;
        }

        int gender = Random.Range(0, Genders.Length);
        for (int i = 0; i < Genders.Length; i++)
        {
            if (i != gender)
                Genders[i].SetActive(false);
            else
                Genders[i].SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
