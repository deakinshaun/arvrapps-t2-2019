using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitData : MonoBehaviour
{
     public  float unitHealth=100f;
     public float WEF;
     public float moveSpeed;
     public float reloadSpeed;
    public float timeLastShot=0f;
     public float shootingAccuracy;
     public float instantDestroyRate;
    public float minDamageRange;
    public float maxDamageRange;
    public Vector3 movementDestination;
     public GameObject[] OPFOR;
     public GameObject target;
     public bool alive = true;
    public AudioClip battleSFX;
    public AudioSource myShotSFX;
    public AudioClip myDeathSound;
    public GameObject crucifix;
    public float time = 0.0f;
    public float destroytime;
    public float distanceToClose;
    public GameObject blast;
    public GameObject myText;
    // Start is called before the first frame update
    void Start()
    {
        target = GetClosestEnemy(OPFOR);
        myShotSFX.clip = battleSFX;

      //  Debug.Log(this.name+" is Finding close tgt" + target.name);
    }

    bool EnemyStillAlive(GameObject[] OPFOR)
    {
        GameObject obj;
        bool tmp = false;
        foreach (GameObject potentialTarget in OPFOR)
        {
            obj = potentialTarget;
            if(obj.GetComponent<UnitData>().alive)
            {
                tmp = true;
                break;
            }
        }
            return tmp;
    }
    // Update is called once per frame
    void Update()
    {
        if (EnemyStillAlive(OPFOR))
        { //Still enemy to to be destroyed
            target = GetClosestEnemy(OPFOR);
            if (this.alive)
            {//Current unit is still in the fight
                if ((Vector3.Distance(target.transform.position, transform.position)) <= this.WEF)
                {// Target currently in range, no need to move
                    //Has the unit finihsed reloading so it can shoot again?

                    if (Time.time >= (timeLastShot + reloadSpeed))
                    {
                        attackUnit(target);
                        timeLastShot = Time.time;
                        //play audio of shot
                        myShotSFX.Play();

                        //    Debug.Log("Shot");
                    }
                    //  Debug.Log(target.name + " in range!");

                }
                else
                {//Target out of range, must move closer
                MoveToAttack(target);
                }
            }
            else {

                float positionx = gameObject.transform.position.x;
                float positionz = gameObject.transform.position.z;
            //Time to bring the crucifix
                //time += Time.deltaTime;
               // destroytime += Time.deltaTime;
             //   if (time < 0.02f)
              //  {
                    GameObject dead = Instantiate(crucifix, new Vector3(positionx, -201, positionz), Quaternion.identity);

                    gameObject.active = false;

               // }
               
               
            }
        }
        else
        {
            //The battle has been won.
            Debug.Log("You won");
        }

    }

     void attackUnit(GameObject target)
    {
      // Debug.Log("Name of target is" + target.name);
        float shotAccuracy = Random.Range(0.0f, 100.0f);
        //Debug.Log("shot accuraccy of " + this.name + "is " + shotAccuracy);
        if (shotAccuracy <= shootingAccuracy)
        {
            // The aggressor has hit the target, is it an instant kill or a wounding shot?
            shotAccuracy = Random.Range(0.0f, 100.0f);
            
            if (shotAccuracy <= instantDestroyRate)
            {
                // This is an instant kill
                //Debug.Log("Ïnstant kill");
                Debug.Log("Unit instantly killed by " + this.name);
                target.GetComponent<UnitData>().unitHealth = 0f;
                target.GetComponent<UnitData>().alive = false;
                float x = transform.position.x;
                float y = transform.position.y;
                float z = transform.position.z;
                GameObject shoot = Instantiate(blast, new Vector3(x , y + 15, z) , Quaternion.identity);
                shoot.transform.position =  transform.forward * 5.0f;
                //Change unit icon to indicate destroyed
                //play audio of detroyed unit
                myShotSFX.clip = target.GetComponent<UnitData>().myDeathSound;
                myShotSFX.Play();
                myShotSFX.clip = battleSFX;
            }
            else
            {
                //wounding shot only
                float damage = Random.Range(minDamageRange, maxDamageRange);
                target.GetComponent<UnitData>().unitHealth -= damage;
                Debug.Log(target.name+" Hit with"+damage+ "points of wounding shot by "+this.name);
                float x = transform.position.x;
                float y = transform.position.y;
                float z = transform.position.z;
                GameObject shoot = Instantiate(blast, new Vector3(x,y+5,z), Quaternion.identity);
                //check to see if unit has been destroyed
                if (target.GetComponent<UnitData>().unitHealth <= 0f)
                {
                    // This is an instant kill
                    Debug.Log("Unit killed with wounding shot by "+this.name);
                    target.GetComponent<UnitData>().unitHealth = 0f;
                    target.GetComponent<UnitData>().alive = false;
                    //Change unit icon to indicate destroyed
                    //play audio of detroyed unit
                    myShotSFX.clip = target.GetComponent<UnitData>().myDeathSound;
                    myShotSFX.Play();
                    myShotSFX.clip = battleSFX;
                }
                
            }
        }

    }

 void MoveToAttack(GameObject target)
    {

        //how far away from target
        distanceToClose = Vector3.Distance(this.transform.position, target.transform.position) +1f;
        this.movementDestination = target.transform.position - new Vector3(WEF, 0, WEF);
        

      
    }

  

    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        //Originally sourced from https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        GameObject obj;
       Debug.Log("Cur unit=" + this.transform.name);
        //for (int i = 0; i < enemies.Length; i++)
       
        foreach (GameObject potentialTarget in enemies)
        {
           // bestTarget = potentialTarget;
            obj = potentialTarget;
            
           if (obj.GetComponent<UnitData>().alive) //Make sure tehy are alive
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                    //Select where the unit needs to move to so it can shoot allocated tgt
                    //movementDestination= new Vector3()
                    Debug.Log(this.name+" tgt updated to " + bestTarget.name);

                }
            }
        }
        return bestTarget;
    }

    float distanceToObjects(GameObject obj1, GameObject obj2)
    {
        return Vector3.Distance(obj1.transform.position, obj2.transform.position);
    }
}
