using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject sponza;
    private Material[] sponzaMaterials;

    // Start is called before the first frame update
    void Start()
    {
        sponzaMaterials = sponza.GetComponent<Renderer>().sharedMaterials;
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(MainCamera.transform.position);

        if (camPositionInPortalSpace.y < 0.5f)
        {
            //disable stencial test
            for(int i = 0; i < sponzaMaterials.Length; i++)
            {
                sponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
            }
        }
        else
        {
            for (int i = 0; i < sponzaMaterials.Length; i++)
            {
                sponzaMaterials[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
            }
        }
    }
}
