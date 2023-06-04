using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class canvasScript : MonoBehaviour
{
    public TMP_Text playPauseBTNText;
    public string playPauseBTNString;

    public TMP_Text scoreLBLText;
    public int scoreLBLTextInt = 0;

    public TMP_Text levelLBLText;
    public int levelLBLTextInt = 1;
    public int numOfClears = 0;

    public Sprite sprite1; 
    public Sprite sprite2;
    public Image muteBTN; 
    private bool isSprite1Active = true;
    private GameObject boombox;  
    private AudioSource audioPlayer;

    private void Update()
    {
        scoreLBLText.text = scoreLBLTextInt.ToString(); 
        if (numOfClears == 4){
            numOfClears = 0;
            levelLBLTextInt += 1;
            levelLBLText.text = levelLBLTextInt.ToString();
        }
    }

    private void Start()
    {
        boombox = GameObject.Find("BGMusicPlayer");
        audioPlayer = boombox.GetComponent<AudioSource>();
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

    
    public void onMuteBTNPressed()
    {
        isSprite1Active = !isSprite1Active;

        muteBTN.sprite = isSprite1Active ? sprite1 : sprite2;
        audioPlayer.volume = isSprite1Active ? 1.0f : 0.0f;
    }

}