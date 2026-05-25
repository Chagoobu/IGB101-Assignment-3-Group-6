using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
   public string nextLevels;

   public void ReplayGame()
   {
	   SceneManager.LoadScene(nextLevels);
   }
}