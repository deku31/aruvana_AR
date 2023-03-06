
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Android;

public class ScreenShootAndShare : MonoBehaviour
{
    private string folderName = "Screenshots";
    public int screenshotIndex = 0;
    private Texture2D screenshotTexture;
    public GameObject ResultImagePanel;
    public Image imgResult;

    private bool isSharing = false;

    void RequestPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }
    private void Start()
    {
        RequestPermission();
    }
    private void Update()
    {
        
    }
    IEnumerator SaveScreenshotCoroutine()
    {
        // Wait for end of frame to take screenshot
        yield return new WaitForEndOfFrame();

        // Get screenshot as texture
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        // Save screenshot to device gallery
        string fileName = "Screenshot_" + screenshotIndex + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, folderName);
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        byte[] imageBytes = texture.EncodeToPNG();
        File.WriteAllBytes(fullPath, imageBytes);

        // Refresh gallery to display new screenshot
        using (AndroidJavaClass refreshGalleryClass = new AndroidJavaClass("com.example.androidutilities.RefreshGallery"))
        {
            refreshGalleryClass.CallStatic("Refresh", filePath);
        }

        // Clean up resources
        Destroy(texture);
    }

    public void sreenshoot()
    {
        StartCoroutine(SaveScreenshotCoroutine());
        StartCoroutine(showResult(0.5f));
    }
    IEnumerator showResult(float time)
    {
        yield return new WaitForSeconds(time);
        LoadScreenshot();
        ResultImagePanel.SetActive(true);
        StopCoroutine(showResult(0));
    }
    private void LoadScreenshot()
    {
        string fileName = "Screenshot_" + screenshotIndex + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, folderName);
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(fullPath))
        {
            byte[] bytes = File.ReadAllBytes(fullPath);
            
            screenshotTexture = new Texture2D(1, 1);
            screenshotTexture.LoadImage(bytes);
            imgResult.sprite = Sprite.Create(screenshotTexture, new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.Log("Screenshot not found: " + fullPath);
        }
    }

    public void Done()
    {
        screenshotIndex++;
        ResultImagePanel.SetActive(false);
    }

    public void Delate()
    {
        string fileName = "Screenshot_" + screenshotIndex + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, folderName);
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(fullPath))
        {
            byte[] bytes = File.ReadAllBytes(fullPath);

            Debug.Log("delate code not redy");

        }
        else
        {
            Debug.Log("Screenshot not found: " + fullPath);
        }
    }



    public void OnShareButtonClick()
    {
        if (!isSharing)
        {
            StartCoroutine(ShareScreenshotCoroutine());
        }
    }

    IEnumerator ShareScreenshotCoroutine()
    {
        isSharing = true;

        yield return new WaitForEndOfFrame();
        string fileName = "Screenshot_" + screenshotIndex + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, folderName);
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(fullPath))
        {
            byte[] bytes = File.ReadAllBytes(fullPath);

            screenshotTexture = new Texture2D(1, 1);
            screenshotTexture.LoadImage(bytes);
            new NativeShare().AddFile(fullPath)
            .SetSubject("Screenshoot AR")
            .SetText("this a result image capture from screenshoot imange")
            .Share();
        }
        isSharing = false;
        
    }

}
