using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Zone
{
    public int id;
    public float size;
    public Vector2 cords;
    public Transform obj;

    public Zone(int id, float size, Vector2 cords)
    {
        this.id = id;
        this.size = size;
        this.cords = cords;
        this.obj = null;
    }
}

/*
 * ids:
 * -2 - Cliff
 * -1 - Water
 * 0 - Flat
 * 1 - Hill
 * 2-3 - Flat and plants
 * 4- Vulcano
*/

public class PlanetGenerator : MonoBehaviour
{
    [Header ("Layers")]
    public Transform Center;
    public Transform[] Layers;

    [Header("Controllers")]
    public LifeController lifeController;

    public Transform HillsParent;
    public Transform CliffsParent;
    public Transform WaterParent;
    public Transform PlantsParent;
    public Transform LifeParent;

    public float Radius;
    public GameObject[] HillPrefabs;
    public GameObject WaterLine;
    public GameObject WaterPrefab;
    public GameObject CliffPrefab;
    public GameObject[] PlantsPrefabs;
    public GameObject[] VulcanoPrefabs;

    public List<Zone> Zones = new List<Zone>();

    public float AngleStep = 10f;

    public float minHillsHeight = 0.5f;
    public float minCliffsHeight = 0.3f;
    public float NormalizingKoef = 1;

	public void Start()
	{
        NormalizingKoef = 1;//2 * AngleStep / Radius;
        GeneratePlanet();
        StartCoroutine(Test());
	}

    public IEnumerator Test()
    {
        yield return new WaitForSeconds(8f);
        CreateLife();
    }

	public void GeneratePlanet()
    {
        ScalePlanet();

        for (float i = 0; i < 360; i+= AngleStep)
        {
            float x = Mathf.Sin(i * Mathf.Deg2Rad) * Radius;
            float y = Mathf.Sqrt(Radius * Radius - x * x);
            //Debug.Log("Angle = " + i + " | X = " + x + " | Y = " + y);
            if(i > 90 && i < 270)
                SetPoint(x, -y, i);
            else
                SetPoint(x, y, i);
        }
        HillsParent.transform.localScale *= 0.999f;
        CombineZones();


    }

    public void ScalePlanet()
    {
        foreach (Transform layer in Layers)
        {
            layer.localScale = Vector3.one * (Radius * 2);
        }
    }

    public void SetPoint(float x, float y, float angle)
    {

        float height = Mathf.PerlinNoise(x + Random.Range(-0.1f, 1.1f), y + Random.Range(-0.1f, 0.1f));
        height = (height - 0.5f) * 2;
        GameObject prToInst = null;
        Transform parent = null;

        int CurrentZone = GetZone(height);

        Zone zn =  new Zone(CurrentZone, height, new Vector2(x,y));
        bool original = false;
        switch(CurrentZone)
        {
            case 0:
                prToInst = null;
                parent = null;
                original = false;
                break;
            case 1:
                prToInst = GetRandomObjFrom(HillPrefabs);
                parent = HillsParent;
                height *= 1.5f;
                original = false;
                break;
            case 3:
            case 2:
                prToInst = GetRandomObjFrom(PlantsPrefabs);
                parent = PlantsParent;
                height = 1;
                original = true;
                break;
            case -1:
                prToInst = WaterPrefab;
                parent = WaterParent;
                original = false;
                break;
            case -2:
                prToInst = CliffPrefab;
                parent = CliffsParent;
                original = false;
                if(Random.Range(0,100) < 40f)
                {
                    SetPart(x, y, GetRandomObjFrom(PlantsPrefabs), PlantsParent, 1, angle, null, true);
                }
                break;
        }

        if(prToInst != null && parent != null)
        {
            SetPart(x, y, prToInst, parent, height, angle, zn, original);
        }

        Zones.Add(zn);
    }

    public void SetPart(float x, float y, GameObject prToInst, Transform parent, float height, float angle, Zone zn, bool original)
    {
        GameObject clone = Instantiate<GameObject>(prToInst, new Vector2(x, y), Quaternion.identity);
        float width = 1;
        float nK = 1;
        if (!original)
        {
            width = 2 * Radius * Mathf.Sin(AngleStep * Mathf.Deg2Rad / 2);
            nK = NormalizingKoef;
        }
        clone.transform.localScale = new Vector3(width, height * nK, 1);
        clone.transform.rotation = Quaternion.Euler(Vector3.forward * -angle);
        clone.transform.parent = parent;
        if(zn != null)
		{
            zn.obj = clone.transform;
		}
    }

    public int GetZone(float height)
    {
        

        int zone = 0;

        if (height < -0.3f)
            zone = -1;
        else if (height >= -0.15 && height < -0.05f)
            zone = -2;
        else if (height >= -0.05 && height < 0f)
            zone = 0;
        else if (height >= 0 && height < 0.05f)
            zone = 2;
        else if (height >= 0.06 && height < 0.1f)
            zone = 3;
        else if (height >= 0.1f)
            zone = 1;
        //Debug.Log(height);
        return zone;
    }


    public void CombineZones()
    {
        for (int i = 1; i < Zones.Count - 1; i++)
        {
            if((Zones[i - 1].id == -1 && Zones[i].id == -1))
            {
                GameObject clone = Instantiate<GameObject>(WaterLine, Zones[i].cords, Quaternion.identity);
                clone.transform.LookAt(Zones[i - 1].cords);
                int d = 1;
                if (Zones[i].cords.y < 0)
                    d = -1;
                clone.transform.localScale = new Vector3(1, Zones[i].size * d * NormalizingKoef, Vector3.Distance(Zones[i].cords, Zones[i - 1].cords));
                clone.transform.parent = WaterParent;
            }

            if(Zones[i].id == 1 && Zones[i - 1].id == 1 && Zones[i + 1].id == 1)
            {
                float height = Mathf.Max(Zones[i].size, Zones[i-1].size, Zones[i+1].size);
                Vector3 hillScale = Zones[i].obj.localScale;
                float width = Vector3.Distance(Zones[i].cords, Zones[i - 1].cords);
                Zones[i].obj.localScale = new Vector3(width*2, height * 2 * NormalizingKoef, 1);
            }

            if (Zones[i].id == 1 && Zones[i - 1].id != 1 && Zones[i + 1].id != 1 && Random.Range(0,10) > 9f)
            {
                GameObject clone = Instantiate<GameObject>(GetRandomObjFrom(VulcanoPrefabs), Zones[i].cords, Quaternion.identity);
                clone.transform.rotation = Zones[i].obj.rotation;
                Zones[i].cords = clone.transform.position;
                Zones[i].id = 4;
                Zones[i].size = 1;
                Destroy(Zones[i].obj.gameObject);
                Zones[i].obj = clone.transform;
                clone.transform.parent = HillsParent;

            }
        }
    }



    public GameObject GetRandomObjFrom(GameObject[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public void CreateLife()
    {
        for (int i = 0; i < Zones.Count; i++)
        {
            switch(Zones[i].id)
            {
                case 2: case 3:
                    lifeController.CreateAnimal(Zones[i].cords);
                    break;
            }
        }
    }
}