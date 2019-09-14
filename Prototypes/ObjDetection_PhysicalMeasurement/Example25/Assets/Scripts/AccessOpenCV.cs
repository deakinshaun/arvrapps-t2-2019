using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using GoogleARCore;

public class AccessOpenCV : MonoBehaviour
{
    //public Material cameraMaterial;

    public GameObject markerTemplate;

    public GameObject markerParent;

    public Camera renderTexCam;

    //public RawImage renderImage;

    private bool modelReady = false;

    private float delayTime = 0.0f;

    public Text text;

    private string[] CLASSES = { "background", "aeroplane", "bicycle", "bird", "boat", "bottle", "bus", "car", "cat", "chair", "cow",
        "diningtable", "dog", "horse", "motorbike", "person", "pottedplant","sheep", "sofa", "train", "tvmonitor" };

    [DllImport("VisualRecognition")]
    private static extern void prepareModel(string dirname);

    [DllImport("VisualRecognition")]
    private static extern int doRecognise(byte[] imageData, int width, int height);

    [DllImport("VisualRecognition")]
    private static extern void retrieveMatch(int i, ref int category, ref float confidence, ref float sx, ref float sy, ref float ex, ref float ey);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(prepareModel());
    }

    IEnumerator prepareModel()
    {
        yield return StartCoroutine(extractFile("", "MobileNetSSD_deploy.caffemodel"));
        yield return StartCoroutine(extractFile("", "MobileNetSSD_deploy.prototxt.txt"));
        prepareModel(Application.persistentDataPath);
        modelReady = true;
    }

    // Copy file from the android package to a readable/writeable region of the host file system.
    IEnumerator extractFile (string assetPath, string assetFile)
    {
        // Source is the streaming assets path.
        string sourcePath = Application.streamingAssetsPath + "/" + assetPath + assetFile;
        if ((sourcePath.Length > 0) && (sourcePath[0] == '/'))
        {
            sourcePath = "file://" + sourcePath;
        }
        string destinationPath = Application.persistentDataPath + "/" + assetFile;
        // Files must be handled via a WWW to extract.
        WWW w = new WWW(sourcePath);
        yield return w;
        try
        {
            File.WriteAllBytes(destinationPath, w.bytes);
        }
        catch (Exception e)
        {
            Debug.Log("Issue writing " + destinationPath + " " + e);
        }
        Debug.Log(sourcePath + " -> " + destinationPath + " " + w.bytes.Length);
    }
    private void clearVisuals()
    {
        foreach (Transform child in markerParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void addVisual(string name, float confidence, float sx, float sy, float ex, float ey)
    {
        GameObject g = Instantiate(markerTemplate);
        g.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * sx, Screen.height * (1-ey));
        Debug.Log("x position " + Screen.width * sx + "," + "y position" + Screen.height * (1-sy));
        Debug.Log("Rect " + g.GetComponent<RectTransform>().rect);
        g.transform.localScale = new Vector3(Screen.width * Mathf.Abs(sx - ex) / 100, Screen.height * Mathf.Abs(sy - ey) / 100, 1.0f);
        Debug.Log("local scale x " + Screen.width * Mathf.Abs(sx - ex) / 100 + "," + "local scale y " + Screen.height * Mathf.Abs(sy - ey) / 100);
        g.transform.GetChild(0).GetComponent<Text>().text = name + "\n" + confidence;
        g.transform.SetParent(markerParent.transform, false);

        TrackableHit sHit, eHit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;
        //Debug.Log("touch.position.x " + touch.position.x);
        //Debug.Log("touch.position.y " + touch.position.y);
        Frame.Raycast(Screen.width * sx, Screen.height * sy, raycastFilter, out sHit);
        Frame.Raycast(Screen.width * ex, Screen.height * sy, raycastFilter, out eHit);
        float dx = sHit.Pose.position.x - eHit.Pose.position.x;
        float dy = sHit.Pose.position.y - eHit.Pose.position.y;
        float dz = sHit.Pose.position.z - eHit.Pose.position.z;
        float distanceMeters = (float)Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
        g.transform.GetChild(1).GetComponent<Text>().text = distanceMeters + "m";
    }

    // Update is called once per frame
    void Update()
    {
        delayTime += Time.deltaTime;
        if (modelReady && (delayTime > 2.0f))
        {
            clearVisuals();
            delayTime = 0.0f;

            Debug.Log("Screen.width " + Screen.width + "," + Screen.height + "Screen.height");
            RenderTexture.active = renderTexCam.targetTexture;

            Texture2D image = new Texture2D(renderTexCam.targetTexture.width, renderTexCam.targetTexture.height, TextureFormat.RGBA32, false);
            image.ReadPixels(new Rect(0, 0, renderTexCam.targetTexture.width, renderTexCam.targetTexture.height), 0, 0);
            image.Apply();

            Debug.Log("11111111111111111111111111111111111111111111111");
            int numMatch = doRecognise(image.GetRawTextureData(), image.width, image.height);
            Debug.Log("2222222222222222222222222222222222222222222222222");
            text.text = "Matches: " + numMatch + "\n";
            for (int i = 0; i < numMatch; i++)
            {
                int category = -1;
                float confidence = 0.0f;
                float sx = 0, sy = 0, ex = 0, ey = 0;
                retrieveMatch(i, ref category, ref confidence, ref sx, ref sy, ref ex, ref ey);
                if (confidence > 0.5f)
                {
                    text.text += "Match: " + CLASSES[category] + " " + confidence + " " + sx + " " + sy + " " + ex + " " + ey + "\n";
                    Debug.Log("Match: " + CLASSES[category] + " " + confidence + " " + sx + " " + sy + " " + ex + " " + ey);
                    addVisual(CLASSES[category], confidence, sx, sy, ex, ey);
                }
            }
        }
    }

    public void Toggle()
    {
        if (transform.gameObject.activeSelf == true)
            transform.gameObject.SetActive(false);
        else transform.gameObject.SetActive(true);
    }
}
