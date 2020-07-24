using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public GameObject panel;
    int counter;
    public void showhidePanel(){
        counter ++;
        if(counter%2==0){
            panel.gameObject.SetActive(false);
        }
        else{
            panel.gameObject.SetActive(true);
        }
    }
}
