using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
   public GameObject quitOption;
   public GameObject optionsOption;
   public GameObject playOption;

   public AudioSource chosen;

   public float selected = 1f;

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.DownArrow) && selected < 3f){
         selected += 1f;
         chosen.Play();
      }

      if (Input.GetKeyDown(KeyCode.UpArrow) && selected > 1f){
         selected -= 1f;
         chosen.Play();
      }

      if (selected == 1f){
         playOption.SetActive(true);
      } else {
         playOption.SetActive(false);
      }
      if (selected == 2f){
         optionsOption.SetActive(true);
      } else {
         optionsOption.SetActive(false);
      }
      if (selected == 3f){
         quitOption.SetActive(true);
      } else {
         quitOption.SetActive(false);
      }

      if (Input.GetKeyDown(KeyCode.Return)){
         switch (selected){
            case 1f:
               Debug.Log("Play Selected");
            break;
            case 2f:
               Debug.Log("Options Selected");
            break;
            case 3f:
               Debug.Log("Quit Selected");
            break;
         }
      }

   }
}
