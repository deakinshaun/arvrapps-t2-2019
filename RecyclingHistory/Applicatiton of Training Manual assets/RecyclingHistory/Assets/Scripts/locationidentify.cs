using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class locationidentify : MonoBehaviour
{
    
    public GameObject assign;// The game object in which the selected object gets stored
    
    public UnitData RedCavdata;// To get Target from UnitData for this particular soldier
    public UnitData BlueCavdata;// To get Target from UnitData for this particular soldier
    public UnitData RedArtydata;// To get Target from UnitData for this particular soldier
    public UnitData BlueArtydata;// To get Target from UnitData for this particular soldier
    public UnitData RedInfdata;// To get Target from UnitData for this particular soldier
    public UnitData BlueInfdata;// To get Target from UnitData for this particular soldier
    public GameObject target; // Target object to store the current target of the current soldier
    public string location;// locaiton of soldier object 
   public GameObject hand;
    public Text myText;
    public int i = 1;
    public GameObject Texting;
   public float timer;
    public bool moveforward;
    // Start is called before the first frame update
    void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {
        
        hand = assign;
        timer += Time.deltaTime;//start timer to move object for specified seconds
        myText.text = assign.name.ToString();
              location = (hand.transform.position.x +"\t"+ hand.transform.position.y + "\t"+  hand.transform.position.z).ToString();
        //check if the soldier is allowed to move 
        if (timer < 5.0f && moveforward == true)
        {
           assign.transform.position = Vector3.MoveTowards(assign.transform.position, target.transform.position, 2f);
            
        }
        else
        {
            assign = null;
            moveforward = false;
        }
        

    }

    // In the below function it assingns the assign game objectto its own soliders name and sets the move forward to true to allow it to move towards its target 
    public void RedCavMarch() {

        target = RedCavdata.target;
        timer = 0;
        moveforward = true;
        Texting.GetComponent<TextMesh>().text = i.ToString();
        i += 1;
        assign = GameObject.Find("RedCav");

    }

    public void BlueCavMarch()
    {

        target = BlueCavdata.target;
        timer = 0;
        moveforward = true;
        Texting.GetComponent<TextMesh>().text = i.ToString();
        i += 1;
        assign = GameObject.Find("BlueCav");
    }

    public void RedArtyMarch()
    {

        target = RedArtydata.target;
        timer = 0;
        moveforward = true;
        Texting.GetComponent<TextMesh>().text = i.ToString();
        i += 1;
        assign = GameObject.Find("RedArty");

    }

    public void BlueArtyMarch()
    {

        target = BlueArtydata.target;
        timer = 0;
        moveforward = true;
        Texting.GetComponent<TextMesh>().text = i.ToString();
        i += 1;
        assign = GameObject.Find("BlueArty");
    }

    public void BlueInfMarch()
    {

        target = BlueInfdata.target;
        timer = 0;
        moveforward = true;
        Texting.GetComponent<TextMesh>().text = i.ToString();
        i += 1;
        assign = GameObject.Find("BlueInf");
    }

    public void RedInfMarch()
    {

        target = RedInfdata.target;
        timer = 0;
        moveforward = true;
        Texting.GetComponent<TextMesh>().text = i.ToString();
        i += 1;
        assign = GameObject.Find("RedInf");
    }

}
