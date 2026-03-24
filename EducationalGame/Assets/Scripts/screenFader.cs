using System;
using System.Threading.Tasks;
using UnityEngine;

public class screenFader : MonoBehaviour
{
    public static screenFader instance;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 0.5f;
    
    public void Awake()
    {
        if  (instance == null) instance = this;
        else Destroy(gameObject);
    }

    async Task Fade(float targetTransparency)
    {
        float start = canvasGroup.alpha, t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, targetTransparency, t / fadeDuration);
            await Task.Yield(); // gives some time 
        }
        canvasGroup.alpha = targetTransparency; // a little backup
    }

    public async Task FadeOut()
    {
        await Fade(1); // fade to black
    }
    
    public async Task FadeIn()
    {
        await Fade(0); // make transparent 
    }
}
