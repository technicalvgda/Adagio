using UnityEngine;
using System.Collections;

// Helper class to create dank frame-by-frame animations, ignore this
// Idea stolen from here: https://gist.github.com/bitbutter/302da1c840b7c93bc789
// - Set Camera's Clear Flags to Skybox
// - To be safe, set Camera's Background color to 314D7900 (originally 314D7905)
// - Set the public fields in editor
// Press P to record, framerate is permanently fucked after this, so restart the game.
// The output image will be lossy. Implement a better Calculate() to fix it.

public class CapturePNG : MonoBehaviour
{
    public string outputFolder;
    public int frameRate = 24;
    public int framesCount = 1;

    int framesRendered = 0;
    bool capture = false;

    int screenWidth;
    int screenHeight;

    GameObject whiteObject;
    GameObject blackObject;
    Camera cameraWhite;
    Camera cameraBlack;
    Camera cameraMain;
    Texture2D texWhite; // slap this
    Texture2D texBlack; // with this
    Texture2D texTransparent; // output to this

    void Awake()
    {
        cameraMain = this.GetComponent<Camera>();
        InitializeCamera();
        InitializeFields();
    }

    void InitializeCamera()
    {
        whiteObject = new GameObject();
        cameraWhite = whiteObject.AddComponent<Camera>();
        cameraWhite.CopyFrom(cameraMain);
        cameraWhite.backgroundColor = Color.white;
        whiteObject.transform.SetParent(transform, true);

        blackObject = new GameObject();
        cameraBlack = blackObject.AddComponent<Camera>();
        cameraBlack.CopyFrom(cameraMain);
        cameraBlack.backgroundColor = Color.black;
        blackObject.transform.SetParent(transform, true);
    }

    void InitializeFields()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        texBlack = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
        texWhite = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
        texTransparent = new Texture2D(screenWidth, screenHeight, TextureFormat.ARGB32, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !capture)
        {
            framesRendered = 0;
            capture = true;
            // Slows game playback time to allow screenshots to be saved between frames
            Time.captureFramerate = frameRate;
            print("Record started");
        }
    }

    void LateUpdate()
    {
        if (capture)
        {
            StartCoroutine(CaptureFrame());
        }
    }

    IEnumerator CaptureFrame()
    {
        yield return new WaitForEndOfFrame();
        if (framesRendered < framesCount)
        {
            RenderToTexture(cameraBlack, texBlack);
            RenderToTexture(cameraWhite, texWhite);
            CalculateToTexture(texWhite, texBlack, texTransparent);
            WritePNG(texTransparent, outputFolder);

            framesRendered++;
        }
        else
        {
            print("Record over");
            capture = false;
            StopAllCoroutines();
        }
    }

    void RenderToTexture(Camera camera, Texture2D tex)
    {
        camera.enabled = true;
        camera.Render();
        tex.ReadPixels(new Rect(0, 0, screenWidth, screenHeight), 0, 0);
        tex.Apply();
        camera.enabled = false;
    }
    
    void CalculateToTexture(Texture2D texWhite, Texture2D texBlack, Texture2D texOutput)
    {
        Color color;
        // rows
        for (int y = 0; y < texOutput.height; ++y)
        {
            // columns
            for (int x = 0; x < texOutput.width; ++x)
            {
                float alpha = texWhite.GetPixel(x, y).r - texBlack.GetPixel(x, y).r;
                alpha = 1.0f - alpha;
                if (alpha == 0)
                {
                    color = Color.clear;
                }
                else
                {
                    color = texBlack.GetPixel(x, y) / alpha;
                }
                color.a = alpha;
                texOutput.SetPixel(x, y, color);
            }
        }
    }

    void WritePNG(Texture2D tex, string folder)
    {
        string filename = string.Format("{0}/output_{1}_{2}.png", folder, framesRendered,
                System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        byte[] bytes = tex.EncodeToPNG();

        try
        {
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }
            System.IO.File.WriteAllBytes(filename, bytes);
        }
        catch (System.IO.IOException e)
        {
            print(e.Message);
            print(e.StackTrace);
        }
    }
}
