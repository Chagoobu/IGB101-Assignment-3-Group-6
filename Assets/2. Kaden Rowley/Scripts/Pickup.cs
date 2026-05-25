using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kaden
{
    public class Pickup : MonoBehaviour
    {
        GameManager gameManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gameManager.currentPickups += 1;
                Destroy(gameObject);
            }
        }
    }
}