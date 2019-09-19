using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public GameObject myPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGrab()
    {
        Instantiate(myPrefab, new Vector3(14.27f, -0.8389649f, -3.83f), Quaternion.identity);
        Debug.Log("buttonclicked");
    }
}
