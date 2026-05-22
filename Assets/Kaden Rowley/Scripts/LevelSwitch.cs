using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


namespace Kaden
{
    public class LevelSwitch : MonoBehaviour
    {
        GameManager gameManager;
        public string nextLevel;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }


        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && gameManager.levelComplete == true)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}