using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class canvasScript : MonoBehaviour
{
    


    //keys-------------------------------------------------------------------
    public string forward;
    public string backward;
    public string left;
    public string right;
    public string rotateX;
    public string rotateZ;

    public TMP_InputField forwardTMP;
    public TMP_InputField backwardTMP;
    public TMP_InputField leftTMP;
    public TMP_InputField rightTMP;
    public TMP_InputField rotateXTMP;
    public TMP_InputField rotateZTMP;
    //-----------------------------------------------------------------------
    private GameObject blocks;
    

    private gameplayManagerScript myGameplayManagerScript;

    public bool needsToSpawn = false;

    public int highScore;

    public int speed = 10;

    public TMP_Text playPauseBTNText;
    public string playPauseBTNString;

    public TMP_Text GOHighScore;
    public TMP_Text GOScore;
    public TMP_Text GOLevel;


    public GameObject gameOverPanel;

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
        if (!gameOverPanel.activeSelf)
        {
            GOHighScore.text = highScoreLBLText.text;
            GOScore.text = scoreLBLText.text;
            GOLevel.text = levelLBLText.text;
        }
        

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

        foreach (Transform block in blocks.transform){
            if(block.position.y >= 75.0f){
                gameOverPanel.SetActive(true);
                onResetBTNPressed();
            }
        }

        //keys------------------------------------
        if(PlayerPrefs.GetString("Forward", "W") != forwardTMP.text){
            PlayerPrefs.SetString("Forward", forwardTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.GetString("Backward", "S") != backwardTMP.text){
            PlayerPrefs.SetString("Backward", backwardTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.GetString("Left", "A") != leftTMP.text){
            PlayerPrefs.SetString("Left", leftTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.GetString("Right", "D") != rightTMP.text){
            PlayerPrefs.SetString("Right", rightTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.GetString("RotateX", "R") != rotateXTMP.text){
            PlayerPrefs.SetString("RotateX", rotateXTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.GetString("RotateZ", "T") != rotateZTMP.text){
            PlayerPrefs.SetString("RotateZ", rotateZTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        
        //----------------------------------------
    }

    private void Start()
    {

        blocks = GameObject.Find("BLOCKS");
        GameObject gameplayManagerObject = GameObject.Find("gameplayManager");
        myGameplayManagerScript = gameplayManagerObject.GetComponent<gameplayManagerScript>();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreLBLText.text = highScore.ToString();

        boombox = GameObject.Find("BGMusicPlayer");
        audioPlayer = boombox.GetComponent<AudioSource>();

        playPauseBTNString = playPauseBTNText.text;

        nextBTN.enabled = false;
        
        gameOverPanel.SetActive(false);

        forwardTMP.text = PlayerPrefs.GetString("Forward", "W");        
        backwardTMP.text = PlayerPrefs.GetString("Backward", "S");
        leftTMP.text = PlayerPrefs.GetString("Left", "A");
        rightTMP.text = PlayerPrefs.GetString("Right", "D");
        rotateXTMP.text = PlayerPrefs.GetString("RotateX", "R");
        rotateZTMP.text = PlayerPrefs.GetString("RotateY", "T");
        


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

    public void onBackBTNPressed(){
        gameOverPanel.SetActive(false);
    }

}