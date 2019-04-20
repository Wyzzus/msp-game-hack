using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour {

    public float gravity = -10f;

    public void Attract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;

        body.GetComponent<Rigidbody2D>().AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(body.up, gravityUp) * body.rotation;

        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
