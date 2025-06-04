using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f; 
    private float accumulated = 0f;     
    private int frames = 0;             
    private float timeLeft;             
    private float fps = 0f;

    void Start()
    {
        timeLeft = updateInterval;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        accumulated += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeLeft <= 0.0)
        {
            fps = accumulated / frames;
            timeLeft = updateInterval;
            accumulated = 0f;
            frames = 0;
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = Screen.height * 2 / 50;
        style.normal.textColor = Color.white;

        Rect rect = new Rect(10, 10, 200, 40);
        string text = string.Format("{0:0.} FPS", fps);
        GUI.Label(rect, text, style);
    }
}
