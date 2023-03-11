using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotCollectible : Collectible
{
    [SerializeField] private int point;
    [SerializeField] private Animation collect;
    [SerializeField] private AudioClip collectSoundEffect;
    public override void OnCollect(GameObject player)
    {
        player.GetComponent<PlayerStat>().AddPoint(point);
        SoundManager.Instance.PlaySoundEffect(collectSoundEffect);
        Destroy(gameObject);
    }
    
}
