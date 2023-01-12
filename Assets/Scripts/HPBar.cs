using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup healthbar;
    [SerializeField] private float hideDistance = 10.0f;
    private Coroutine routine;
    private Transform lookAtTarget;

    // private void Awake()
    // {
    //     text = GetComponentInChildren<Text>();
    //     slider = GetComponentInChildren<Slider>();
    // }

    private void Awake()
    {
        routine = null;
    }

    public void ChnageHealth(float value, float maxValue)
    {
        text.text = $"{value} / {maxValue}";
        slider.value = value / maxValue;
    }

    public void LookAtTarget(Transform lookAtTarget)
    => this.lookAtTarget = lookAtTarget;

    private void Distance() {
        float dist = Vector3.Distance(this.lookAtTarget.position, transform.position);
        // Debug.Log(dist);
        if (dist > hideDistance)
            healthbar.alpha = 0f;
        else
            healthbar.alpha = 1f;
    }

    public void ShowDuring(float showTime)
    {
        if (routine != null)
            StopCoroutine(routine);
        
        routine = StartCoroutine(ShowHealthBar(showTime));
    }

    public IEnumerator ShowHealthBar(float showTime) {
        // Debug.Log("코루틴 실행됨");
        healthbar.alpha = 1f;
        yield return new WaitForSeconds(showTime);
        
        routine = null;
    }

    private void Update()
    {
        if (this.lookAtTarget == null)
            return ;
        
        if (routine == null)
            Distance();
        
        transform.LookAt(lookAtTarget); 
    }

}
