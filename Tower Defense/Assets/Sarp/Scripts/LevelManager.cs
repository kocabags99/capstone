﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;

    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }
    
    private void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;
    }

    private void ReduceLives(EnemyS enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
        }
    }
    
    private void WaveCompleted()
    {
        CurrentWave++;
        //AchievementManager.Instance.AddProgress("Waves10", 1);
        //AchievementManager.Instance.AddProgress("Waves20", 1);
        //AchievementManager.Instance.AddProgress("Waves50", 1);
        //AchievementManager.Instance.AddProgress("Waves100", 1);
    }
    
    private void OnEnable()
    {
        EnemyS.OnEndReached += ReduceLives;
        Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        EnemyS.OnEndReached -= ReduceLives;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }
}
