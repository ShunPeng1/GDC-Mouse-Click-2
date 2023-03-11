using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int point;
    [SerializeField] private int life;


    public void AddPoint(int adding)
    {
        point += adding;
        UIManager.Instance.ChangeScore(point);
    }

    public void AddLife(int adding)
    {
        life += adding;
    }
}
