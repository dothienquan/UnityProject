using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TypingGameManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text wordDisplay;
    public TMP_InputField inputField;
    public TMP_Text scoreDisplay;
    public TMP_Text timerDisplay;

    [Header("Word List")]
    public TextAsset wordFile;

    [Header("Game Settings")]
    public float gameTime = 60f;

    private List<string> wordList = new List<string>();
    private string currentWord;
    private int score = 0;
    private float timeRemaining;
    private bool gameActive = true;

    void Start()
    {
        LoadWords();
        GetNewWord();

        inputField.onValueChanged.AddListener(CheckInput);

        timeRemaining = gameTime;
        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                EndGame();
            }
        }
    }

    void LoadWords()
    {
        string[] words = wordFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        wordList.AddRange(words);
    }

    void GetNewWord()
    {
        currentWord = wordList[Random.Range(0, wordList.Count)];
        wordDisplay.text = currentWord;
        inputField.text = "";
    }

    void CheckInput(string playerInput)
    {
        if (!gameActive) return;

        if (playerInput.Trim() == currentWord)
        {
            score++;
            UpdateScoreDisplay();
            GetNewWord();
        }

        // Ensure the input field stays focused after Enter is pressed
        inputField.ActivateInputField();
    }

    void UpdateScoreDisplay()
    {
        scoreDisplay.text = "Score: " + score;
    }

    void UpdateTimerDisplay()
    {
        timerDisplay.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    void EndGame()
    {
        gameActive = false;
        wordDisplay.text = "Game Over!";
        inputField.interactable = false;
    }
}
