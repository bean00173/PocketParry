using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotCapturer : MonoBehaviour
{
    public static ScreenshotCapturer instance;
    int totalScreenshots;
    public KeyCode screenshotKey = KeyCode.P;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        var info = new DirectoryInfo("Assets/Screenshots");
        var fileInfo = info.GetFiles();
        if(fileInfo != null)
        {
            foreach (FileInfo file in fileInfo)
            {
                if(file.Extension != ".meta")
                {
                    Debug.Log(file.Name);
                    totalScreenshots++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(screenshotKey))
        {
            TakeScreenshot("Manual_Capture");
        }
    }

    public void TakeScreenshot(string prefix)
    {
        Debug.Log($"Capturing Screen... Existing Files : {totalScreenshots}");
        if (totalScreenshots < 10)
        {
            ScreenCapture.CaptureScreenshot($"Assets/Screenshots/{prefix}_00{totalScreenshots}.jpg", 4);
        }
        else if (totalScreenshots < 100)
        {
            ScreenCapture.CaptureScreenshot($"Assets/Screenshots/{prefix}_0{totalScreenshots}.jpg", 4);
        }
        else
        {
            ScreenCapture.CaptureScreenshot($"Assets/Screenshots/{prefix}_{totalScreenshots}.jpg", 4);
        }

        totalScreenshots++;
    }
}
