using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public List<GameObject> itemsAdded = new List<GameObject>(); //contain a list of food items that were added to cart

    /*
        Add new food choice gameobject to the itemsAdded list 
    */
    public void addToCart(GameObject item)
    {
        itemsAdded.Add(item);
    }

    /*
        Clear the session
    */
    public void clearCart()
    {
        itemsAdded.Clear();
    }
}
