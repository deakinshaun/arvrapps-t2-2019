using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthColour : MonoBehaviour
{
    public float healthline;
    public UnitData data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        healthline = data.unitHealth;

        if (healthline >= 80 && healthline <100)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("green");
        }
        else if (healthline >= 60 && healthline < 80)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            Debug.Log("yellowgreen");
        }
        else if (healthline >= 40 && healthline < 60)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log("yellow");
        }
        else if (healthline >= 20 && healthline < 40)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 140, 0);
            Debug.Log("orange");
        }
        else if (healthline > 0 && healthline < 20)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("red");
        }
    }
}
