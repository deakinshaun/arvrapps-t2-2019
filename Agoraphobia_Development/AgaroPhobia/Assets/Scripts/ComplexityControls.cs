using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ComplexityControls : MonoBehaviour
{

    public TextMeshPro modelCount;
    public GameObject human_1;
    public GameObject human_2;
    public GameObject human_3;
    public GameObject human_4;
    public GameObject human_5;
    public GameObject human_6;

    public int activeModels;



    // Start is called before the first frame update
    void Start()
    {
        activeModels = 1;

    }

    // Update is called once per frame
    void Update()

    {

        //if blah blah swpie right blah
        //add one to activeModels

        //if blah blah swpie left blah
        //remove one activeModels

        hideModel(human_1, 1);
        hideModel(human_2, 2);
        hideModel(human_3, 3);
        hideModel(human_4, 4);
        hideModel(human_5, 5);
        hideModel(human_6, 6);


        modelCount.text = "Models " + activeModels.ToString();
    }

    void hideModel(GameObject human, int levelOfComplexity)
    {
        if (activeModels >= levelOfComplexity)
        {
            human.SetActive(true);
        }
        else
            human.SetActive(false);
    }





}
