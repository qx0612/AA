using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public enum HealthStates
    {
        Sick,
        Neutral,
        Healthy
    }


    public class HealthManager : MonoBehaviour
    {
        public HealthStates healthState;
        private NotificationManager nm;
        private DisplayEndUI display;
        private FindMatch fm;
        public SpriteRenderer sr;
        public Sprite[] sprites;

        public GameObject emojiState;

        public int maxHealth = 100;
        public float currentHealth;
        public float min_sickHealth = 1f;
        public float min_neutralHealth = 25f;
        public float min_healthyHealth = 75f;

        private float rateOfDecrease;
        private float addNormal = 1f;
        private float addBonus = 2f;

        private float addHealth;

        public HealthBar healthBar;

        private string bonusTag;
        private string bonusTag1;
        private float healthToAdd;

        private Animator anim;
        public string boolNameSleep; // red
        public string boolNameEat; // yellow
        public string boolNameRun; // green

        public GameObject bannerEat;
        public GameObject bannerSleep;
        public GameObject bannerRun;

        public AudioSource audiosrc;

        void Start()
        {
            fm = FindObjectOfType<FindMatch>();
            display = FindObjectOfType<DisplayEndUI>();
            nm = FindObjectOfType<NotificationManager>();
            currentHealth = min_neutralHealth;
            healthState = HealthStates.Sick;
            healthBar.SetHealth(min_neutralHealth);

            anim = GetComponent<Animator>();
            audiosrc = GetComponent<AudioSource>();
        }

        void FixedUpdate()
        {
            currentHealth -= rateOfDecrease * Time.deltaTime;
            healthBar.SetHealth(currentHealth);

            CheckState();
        }

        private void CheckState()
        {
            if (currentHealth >= 100)
            {
                display.DisplayWinUI();
            }
            else if (currentHealth < 1)
            {
                display.DisplayDeathUI();
            }
            else if (currentHealth < min_neutralHealth && currentHealth > 1) // red health
            {
                healthState = HealthStates.Sick;
                nm.DisplayNotification(0);
                sr.sprite = sprites[0];

                bannerSleep.SetActive(true);
                bannerEat.SetActive(false);
                bannerRun.SetActive(false);

                SetBoolActiveSleep();
                SetBoolInactiveRun();
                SetBoolInactiveEat();

                if (!audiosrc.isPlaying)
                {
                    audiosrc.Play();
                }
            }

            else if (currentHealth >= min_neutralHealth && currentHealth < min_healthyHealth) // yellow health
            {
                healthState = HealthStates.Neutral;
                nm.DisplayNotification(1);
                sr.sprite = sprites[1];

                if (audiosrc.isPlaying)
                {
                    audiosrc.Stop();
                }

                bannerSleep.SetActive(false);
                bannerEat.SetActive(true);
                bannerRun.SetActive(false);

                SetBoolActiveEat();
                SetBoolInactiveRun();
                SetBoolInactiveSleep();
            }

            else if (currentHealth >= min_healthyHealth && currentHealth < 100f) // green health
            {
                healthState = HealthStates.Healthy;
                nm.DisplayNotification(2);
                sr.sprite = sprites[2];

                if (audiosrc.isPlaying)
                {
                    audiosrc.Stop();
                }

                bannerSleep.SetActive(false);
                bannerEat.SetActive(false);
                bannerRun.SetActive(true);

                SetBoolActiveRun();
                SetBoolInactiveEat();
                SetBoolInactiveSleep();
            }
            SetBonus(healthState);
        }

        private void SetBonus(HealthStates states)
        {
            switch (states)
            {
                case HealthStates.Sick:
                    bonusTag = "Water";
                    bonusTag1 = "Sleeping";
                    rateOfDecrease = 1.0f;
                    break;
                case HealthStates.Neutral:
                    bonusTag = "Fruit";
                    bonusTag1 = "Vegetable";
                    rateOfDecrease = 1.25f;
                    break;
                case HealthStates.Healthy:
                    bonusTag = "Running";
                    bonusTag1 = "?";
                    rateOfDecrease = 1.5f;
                    break;
            }
        }

        public void CalcAddHealth(string pieceTag)
        {
            int prefabCount = 0;
            int bonusCount = 0;

            if (pieceTag == bonusTag || pieceTag == bonusTag1)
            {
                bonusCount++;
            }
            else
            {
                prefabCount++;

            }
            healthToAdd = (prefabCount * addNormal) + (bonusCount * addBonus);

            //Debug.Log(healthToAdd);

            currentHealth += healthToAdd;

            healthToAdd = 0;
            bonusCount = 0;
            prefabCount = 0;
        }

        // Yellow Health
        public void SetAnimBoolEat(bool active)
        {
            anim.SetBool(boolNameEat, active);
        }

        public void SetBoolActiveEat()
        {
            anim.SetBool(boolNameEat, true);
        }

        public void SetBoolInactiveEat()
        {
            anim.SetBool(boolNameEat, false);
        }

        // Red Health
        public void SetAnimBoolSleep(bool active)
        {
            anim.SetBool(boolNameSleep, active);
        }

        public void SetBoolActiveSleep()
        {
            anim.SetBool(boolNameSleep, true);
        }

        public void SetBoolInactiveSleep()
        {
            anim.SetBool(boolNameSleep, false);
        }

        // Green Health
        public void SetAnimBoolRun(bool active)
        {
            anim.SetBool(boolNameRun, active);
        }

        public void SetBoolActiveRun()
        {
            anim.SetBool(boolNameRun, true);
        }

        public void SetBoolInactiveRun()
        {
            anim.SetBool(boolNameRun, false);
        }
    }
}

