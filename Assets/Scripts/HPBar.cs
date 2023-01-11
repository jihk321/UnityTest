using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup healthbar;
    private float dist;

    private Transform lookAtTarget;

    // private void Awake()
    // {
    //     text = GetComponentInChildren<Text>();
    //     slider = GetComponentInChildren<Slider>();
    // }

    public void ChnageHealth(float value, float maxValue)
    {
        text.text = $"{value} / {maxValue}";
        slider.value = value / maxValue;
    }

    public void LookAtTarget(Transform lookAtTarget)
    => this.lookAtTarget = lookAtTarget;

    private void Distance() {
        dist = Vector3.Distance(this.lookAtTarget.position, transform.position);
        // Debug.Log(dist);
        if (dist > 10.0f) healthbar.alpha = 0f;
        else healthbar.alpha = 1f;
    }

    public IEnumerator FarAttack() {

        if (healthbar.alpha == 0f) {
            healthbar.alpha = 1f;
            yield return new WaitForSeconds(1);
            // healthbar.alpha = 0f;
        } 
    }

    private void Update()
    {
        if (this.lookAtTarget == null)
            return ;
        Distance();
        transform.LookAt(lookAtTarget); 
    }

}
