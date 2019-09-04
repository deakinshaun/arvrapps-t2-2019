using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using System.Text;

public class PlayCamera : MonoBehaviour
{
    public Material camTexMaterial;

    private WebCamTexture webcamTexture;

    public RawImage camDisplay;

    public RenderTexture ARCamTex;

    //private Texture ARCoreCameraTexture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //onCopyTexture();
        //if (webcamTexture == null)
        //
        //webcamTexture = new WebCamTexture();

        //camTexMaterial.mainTexture = webcamTexture;

        //}
        //if (!webcamTexture.isPlaying)
        //{
        //webcamTexture.Play();
        //}

    }

    public void onCopyTexture()
    {
        Debug.Log("onCopyTexture");
        //using (var ARCoreCameraImageBytes = Frame.CameraImage.AcquireCameraImageBytes())
        //{
        //    if (!ARCoreCameraImageBytes.IsAvailable)
        //    {
        //        return;
        //    }
        //    Debug.Log("ARCoreCameraImageBytes 111" + ARCoreCameraImageBytes.Width + " " + ARCoreCameraImageBytes.Height + " " + ARCoreCameraImageBytes.YRowStride);
        //    //printByteArray()
        //    int bufferSize = ARCoreCameraImageBytes.Width * ARCoreCameraImageBytes.Height;
        //    byte[] singleColorChannel = new byte[bufferSize];
        //    //byte[] singleColorChannel = new byte[6] { 0, 200, 100, 87, 214, 178 };
        //    System.Runtime.InteropServices.Marshal.Copy(ARCoreCameraImageBytes.Y, singleColorChannel, 0, bufferSize);
        //    Debug.Log("singleColorChannel ");
        //    printByteArray(singleColorChannel);
        //    Texture2D finalTex = new Texture2D(ARCoreCameraImageBytes.Width, ARCoreCameraImageBytes.Height, TextureFormat.R8, false);
        //    //Texture2D finalTex = new Texture2D(3, 2, TextureFormat.R8, false);
        //    finalTex.LoadRawTextureData(singleColorChannel);
        //    finalTex.Apply();
        //    camDisplay.texture = finalTex;
            
        //}
        camDisplay.texture = Frame.CameraImage.Texture;
    }

    public void onScreenshot()
    {
        Debug.Log("onScreenshot");
        RenderTexture.active = ARCamTex;
        Texture2D image = new Texture2D(640, 480, TextureFormat.RGBA32, false);
        //image.ReadPixels(new Rect(0, 0, 640, 480), 0, 0);
        //image.Apply();
    }
    void printByteArray(byte[] byteArray)
    {
        StringBuilder result = new StringBuilder("byte[] array { ");
        foreach (var b in byteArray)
        {
            result.Append(b + ",");
        }
        result.Append("}");
        Debug.Log(result.ToString());
    }
}
