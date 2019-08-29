using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this to the camera
public class PanoramaCamera : MonoBehaviour
{
    public float speed = 20f;

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        transform.rotation *= Quaternion.AngleAxis(h * speed, transform.up)
        * Quaternion.AngleAxis(v * speed, transform.right);
    }
}
