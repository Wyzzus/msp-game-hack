using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    public Transform Center;
    public Transform[] Layers;

    public Transform HillsParent;
    public Transform CliffsParent;

    public float Radius;
    public GameObject HillPrefab;
    public GameObject CliffPrefab;

    public float AngleStep = 10f;

    public float minHillsHeight = 0.5f;
    public float minCliffsHeight = 0.3f;

	public void Start()
	{
        GeneratePlanet();
	}

	public void GeneratePlanet()
    {
        ScalePlanet();

        for (float i = 0; i < 360; i+= AngleStep)
        {
            float x = Mathf.Sin(i * Mathf.Deg2Rad) * Radius;
            float y = Mathf.Sqrt(Radius * Radius - x * x);
            Debug.Log("Angle = " + i + " | X = " + x + " | Y = " + y);
            if(i > 90 && i < 270)
                SetPoint(x, -y, i);
            else
                SetPoint(x, y, i);
        }
        HillsParent.transform.localScale *= 0.99f;


    }

    public void ScalePlanet()
    {
        foreach (Transform layer in Layers)
        {
            layer.localScale = Vector3.one * (Radius * 2 + 0);
        }
    }

    public void SetPoint(float x, float y, float angle)
    {

        float height = Mathf.PerlinNoise(x + Random.Range(-0.1f, 1.1f), y + Random.Range(-0.1f, 1.1f));

        GameObject clone;
        if (height > minHillsHeight)
        {
            clone = Instantiate<GameObject>(HillPrefab, new Vector2(x, y), Quaternion.identity);
            clone.transform.parent = HillsParent;
        } else
        {
            clone = Instantiate<GameObject>(CliffPrefab, new Vector2(x, y), Quaternion.identity);
            clone.transform.parent = CliffsParent;
        }

        //clone.transform.LookAt(Center);
        if (height >= minCliffsHeight && height <= minHillsHeight)
            height = 0;
        clone.transform.localScale = new Vector3(2 * Radius * Mathf.Sin(AngleStep * Mathf.Deg2Rad /2), height, 0.2f);
        clone.transform.rotation = Quaternion.Euler(Vector3.forward * -angle);

    }

}


    /*public GameObject prefab;

    public List<Vector2> Up = new List<Vector2>();
    public List<Vector2> Down = new List<Vector2>();

    public void Build(float centerX, float centerY, float radius) 
    {
        float R = 5;
        float y = 0;
        float delta = 0.1f;
        for (float x = -R; x <= R; x += delta)
        {
            y = Mathf.Sqrt(R * R - x * x);
            float scaleFactor = (1 / delta);
            float xR = Mathf.Round(x * scaleFactor) / scaleFactor;
            float yR = Mathf.Round(y * scaleFactor) / scaleFactor;
            SetPoint(xR, yR, 1);
            SetPoint(xR, yR, -1);
        }
    }

    public void SetPoint(float x, float y, int pos)
    {
        //GameObject clone = Instantiate<GameObject>(prefab, new Vector2(x, y), Quaternion.identity);
        if(pos > 0)
        {
            Up.Add(new Vector2(x, y));
        }
        else
        {
            Up.Add(new Vector2(x, -y));
        }
    }

	public void Start()
	{
        Build(0,0, 10);
	}

	public void Generate()
    {
        for (int i = 1; i < Up.Count; i++)
        {
            
        }
    }
}*/
