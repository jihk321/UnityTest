using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStyle : MonoBehaviour
{
    [SerializeField] private CanvasGroup reloadM;
    [SerializeField] private GameObject reloadObject;
    // int min, max;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(FadeIn(reloadM,1f));
    }

    private void OnDisable()
    {
        StartCoroutine(FadeOut(reloadM,1f));
    }

    IEnumerator FadeIn(CanvasGroup target, float sec) {
        target.alpha = 0;
        while (target.alpha == 1) {
            target.alpha += 0.1f;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator FadeOut(CanvasGroup target, float sec) {
        target.alpha = 1;
        while (target.alpha == 0) {
            target.alpha -= 0.1f;
            yield return new WaitForEndOfFrame();
        }
    }
}
