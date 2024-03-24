using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BulidButtons : MonoBehaviour
{
    public int Seconds = 10;
    public int type = 1;
    public Text homeTimer;
    public Sprite buildImage;
    void Start() {
        TimeSpan alfa = new TimeSpan(0, 0, 0, Seconds);
        homeTimer.text = alfa.Minutes.ToString("00") + ":" + alfa.Seconds.ToString("00");
    }
    void OnMouseUpAsButton()
    {
        Timer.seconds = Seconds;
        Timer.typeB = type;
        Timer.isItemSelected = true;
        Timer.buildImage = buildImage;
    }
}

