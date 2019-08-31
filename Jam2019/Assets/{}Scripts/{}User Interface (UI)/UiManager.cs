using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour  {
   public GameObject quitOption;
   public GameObject optionsOption;
   public GameObject playOption;

   public AudioSource chosen;

   public float selected = 1f;

   void Update(){
      if (Input.GetMouseButtonDown(0))
         switch(selected){
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

   public void hoverPlay(){
      playOption.SetActive(true);
      chosen.Play();
      selected = 1f;
   }

   public void hoverPlayExit(){
      playOption.SetActive(false);
   }

   public void hoverOptions(){
      selected = 2f;
      optionsOption.SetActive(true);
      chosen.Play();
   }

   public void hoverOptionsExit(){
      quitOption.SetActive(false);
   }

   public void hoverQuit(){
      selected = 3f;
      quitOption.SetActive(true);
      chosen.Play();
   }

   public void hoverQuitExit(){
      quitOption.SetActive(false);
   }

   public void SelectButton(){
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
