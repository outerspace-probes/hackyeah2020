using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text rescuedAnimalsNum = default;
    [SerializeField] Text gameOverAnimalsNum = default;
    [SerializeField] GameObject gameOverPanel = default;
    [SerializeField] GameObject congratsPanel = default;
    [SerializeField] PlayerController1 player = default;

    int rescuedAnimals = 0;

    // singleton
    public static GameManager instance;
    private void Awake() { if (instance == null) { instance = this; } }

    public void AddAnimal()
    {
        rescuedAnimals++;
        rescuedAnimalsNum.text = rescuedAnimals.ToString();
    }

    public void ProcessGameOver()
    {
        gameOverAnimalsNum.text = "RESCUED ANIMALS: " + rescuedAnimals.ToString();
        gameOverPanel.SetActive(true);
    }

    public void ProcessWin()
    {
        player.isActiveGame = false;
        Invoke("ShowCongrats", 3f);
    }
  
    private void ShowCongrats()
    {
        congratsPanel.SetActive(true);
    }
}
