using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using System;

public class SpeechRecognitionREST : MonoBehaviour
{
    private string subscriptionKey = "c42c258a12094d6ab0e30111cfd7f26c";
    private string token;
    //public Text debug;
    private GameObject cube;
    //public GameObject projectile;
    //public GameObject clone;
    // length of any recording sent. 10 s is the current limit.
    private int recordDuration = 2;


    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
        // all Certificates are accepted
        return true;
    }

    void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;
    }

    //private void Update()
    //{
    //    foreach (Touch touch in Input.touches)
    //    {
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            // Construct a ray from the current touch coordinates
    //            Ray ray = Camera.main.ScreenPointToRay(touch.position);
    //            if (Physics.Raycast(ray))
    //            {
    //                // Create a particle if hit
    //                Instantiate(cube, transform.position, transform.rotation);
    //            }
    //        }
    //    }

    //    if (Input.anyKeyDown)
    //    {

    //        Trigger();
    //    }
    //    //Trigger();
    //}

    void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                //Color changed to magenta due to testing purpose
                this.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                Trigger();
            }
        }
    }
    // Not really needed, but useful to test access to service.

    public void Authentication()
    {

        // Unity webforms do not handle the certificates required for https servers.

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://westus.api.cognitive.microsoft.com/sts/v1.0/issueToken");

        request.ContentType = "application/x-www-form-urlencoded";

        request.Method = "POST";
        request.ContentLength = 0;
        request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;


        var response = (HttpWebResponse)request.GetResponse();


        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        Debug.Log("Token received: " + responseString);
        //debug.text = "Token received: " + responseString;

        token = responseString;

    }

    public void SpeechToText(byte[] wavData)
    {

        // Send the request to the service.

        string fetchUri = "https://westus.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1"; //if errors - check the web code is right? //200 is corrct signal

        // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fetchUri + "?language=en-US&format=detailed");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fetchUri + "?language=en-US&format=simple");
        request.ContentType = "application/x-www-form-urlencoded";

        request.Method = "POST";

        request.ContentType = "audio/wav; codecs=audio/pcm; samplerate=16000";

        request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;


        Stream rs = request.GetRequestStream();
        rs.Write(wavData, 0, wavData.Length);
        rs.Close();

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        Debug.Log("Responsefrom service: " + responseString); //response string is full detailed response //json parser to put out the field needed
                                                              //check if 'up' is somewhere inn the response.



        //debug.text = "Response from service: " + responseString;

        //REFER THIS FOR EXTRACTING WORDS NEEDED
        if (responseString.Contains("Up"))
        {
            Debug.Log("Key Detected");
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, 0.2f, 0);
        }
        if (responseString.Contains("Down"))
        {
            Debug.Log("Key Detected");
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, 0.2f, 0);
        }
        if (responseString.Contains("Move Left"))
        {
            Debug.Log("Key Detected");
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, 0, 0.2f);
        }
        if (responseString.Contains("Move Right"))
        {
            Debug.Log("Key Detected");
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, 0, -0.2f);
        }
        if (responseString.Contains("Forward"))
        {
            Debug.Log("Key Detected");
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(-0.2f, 0, 0);
        }
        if (responseString.Contains("Back"))
        {
            Debug.Log("Key Detected");
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0.2f, 0, 0);
        }
        if (responseString.Contains("Change color"))
        {
            Debug.Log("Colour change command given");
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        
        //Canvas.GetDefaultCanvasTextMaterial.SpeechToText = 

    }

    private IEnumerator recordAudio()
    {
        // Set the microphone recording. Service requires 16 kHz sampling.

        AudioClip audio = Microphone.Start(null, false, recordDuration, 16000);

        yield return new WaitForSeconds(recordDuration);

        Microphone.End(null);


        // Play the recording back, to validate it was recorded correctly.

        AudioSource audioSource = GetComponent<AudioSource>();

        audioSource.clip = audio;
        //audioSource.Play();


        // Convert it to a wav file, and up load to the service.

        byte[] wavData = ConvertToWav(audio);

        SpeechToText(wavData);
    }

    public void Trigger()
    {

        //Authentication();

        StartCoroutine(recordAudio());

        //after authenitcation cheked and working, uncomment record audio 
        //comment out auth after everything works

    }

    // Remaining functions adapted from: https://gist.github.com/darktable/231706326
    const int HEADER_SIZE = 44;

    static byte[] ConvertToWav(AudioClip clip)
    {

        var samples = new float[clip.samples];

        clip.GetData(samples, 0);

        Int16[] intData = new Int16[samples.Length];

        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]


        Byte[] bytesData = new Byte[HEADER_SIZE + samples.Length * 2];

        //bytesData array is twice the size of
        //dataSource array because a floatconverted in Int16 is 2 bytes.


        int rescaleFactor = 32767;
        //to convert float to Int16

        WriteHeader(bytesData, clip);


        for (int i = 0; i < samples.Length; i++)
        {

            intData[i] = (short)(samples[i] * rescaleFactor);

            Byte[] byteArr = new Byte[2];

            byteArr = BitConverter.GetBytes(intData[i]);

            byteArr.CopyTo(bytesData, HEADER_SIZE + i * 2);

        }


        return bytesData;

    }


    static void WriteHeader(byte[] bytesData, AudioClip clip)
    {

        var hz = clip.frequency;

        var channels = clip.channels;

        var samples = clip.samples;


        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");

        riff.CopyTo(bytesData, 0);

        Byte[] chunkSize = BitConverter.GetBytes(HEADER_SIZE + clip.samples * 2 - 8);

        chunkSize.CopyTo(bytesData, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");

        wave.CopyTo(bytesData, 8);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");

        fmt.CopyTo(bytesData, 12);

        Byte[] subChunk1 = BitConverter.GetBytes(16);

        subChunk1.CopyTo(bytesData, 16);

        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);

        audioFormat.CopyTo(bytesData, 20);

        Byte[] numChannels = BitConverter.GetBytes(channels);

        numChannels.CopyTo(bytesData, 22);
        Byte[] sampleRate = BitConverter.GetBytes(hz);

        sampleRate.CopyTo(bytesData, 24);

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2);
        //sampleRate* bytesPerSample*number ofchannels, here 44100 * 2 * 2
        byteRate.CopyTo(bytesData, 28);


        UInt16 blockAlign = (ushort)(channels * 2);

        BitConverter.GetBytes(blockAlign).CopyTo(bytesData, 32);


        UInt16 bps = 16;

        Byte[] bitsPerSample = BitConverter.GetBytes(bps);

        bitsPerSample.CopyTo(bytesData, 34);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");

        datastring.CopyTo(bytesData, 36);

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        subChunk2.CopyTo(bytesData, 40);

    }

}