using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text counterText;
    public float minutes, sec;
    void start(){
        counterText=GetComponent<Text>() as Text;
    }
    void update(){
        minutes= (int)(Time.time/60f);
        sec=(int)(Time.time%60f);
        counterText.text=minutes.ToString("00")+":"+ sec.ToString("00");
    }
}
