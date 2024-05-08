using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;
    public Gradient gradient;
    public Image fill;
    public float speed = 2;
    private float targetHealth;
    private bool lerpingHealth = false;
    private float timeScale = 0;

    void Start()
    {
        //slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float health)
    {

        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetMaxHealthLerp(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetHealthLerp(float health)
    {
        targetHealth = health;
        timeScale = 0;

        if (!lerpingHealth)
            StartCoroutine(LerpHealth());
    }


    private IEnumerator LerpHealth()
    {
       
        float startHealth = slider.value;

        lerpingHealth = true;

        while (timeScale < 1)
        {
            timeScale += Time.deltaTime * speed;
            slider.value = Mathf.Lerp(startHealth, targetHealth, timeScale);
            fill.color = gradient.Evaluate(slider.normalizedValue);
            yield return null;
        }
        lerpingHealth = false;
        yield return null;
    }
}
