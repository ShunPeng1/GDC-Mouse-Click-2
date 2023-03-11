using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    static public UIManager instance;

    public GameObject bnGameOver;

    private void Awake()
    {
        UIManager.instance = this;
        this.bnGameOver = GameObject.Find("bnGameOver");
        this.bnGameOver.SetActive(false);
        //UIManager.instance.bnGameOver.SetActive(true); (thêm vào khi thua)
    }
    
}
