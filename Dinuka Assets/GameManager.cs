using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject AnnotationPanel;
    public Text IngredientsField;
    public Text AllergensField;

    private bool isAnnotationsShown = false;

    void Update()
    {
        
    }

    /*
        Show and hide annotations UI panel
    */
    public void displayAnnotation() {
        if(isAnnotationsShown) {
            AnnotationPanel.SetActive(false);
            isAnnotationsShown = false;
        }
        else {
            AnnotationPanel.SetActive(true);
            isAnnotationsShown = true;
        }
    }
}
