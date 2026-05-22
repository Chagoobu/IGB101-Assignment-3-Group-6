using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Kaden
{
    public class GameManager : MonoBehaviour
    {
        public GameObject player;

        //pickup and level completion logic
        public int currentPickups = 0;
        public int maxPickups = 5;
        public bool levelComplete = false;

        public Text pickupText;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            LevelCompleteCheck();
            UpdateGUI();
        }

        void LevelCompleteCheck()
        {
            if (currentPickups >= maxPickups)
            {
                levelComplete = true;
            }
            else
            {
                levelComplete = false;
            }
        }

        void UpdateGUI()
        {
            if (pickupText != null)
            {
                pickupText.text = "Pickups: " + currentPickups + "/" + maxPickups;
            }
        }
    }
}
