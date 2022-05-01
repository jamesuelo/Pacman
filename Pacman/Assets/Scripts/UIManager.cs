using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;
    public Text scoreLabel;
    public Text titleLabel;
    private int totalScore;
    void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        totalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.gamePaused)
        {
            
            titleLabel.enabled = true;
        }
        else
        {
            titleLabel.enabled = false;
        }
    }

    public void ScorePoints(int points)
    {
        totalScore += points;
        scoreLabel.text = "Score. " + totalScore;
    }
}
