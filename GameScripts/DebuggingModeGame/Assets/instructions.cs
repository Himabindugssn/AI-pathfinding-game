using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instructions : MonoBehaviour
{
    public GameObject panel;
    int counter;
    public void showhidePanel(){
        counter ++;
        if(counter%2==1){
            panel.gameObject.SetActive(false);
        }
        else{
            panel.gameObject.SetActive(true);
        }
    }
}
