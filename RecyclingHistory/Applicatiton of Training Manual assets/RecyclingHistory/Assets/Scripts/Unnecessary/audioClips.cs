using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioClips : MonoBehaviour
{
    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    public AudioClip four;
    public AudioClip five;
    public AudioClip six;
    public AudioSource audio;
    public bool view;
    public GameObject Target;
    public GameObject Sphere;
    public float speed = 200.0f;
    public float rotationspeed = 200.0f;
    // Start is called before the first frame update
    void Start()
    {
       // audio = GetComponent<AudioSource>();
    }

    
    // Update is called once per frame
    void Update()
    {
        float leftright = Input.GetAxis("Horizontal");
        Sphere.transform.rotation = Sphere.transform.rotation * Quaternion.AngleAxis(leftright * rotationspeed * Time.deltaTime, axis: Sphere.transform.up);

        if (Target.GetComponent<Renderer>().isVisible)
        {
            Debug.Log("Visible");
            view = true;
        }
        else
        {
            Debug.Log("Not Visible");
            view = false;
        }
            
        if (Input.GetKeyDown(KeyCode.H) && view == true)
        {
            Debug.Log("pressed");
            int i;
            i = Random.Range(1,7);
            if (i == 1) {
                audio.PlayOneShot(one);
                Debug.Log("1");
            }
            if (i == 2)
            {
                audio.PlayOneShot(two);
                Debug.Log("2");
            }
            if (i == 3)
            {
                audio.PlayOneShot(three);
                Debug.Log("3");
            }
            if (i == 4)
            {
                audio.PlayOneShot(four);
                Debug.Log("4");
            }
            if (i == 5)
            {
                audio.PlayOneShot(five);
                Debug.Log("5");
            }
            if (i == 6)
            {
                audio.PlayOneShot(six);
                Debug.Log("6");
            }
        }
    }
   
}
