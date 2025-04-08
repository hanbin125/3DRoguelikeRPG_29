using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCondition : MonoBehaviour
{
    public float curentCooldown;
    public float maximumCooldown;
    public float startValue;
    public float passiveValue;
    public float plusValue;
    public Image uiBar;

    private void Start()
    {

    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    public void Add(float amount)
    {
        curentCooldown = Mathf.Min(curentCooldown + amount, maximumCooldown);
    }

    public void Subtract(float amount)
    {
        curentCooldown = Mathf.Max(curentCooldown - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curentCooldown / maximumCooldown;
    }

}
