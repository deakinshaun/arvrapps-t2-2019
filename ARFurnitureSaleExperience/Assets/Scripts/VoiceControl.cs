using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using System;

[Serializable]
public class TextResponse
{
    public string RecognitionStatus;
    public string DisplayText;
    public int Offset;
    public int Duration;
}

public class VoiceControl : MonoBehaviour
{
    private string subscriptionKey = "7cbc5039c88d4b019f1e80c4a070be2e";
    private string token;

    //private float shakeDetectionThreshold = 1.5f;
    //private float shakeDetectionInterval = 3.0f;
    //private float timeFromLastShake;

    public Text resultText;
    //public Text statusText;

    public GameObject recordButton;

    //controllable button via voice
    public Button scanImageButton;
    public Button furnitureButton;
    public Button openCollectionButton;
    public Button sofaButton;
    public Button chairButton;
    public Button placeButton;
    public Button measureButton;
    public Button clearButton;
    public Button gridButton;
    public Button selectButton;
    public Button deleteButton;

    // length of any recording sent. 10 s is the current limit.
    private int recordDuration = 2;

    private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
    {
        // all Certificates are accepted
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;
    }

    public void SpeechToText(byte[] wavData)
    {
        Debug.Log("Unity SpeechToText time" + Time.time);
        // Send the request to the service.
        string fetchUri = "https://westus.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fetchUri + "?language=en-US&format=simple");
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "POST";
        request.ContentType = "audio/wav; codecs=audio/pcm; samplerate=16000";
        request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;

        Debug.Log("Before write wav to request time" + Time.time);
        Stream rs = request.GetRequestStream();
        rs.Write(wavData, 0, wavData.Length);
        rs.Close();
        Debug.Log("After write wav to request time" + Time.time);
        var response = (HttpWebResponse)request.GetResponse();
        Debug.Log("After getting result time" + Time.time);
        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        TextResponse textResponse = JsonUtility.FromJson<TextResponse>(responseString);
        string result = textResponse.DisplayText;
        resultText.text = result;
        string status = textResponse.RecognitionStatus;
        //statusText.text = status;
        Debug.Log("Unity Response from service: " + responseString);
        Debug.Log("Unity display Text: " + result);
        if (status == "Success")
            OnVoiceCommandResponse(result);
    }
    void OnVoiceCommandResponse(string command)
    {
        Debug.Log("OnVoiceCommandResponse: " + command);
        if (command.ToLower().Contains("scan"))
        {
            if (scanImageButton.gameObject.activeSelf) scanImageButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("furniture"))
        {
            if (furnitureButton.gameObject.activeSelf) furnitureButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("collection"))
        {
            if (openCollectionButton.gameObject.activeSelf) openCollectionButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("sofa"))
        {
            if (sofaButton.gameObject.activeSelf) sofaButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("chair"))
        {
            if (chairButton.gameObject.activeSelf) chairButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("place"))
        {
            if (placeButton.gameObject.activeSelf) placeButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("select"))
        {
            if (selectButton.gameObject.activeSelf) selectButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("destroy"))
        {
            if (deleteButton.gameObject.activeSelf) deleteButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("measure"))
        {
            if (measureButton.gameObject.activeSelf) measureButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("clear"))
        {
            if (clearButton.gameObject.activeSelf) clearButton.onClick.Invoke();
        }
        if (command.ToLower().Contains("surface"))
        {
            if (gridButton.gameObject.activeSelf) gridButton.onClick.Invoke();
        }
    }
    private IEnumerator recordAudio()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Unity Name: " + device);
        }
        Debug.Log("Unity Start recording " + Time.time);
        // Set the microphone recording. Service requires 16 kHz sampling.
        AudioClip audio = Microphone.Start(null, false, recordDuration, 16000);
        yield return new WaitForSeconds(recordDuration);
        Microphone.End(null);
        recordButton.GetComponent<Button>().interactable = true;
        recordButton.GetComponent<Image>().enabled = false;
        //// Play the recording back , to validate it was recorded correctly.
        //AudioSource audioSource = GetComponent<AudioSource>();
        //audioSource.clip = audio;
        //audioSource.Play();

        // Convert it to a wav file , and upload to the service.
        byte[] wavData = ConvertToWav(audio);
        SpeechToText(wavData);
    }

    public void Trigger()
    {
        recordButton.GetComponent<Button>().interactable = false;
        recordButton.GetComponent<Image>().enabled = true;
        //Authentication ();
        StartCoroutine(recordAudio());
    }

    // Remaining functions adapted from: https://gist.github.com/darktable/2317063
    const int HEADER_SIZE = 44;
    static byte[] ConvertToWav(AudioClip clip)
    {
        var samples = new float[clip.samples];
        clip.GetData(samples, 0);
        Int16[] intData = new Int16[samples.Length];
        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

        Byte[] bytesData = new Byte[HEADER_SIZE + samples.Length * 2];
        //bytesData array is twice the size of
        //dataSource array because a float converted in Int16 is 2 bytes.

        int rescaleFactor = 32767; //to convert float to Int16
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
        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); //sampleRate* bytesPerSample*number of channels, here 44100 * 2 * 2
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
    // Update is called once per frame
    void Update()
    {

    }
}
