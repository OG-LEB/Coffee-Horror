using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance;

    [Header("UI Components")]
    public TextMeshProUGUI messageText; 

    [Header("Fade Settings")]
    public float fadeDuration = 0.5f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        SetAlpha(0);
        messageText.text = "";
    }

    public void ShowMessage(string message, float duration)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        messageText.text = message;

        // Fade In
        yield return FadeText(0f, 1f, fadeDuration);

        // Wait
        yield return new WaitForSeconds(duration);

        // Fade Out
        yield return FadeText(1f, 0f, fadeDuration);

        messageText.text = "";
    }

    private IEnumerator FadeText(float from, float to, float duration)
    {
        float time = 0f;
        Color c = messageText.color;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(from, to, time / duration);
            messageText.color = new Color(c.r, c.g, c.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        messageText.color = new Color(c.r, c.g, c.b, to);
    }

    private void SetAlpha(float a)
    {
        Color c = messageText.color;
        messageText.color = new Color(c.r, c.g, c.b, a);
    }
}
