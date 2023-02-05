using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerStats stats;
    public float maxWidth;
    public Image bar;

    private void Update()
    {
        if (stats)
        {
            bar.rectTransform.sizeDelta = new Vector2((stats.health / 100f) * maxWidth, 25);
        }
    }
}
