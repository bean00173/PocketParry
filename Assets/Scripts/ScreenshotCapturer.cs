using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotCapturer : MonoBehaviour
{
    int totalScreenshots;
    public KeyCode screenshotKey = KeyCode.P;

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
            Debug.Log($"Capturing Screen... Existing Files : {totalScreenshots}");
            if (totalScreenshots < 100)
            {
                ScreenCapture.CaptureScreenshot($"Assets/Screenshots/Screenshot_{totalScreenshots}.png", 4);
            }
            else if(totalScreenshots < 10)
            {
                ScreenCapture.CaptureScreenshot($"Assets/Screenshots/Screenshot_0{totalScreenshots}.png", 4);
            }
            else
            {
                ScreenCapture.CaptureScreenshot($"Assets/Screenshots/Screenshot_00{totalScreenshots}.png", 4);
            }

            totalScreenshots++;
        }
    }
}
