using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class fov : MonoBehaviour
{
    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    public AudioClip four;
    public AudioClip five;
    public AudioClip six;
    public AudioClip environment;
    public AudioClip interval;
    public AudioSource audio;
    public AudioSource audio2;
    public AudioSource audio3;
    public GameObject Target;
    public bool view;
    public VideoPlayer videoPlayer;
    public GameObject video;
    public float timer;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
       //Autonomus starts audio playing

        audio.Play();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (audio.isPlaying) {
            Debug.Log("war");

        }
        if (audio2.isPlaying)
        {
            Debug.Log("interval");

        }
        if (audio3.isPlaying)
        {
            Debug.Log("shots");

        }

        timer += Time.deltaTime;
        if (timer > 30.0f)
        {
            audio2.Play();
            Debug.Log("interval");// Sound to be played at a regular interval durinf the scene placed a bit far from user.
        }
        else {
            timer = 0;
        }
        // check if the user is able to see the objects after looking at the image target which will be used to play the SFX
        if (Target.GetComponent<Renderer>().isVisible)
        {
            
            Debug.Log("Visible");
            view = true;
            audio.Pause();
           

        }
        else
        {
            Debug.Log("Not Visible");
            view = false;
         
            // video.GetComponent<VideoPlayer>().Pause();
        }


        //If the user is able to see the game objets of image target sfx should be played 
        if (view == true)
        {
            // video.GetComponent<VideoPlayer>().Play();
            audio.Pause();
            if (audio3.isPlaying == false)
            {
                i = Random.Range(1, 7);// generate a random number to select the right audio SFX to be played

                Debug.Log("pressed");


                if (i == 1)
                {
                    audio3.PlayOneShot(one);
                    Debug.Log("1");
                    // view = false;
                }
                if (i == 2)
                {
                    audio3.PlayOneShot(two);
                    Debug.Log("2");
                }
                if (i == 3)
                {
                    audio3.PlayOneShot(three);
                    Debug.Log("3");
                }
                if (i == 4)
                {
                    audio3.PlayOneShot(four);
                    Debug.Log("4");
                }
                if (i == 5)
                {
                    audio3.PlayOneShot(five);
                    Debug.Log("5");
                }
                if (i == 6)
                {
                    audio3.PlayOneShot(six);
                    Debug.Log("6");
                }
            }
        }
        else
        {
            audio3.Pause();
            audio.UnPause();
        }
       

    }

  
}
