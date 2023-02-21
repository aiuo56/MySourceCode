using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
   public static bool GameOver = false;
   public GameObject deadUI, finishUI;


   public void RealFinish()
   {
      finishUI.SetActive(true);
      GameOver = false;
   }



   public void Retry()
   {
      deadUI.SetActive(true);

   }
   public void GameRetry()
   {
      SceneManager.LoadScene("SampleScene");
      GameOver = false;
   }

   public void GameFinish()
   {
      SceneManager.LoadScene("StartMenu");
      GameOver = false;
   }

   public void Finish()
   {
      SceneManager.LoadScene("StartMenu");
   }
}
