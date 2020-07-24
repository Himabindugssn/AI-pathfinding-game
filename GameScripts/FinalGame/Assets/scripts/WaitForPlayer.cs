using UnityEngine;
 using System.Collections;
 
 public class WaitForPlayer : MonoBehaviour
 {
     public GameObject waitScreen;
     public GameObject mainPlayer;
     
     // flag to determine if we are waiting for uset input to start game
     private bool waitingToStartGame = true;
 
     // Use this for initialization
     void Start ()
     {
         if (waitScreen != null)
         {
             waitScreen.SetActive(true);
         }
         else
         {
             waitingToStartGame = false;
             Debug.LogError("waitScreen was not set in the inspector. Please set and try again");
         }
         if (mainPlayer != null)
         {
             mainPlayer.SetActive(false);
         }
         else
         {
             Debug.LogError("mainPlayer was not set in the inspector. Please set and try again");
         }
     }
 
     void Update ()
     {
         // if the waitingToStartGame is enabled and the 'S' key has been pressed
         if (waitingToStartGame && (Input.GetKeyDown(KeyCode.S)))
         {
             // set the flag to false so that will no longer be checking for input to start game
             waitingToStartGame = false;
             if (waitScreen != null)
             {
                 waitScreen.SetActive(false);
             }
             if (mainPlayer != null)
             {
                 mainPlayer.SetActive(true);
             }
         }
     }
 }