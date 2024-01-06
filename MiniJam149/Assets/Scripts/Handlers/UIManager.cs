using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player HUD")]
    [SerializeField] private Transform enemyIcon;
    [SerializeField] private Transform[] availablePositions;
    [SerializeField] private TMP_Text scoreText;

    [Header("Screens")]
    [SerializeField] private Image fadeScreen;
    [SerializeField] private GameObject consoleScreen;
    [SerializeField] private GameObject gameOverScreen;

    public void SetUI(int time, int score)
    {
        scoreText.text = score.ToString();
        enemyIcon.position = availablePositions[time - 1].position;
    }

    public void SetConsoleVisibility(bool visible, Action callback)
    {
        
    }

    public void ShowGameOver()
    {

    }
}
