using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Run : MonoBehaviour
{
    public Animator animation;
    private float runSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
     animation = GetComponent<Animator>();
        Debug.Log("started");
       

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("updateed");



        // if (Input.GetKey("b"))
        //  {
        //Debug.Log("Hello: ");
        animation.Play("run");
        transform.position += Vector3.forward * Time.deltaTime * runSpeed;

    //}
        //else
       // {
        //    animation.Play("idle");
       // }
        //else()
        
    }
   
    public void moveleftright()
    {
        Debug.Log("leftorright");
        transform.position -= Vector3.right * Time.deltaTime * 1.50f;
    }
}
