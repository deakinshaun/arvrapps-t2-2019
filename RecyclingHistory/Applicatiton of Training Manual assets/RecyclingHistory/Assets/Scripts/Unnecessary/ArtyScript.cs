using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtyScript : MonoBehaviour
{
    public float WEF = 400f;
    public GameObject target;
    public GameObject[] OPFOR;
    public float health =100f;


    public Color myColor;
    MeshRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
     
     //   myRenderer = target.GetComponent<MeshRenderer>();
       // target = GetClosestEnemy(OPFOR);

        Debug.Log("Finding close tgt"+target.name);
    }

    // Update is called once per frame
    void Update()
    {
        //Is target within range?
        if ((Vector3.Distance(target.transform.position, transform.position))<=this.WEF)
            {
            
               myRenderer= target.GetComponent<MeshRenderer>(); ;
            myRenderer.material.color = Color.red;// myColor;
            Debug.Log(target.name + " in range!");

        }
      /*  target = GetClosestEnemy(OPFOR);
        if (target = null)
        {
            Debug.Log("Finding close tgt");
       
        }*/
    }
    // public GameObject NearestTarget(GameObject attackingUnit, GameObject[] OPFOR)
    //{


 
    //}

}
