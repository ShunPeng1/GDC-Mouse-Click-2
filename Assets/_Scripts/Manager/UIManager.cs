using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI text;
    int score;
  
    public void ChangeScore(int carrotValue)
    {
        text.text = "X" + carrotValue.ToString();
    }
}
