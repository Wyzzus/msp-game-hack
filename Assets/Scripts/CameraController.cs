using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Camera MyCamera;
	public float ZoomSpeed = 1;
	public float OffsetSpeed = 1;

	public float MaxZoom = 15f;
	public float MinZoom = 1f;

	public float MaxOffset;
	public float MinOffset = 0;


	float zoom = 15f;
	float offset = 0f;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PlanetController.instance.PlanetSettings)
		{
			MaxOffset = PlanetController.instance.PlanetSettings.Radius + 0.5f;
		}

		zoom += Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
		zoom = Mathf.Clamp(zoom, MinZoom, MaxZoom);
		MyCamera.orthographicSize = zoom;

		offset -= Input.GetAxis("Mouse ScrollWheel") * OffsetSpeed;
		offset = Mathf.Clamp(offset, MinOffset, MaxOffset);
		transform.position = new Vector3(0, offset, -10f);
	}
}
