using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int carrotScore;
    [SerializeField] private int lifeScore;


    public void AddPoint(int adding)
    {
        carrotScore += adding;
        UIManager.Instance.UpdateCarrotScore(carrotScore);
    }

    public void AddLife(int adding)
    {
        lifeScore += adding;
    }
}
