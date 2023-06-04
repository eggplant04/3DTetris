using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class canvasScript : MonoBehaviour
{
    public TMP_Text playPauseBTNText;
    public string playPauseBTNString;

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