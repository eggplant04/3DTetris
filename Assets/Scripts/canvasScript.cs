using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class canvasScript : MonoBehaviour
{
    public TMP_Text playPauseBTNText;
    public string playPauseBTNString;

    public TMP_Text scoreLBLText;
    public int scoreLBLTextInt = 0;

    public TMP_Text levelLBLText;
    public int levelLBLTextInt = 1;
    public int numOfClears = 0;

    private void Update()
    {
        scoreLBLText.text = scoreLBLTextInt.ToString(); 
        if (numOfClears == 3){
            numOfClears = 0;
            levelLBLTextInt += 1;
            levelLBLText.text = levelLBLTextInt.ToString();
        }
    }

    private void Start()
    {
        
        playPauseBTNString = playPauseBTNText.text;
    }

    public void playPauseBTN()
    {
        if (playPauseBTNText.text == "PLAY")
        {
            playPauseBTNText.text = "PAUSE";
        }
        else
        {
            playPauseBTNText.text = "PLAY";
        }
        playPauseBTNString = playPauseBTNText.text;
    }
}