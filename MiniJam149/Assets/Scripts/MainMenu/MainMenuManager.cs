using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Fade dependency")]
    [SerializeField] private Image fadeBackground;

    [ContextMenu("Fade")]
    public void PlayGame()
    {
        AudioManager.instance.Play("MenuButton");
        LoadScene();   
    }

    private void LoadScene()
    {
        //Animation
        DOTween.ToAlpha(() => fadeBackground.color, x => fadeBackground.color = x, 1, 2).onComplete +=
            () =>
            {
                SceneManager.LoadScene("Game");
            };
    }
}

