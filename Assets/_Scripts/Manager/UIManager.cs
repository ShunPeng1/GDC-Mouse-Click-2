using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    
    public GameObject bnGameOver;

    public TextMeshProUGUI text;
    int score;

    private void Start()
    {
        this.bnGameOver = GameObject.Find("bnGameOver");
        this.bnGameOver.SetActive(false);
        //UIManager.instance.bnGameOver.SetActive(true); (thêm vào khi thua)
    }
    
    

    public void ChangeScore(int carrotValue)
    {
        text.text = "X" + carrotValue.ToString();
    }
}
