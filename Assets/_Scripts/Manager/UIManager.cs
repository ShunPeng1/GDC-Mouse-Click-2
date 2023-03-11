using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    static public UIManager instance;

    public GameObject bnGameOver;

    public TextMeshProUGUI text;
    int score;

    private void Awake()
    {
        UIManager.instance = this;
        this.bnGameOver = GameObject.Find("bnGameOver");
        this.bnGameOver.SetActive(false);
        //UIManager.instance.bnGameOver.SetActive(true); (thêm vào khi thua)
    }
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int carrotValue)
    {
        score += carrotValue;
        text.text = "X" + score.ToString();
    }
}
