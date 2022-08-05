using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;

    private Blade blade;
    private Spawner spawner;

    private int score;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        scoreText.text = score.ToString();
    }    
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    
    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;
    }
}
