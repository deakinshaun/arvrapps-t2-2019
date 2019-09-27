using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : MonoBehaviour
{
    public float zLoc, xLoc;
    public int maxOPFOR;
    public int maxBLUFOR;
    public GameObject[] BLUFOR;//Friendly forces or units you control
    public GameObject[] OPFOR;//Enemy forces that you are trying to destroy
    public bool alive = true;
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BLUFOR.Length; i++)
        {
            BLUFOR[i].transform.position = new Vector3(1, 1, 700);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
