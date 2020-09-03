using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        public void SetHealth(float health)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
