using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class AntibioticAbility : MonoBehaviour
    {
        public HealthBar healthBar;
        public HealthManager healthManager;
        private int counter = 3;

        public Button btn;

        private void Start()
        {
            btn.onClick.AddListener(Effectiveness);
        }

        private void Update()
        {
            if (healthManager.healthState == HealthStates.Sick && counter > 0)
            {
                btn.interactable = true;
            }
            else
            {
                btn.interactable = false;
            }
        }

        private void Effectiveness()
        {
            if (counter == 3)
            {
                healthManager.currentHealth += 25;
                counter -= 1;
            }

            else if (counter == 2)
            {
                healthManager.currentHealth += 15;
                counter -= 1;
            }

            else if (counter == 1)
            {
                healthManager.currentHealth += 10;
                counter -= 1;
            }
        }

    }
}