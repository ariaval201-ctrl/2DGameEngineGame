using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Image fadeImage; 
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (fadeImage)
            fadeImage.color = new Color(1, 1, 1, 0); 
    }

    public void FadeToWhite(System.Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(onComplete));
    }

    private IEnumerator FadeCoroutine(System.Action onComplete)
    {
        float t = 0;
        Color startColor = fadeImage.color;
        Color endColor = new Color(1, 1, 1, 1);

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, t / fadeDuration);
            yield return null;
        }

        fadeImage.color = endColor;

        onComplete?.Invoke();
    }

    public void FadeFromWhite(System.Action onComplete = null)
{
    StartCoroutine(FadeFromCoroutine(onComplete));
}

private IEnumerator FadeFromCoroutine(System.Action onComplete)
{
    float t = 0;
    Color startColor = fadeImage.color; 
    Color endColor = new Color(1, 1, 1, 0); 

    while (t < fadeDuration)
    {
        t += Time.deltaTime;
        fadeImage.color = Color.Lerp(startColor, endColor, t / fadeDuration);
        yield return null;
    }

    fadeImage.color = endColor;
    onComplete?.Invoke();
}
}