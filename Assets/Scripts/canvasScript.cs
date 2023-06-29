using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class canvasScript : MonoBehaviour
{
    // References to input fields for keys
    public TMP_InputField forwardTMP;
    public TMP_InputField backwardTMP;
    public TMP_InputField leftTMP;
    public TMP_InputField rightTMP;
    public TMP_InputField rotateXTMP;
    public TMP_InputField rotateZTMP;

    private GameObject blocks; // Reference to the parent object of blocks

    private gameplayManagerScript myGameplayManagerScript; // Reference to the gameplay manager script

    public bool needsToSpawn = false; // Flag to determine if a block needs to be spawned

    public int highScore; // High score

    public int speed = 10; // Speed of the game


    public Image PlayPauseBTN; // Reference to Play/Pause button image
    public Sprite PPSprite1; // Sprite for PLAY button
    public Sprite PPSprite2; // Sprite for PAUSE button
    public TMP_Text playPauseBTNText; // Text for the play/pause button
    public string playPauseBTNString = "PLAY"; // String value of the play/pause button text
    private bool isPPSprite1Active = true; //Flag if PPSprite1 is selected

    public TMP_Text GOHighScore; // Game over high score text
    public TMP_Text GOScore; // Game over score text
    public TMP_Text GOLevel; // Game over level text

    private bool newGame = true; // Flag to indicate if it's a new game

    public GameObject gameOverPanel; // Game over panel

    public GameObject settingsPanel; // Settings panel

    public TMP_Text scoreLBLText; // Text for the current score
    public int scoreLBLTextInt = 0; // Integer value of the current score

    public TMP_Text highScoreLBLText; // Text for the high score

    public TMP_Text levelLBLText; // Text for the current level
    public int levelLBLTextInt = 1; // Integer value of the current level
    public int numOfClears = 0; // Number of cleared layers

    public Sprite sprite1; // Sprite 1 for the mute button
    public Sprite sprite2; // Sprite 2 for the mute button
    public Image muteBTN; // Mute button image
    private bool isSprite1Active = true; // Flag to determine the active sprite

    public Sprite BBBSprite; // Sprite for the blue block
    public Sprite BRBSprite; // Sprite for the red block
    public Sprite BGBSprite; // Sprite for the green block
    public Image nextBTN; // Next block image

    private GameObject boombox; // Reference to the boombox object
    private AudioSource audioPlayer; // Audio player component

    private void Update()
    {
        if (!gameOverPanel.activeSelf)
        {
            // Update the game over panel with the latest scores and level
            GOHighScore.text = highScoreLBLText.text;
            GOScore.text = scoreLBLText.text;
            GOLevel.text = levelLBLText.text;
        }

        // Update the score and high score text
        scoreLBLText.text = scoreLBLTextInt.ToString();
        highScoreLBLText.text = highScore.ToString();

        // Check if a level has been cleared
        if (numOfClears == 4)
        {
            numOfClears = 0;
            levelLBLTextInt += 1;
            levelLBLText.text = levelLBLTextInt.ToString();
            audioPlayer.pitch += 0.03f;
            speed += 5;
        }

        // Update the high score if the current score exceeds it
        if (scoreLBLTextInt > highScore)
        {
            highScore = scoreLBLTextInt;
            highScoreLBLText.text = highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // Update the next block image based on the next block type
        switch (myGameplayManagerScript.next)
        {
            case 1: // Blue block
                nextBTN.sprite = BBBSprite;
                break;
            case 2: // Green block
                nextBTN.sprite = BGBSprite;
                break;
            case 3: // Red block
                nextBTN.sprite = BRBSprite;
                break;
            default:
                Debug.LogError("Invalid random number generated!");
                break;
        }

        // Check if any block has reached the top
        foreach (Transform block in blocks.transform)
        {
            if (block.position.y >= 75.0f)
            {
                // Game over condition
                gameOverPanel.SetActive(true);
                onResetBTNPressed();
            }
        }

        // Check if any of the key inputs have been changed
        // and save the new input values in PlayerPrefs
        if (PlayerPrefs.GetString("Forward", "W") != forwardTMP.text)
        {
            PlayerPrefs.SetString("Forward", forwardTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("Backward", "S") != backwardTMP.text)
        {
            PlayerPrefs.SetString("Backward", backwardTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("Left", "A") != leftTMP.text)
        {
            PlayerPrefs.SetString("Left", leftTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("Right", "D") != rightTMP.text)
        {
            PlayerPrefs.SetString("Right", rightTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("RotateX", "R") != rotateXTMP.text)
        {
            PlayerPrefs.SetString("RotateX", rotateXTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("RotateZ", "T") != rotateZTMP.text)
        {
            PlayerPrefs.SetString("RotateZ", rotateZTMP.text.ToUpper());
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        // Find the reference to the blocks object and gameplay manager script
        blocks = GameObject.Find("BLOCKS");
        GameObject gameplayManagerObject = GameObject.Find("gameplayManager");
        myGameplayManagerScript = gameplayManagerObject.GetComponent<gameplayManagerScript>();

        // Load the high score from PlayerPrefs and update the high score text
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreLBLText.text = highScore.ToString();

        // Find the reference to the boombox object and audio player component
        boombox = GameObject.Find("BGMusicPlayer");
        audioPlayer = boombox.GetComponent<AudioSource>();

        // Set the initial state of the play/pause button and next block image
        playPauseBTNText.text = "PLAY";
        nextBTN.enabled = false;

        // Deactivate the game over panel and settings panel
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);

        // Load the key input values from PlayerPrefs and update the input fields
        if(PlayerPrefs.GetString("Forward", "W") != ""){
            forwardTMP.text = PlayerPrefs.GetString("Forward", "W");
        }
        if(PlayerPrefs.GetString("Backward", "S") != ""){
            backwardTMP.text = PlayerPrefs.GetString("Backward", "S");
        }
        if(PlayerPrefs.GetString("Left", "A") != ""){
            leftTMP.text = PlayerPrefs.GetString("Left", "A");
        }
        if(PlayerPrefs.GetString("Right", "D") != ""){
            rightTMP.text = PlayerPrefs.GetString("Right", "D");
        }
        if(PlayerPrefs.GetString("RotateX", "R") != ""){
            rotateXTMP.text = PlayerPrefs.GetString("RotateX", "R");
        }
        if(PlayerPrefs.GetString("RotateZ", "T") != ""){
            rotateZTMP.text = PlayerPrefs.GetString("RotateZ", "T");
        }
        
        
        
        
        
    }

    // Play/Pause button action
    public void playPauseBTN()
    {
        isPPSprite1Active = !isPPSprite1Active;
        PlayPauseBTN.sprite = isPPSprite1Active ? PPSprite1 : PPSprite2;

        if (playPauseBTNText.text == "PLAY")
        {
            // Start the game if it's a new game
            playPauseBTNText.text = "PAUSE";
            if (newGame)
            {
                needsToSpawn = true;
                newGame = false;
            }
            nextBTN.enabled = true;
        }
        else
        {
            // Pause the game
            playPauseBTNText.text = "PLAY";
        }
        playPauseBTNString = playPauseBTNText.text;
    }

    // Mute button action
    public void onMuteBTNPressed()
    {
        // Toggle between sprite 1 and sprite 2 and adjust the audio volume
        isSprite1Active = !isSprite1Active;
        muteBTN.sprite = isSprite1Active ? sprite1 : sprite2;
        audioPlayer.volume = isSprite1Active ? 1.0f : 0.0f;
    }

    // Reset button action
    public void onResetBTNPressed()
    {
        PlayPauseBTN.sprite = PPSprite1;
        isPPSprite1Active = true;
        // Reset the game state and destroy all blocks
        newGame = true;
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

    // Back button action for the game over panel
    public void onBackBTNPressed()
    {
        gameOverPanel.SetActive(false);
    }

    // Back button action for the settings panel
    public void onSettingsBackBTNPressed()
    {
        settingsPanel.SetActive(false);
    }

    // Settings open button action
    public void onSettingsOpenBTNPressed()
    {
        PlayPauseBTN.sprite = PPSprite1;
        isPPSprite1Active = true;
        // Open the settings panel
        playPauseBTNText.text = "PLAY";
        playPauseBTNString = playPauseBTNText.text;
        settingsPanel.SetActive(true);
    }

    public void onquitBTNPressed(){
        Application.Quit();
    }
}