using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballShoot : MonoBehaviour
{
    float timer = 0.0f;
    public Rigidbody projectile;
    public float speed = -10.0f;
    public lookatgoolie restart;
    public bool starttimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {

        
       
        
        Debug.Log(timer);
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody p = Instantiate(projectile, transform.position, transform.rotation);
            p.velocity = transform.right  * speed;
            starttimer = true;
        }
        if (starttimer == true) {
            timer += Time.deltaTime;
        }
        if (timer > 2.0f) {
            starttimer = false;
            restart.ballgone();
            timer = 0.0f;
        }
    }
}
