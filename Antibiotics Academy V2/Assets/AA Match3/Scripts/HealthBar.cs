using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;                              //reference to the health slider
        public Gradient gradient;                          //reference to the health gradient
        public Image fill;                                 //reference to the image

        public void SetHealth(float health)                //function to set the health based on the game's health
        {
            slider.value = health;                         //sets slider value according to the health given in the argument
            fill.color = gradient.Evaluate(slider.normalizedValue);  //change the color of the fill according to the health
        }
    }
}
