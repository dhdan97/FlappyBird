using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Gives us access to TextMeshPro components

public class Score : MonoBehaviour
{
    public static Score instance; // Our singleton

    public TextMeshProUGUI scoreText;
    int score;
    int highScore;

    // A typical Singleton pattern initalization, always do it in the Awake function
    private void Awake()
    {
        // First we check if there are multiple instances of this gameobject/component, if there is then destory this gameobject
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this; // Then we set our singleton to this instance
        }

        Kill.onKill += onDeath;
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Loads the highscore from the saved key of "HighScore" into our highScore variable
        UpdateUI();
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public int HighScore // Use this to access the highscore int
    {
        get
        {
            return highScore;
        }
    }

    void UpdateUI()
    {
        scoreText.text = score + "";
    }

    void onDeath()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score); // Saves the high score into our "HighScore" key
        }
    }

    private void OnDestroy()
    {
        Kill.onKill -= onDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
