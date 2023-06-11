using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class canvasScript : MonoBehaviour
{
    
    private gameplayManagerScript myGameplayManagerScript;

    public bool needsToSpawn = false;

    public int highScore;

    public int speed = 10;

    public TMP_Text playPauseBTNText;
    public string playPauseBTNString;

    public TMP_Text scoreLBLText;
    public int scoreLBLTextInt = 0;

    public TMP_Text highScoreLBLText;

    public TMP_Text levelLBLText;
    public int levelLBLTextInt = 1;
    public int numOfClears = 0;

    public Sprite sprite1; 
    public Sprite sprite2;
    public Image muteBTN; 
    private bool isSprite1Active = true;
    
    public Sprite BBBSprite;
    public Sprite BRBSprite;
    public Sprite BGBSprite;
    public Image nextBTN;

    private GameObject boombox;  
    private AudioSource audioPlayer;

    private void Update()
    {
        scoreLBLText.text = scoreLBLTextInt.ToString(); 
        highScoreLBLText.text = highScore.ToString();
        if (numOfClears == 4){
            numOfClears = 0;
            levelLBLTextInt += 1;
            levelLBLText.text = levelLBLTextInt.ToString();
            audioPlayer.pitch += 0.03f;
            speed += 5;
        }
        if (scoreLBLTextInt > highScore)
        {
            highScore = scoreLBLTextInt;
            highScoreLBLText.text = highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        switch (myGameplayManagerScript.next)
        {
            case 1://blue
                nextBTN.sprite = BBBSprite;
                break;
            case 2://green
                nextBTN.sprite = BGBSprite;
                break;
            case 3://red
                nextBTN.sprite = BRBSprite;
                break;
            default:
                Debug.LogError("Invalid random number generated!");
                break;
        }
    }

    private void Start()
    {
        GameObject gameplayManagerObject = GameObject.Find("gameplayManager");
        myGameplayManagerScript = gameplayManagerObject.GetComponent<gameplayManagerScript>();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreLBLText.text = highScore.ToString();

        boombox = GameObject.Find("BGMusicPlayer");
        audioPlayer = boombox.GetComponent<AudioSource>();

        playPauseBTNString = playPauseBTNText.text;

        nextBTN.enabled = false;
    }

    public void playPauseBTN()
    {
        if (playPauseBTNText.text == "PLAY")
        {
            playPauseBTNText.text = "PAUSE";
            needsToSpawn = true;
            nextBTN.enabled = true;
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

    public void onResetBTNPressed()
    {
        myGameplayManagerScript.next = Random.Range(1, 4);

        playPauseBTNText.text = "PLAY";
        playPauseBTNString = playPauseBTNText.text;
        nextBTN.enabled = false;
        speed = 10;

        scoreLBLTextInt = 0;

        levelLBLTextInt = 1;
        levelLBLText.text = levelLBLTextInt.ToString();

        numOfClears = 0;

        audioPlayer.pitch = 1.0f;

        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objects)
        {
            if (obj.name.Contains("Block"))
            {
                Destroy(obj);
            }
        }

        Transform blocks = GameObject.Find("BLOCKS").transform;

        if (blocks != null)
        {
            foreach (Transform child in blocks)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            Debug.Log("Blocks object not found!");
        }
    }

}