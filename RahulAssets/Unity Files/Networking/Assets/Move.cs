using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Move : NetworkBehaviour
{
    public float speed = 2f;
    public float turnspeed = 100.0f;
    [SyncVar]
    Vector3 Serverposition;
 

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {

            moveobject();
        }
      
    }
 
    void moveobject()
    {
        float v = Input.GetAxis("Vertical");
        transform.position += Vector3.right * Time.deltaTime * speed * v;
        float h = Input.GetAxis("Horizontal");
        transform.rotation *= Quaternion.AngleAxis(h * turnspeed * Time.deltaTime, transform.up);
        CmdUpdatePosition(transform.position);
    }
    [Command]
    void CmdUpdatePosition( Vector3 newPosition) {
        Serverposition = newPosition;
    }

   
}
