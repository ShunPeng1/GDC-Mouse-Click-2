using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI carrotScore;
    
    public GameObject mainPanel, gameOverPanel;
    
    
    public void OnGameOver()
    {
        mainPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void UpdateCarrotScore(int score)
    {
        carrotScore.text = score.ToString();
    }
}
