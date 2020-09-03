using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class NotificationManager : MonoBehaviour
    {
        public Text notificationText;
        private string[] sentences = new string[3];

        void Start()
        {
            sentences[0] = "Water & Rest X2!";
            sentences[1] = "Fruits and Vegetables X2!";
            sentences[2] = "Running X2!";
        }


        public void DisplayNotification(int index)
        {
            notificationText.text = sentences[index];
        }
    }
}
