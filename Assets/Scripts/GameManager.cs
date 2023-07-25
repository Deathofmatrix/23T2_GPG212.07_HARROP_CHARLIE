using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HarropCharlie.MusicGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelSO[] levelSO;
        [SerializeField] private GameObject[] levelPanelsGO;
        [SerializeField] private LevelPanel[] levelPanels;
        [SerializeField] private Button[] levelButtons;

        [SerializeField] private string userName;
        [SerializeField] private int maxLevel;
        [SerializeField] private int highScore;

        [SerializeField] private TMP_Text usernameText;
        [SerializeField] private TMP_Text maxLevelText;
        [SerializeField] private TMP_Text highScoreText;

        private void Start()
        {
            for (int i = 0; i < levelSO.Length; i++)
            {
                levelPanelsGO[i].SetActive(true);
            }

            if (PlayerPrefs.HasKey("maxLevel"))
            {
                maxLevel = PlayerPrefs.GetInt("maxLevel");
            }
            else
            {
                maxLevel = 1;
                PlayerPrefs.SetInt("maxLevel", maxLevel);
            }

            highScore = PlayerPrefs.GetInt("highScore");
            usernameText.text = "UserName: " + userName;
            maxLevelText.text = "Max Level: " + maxLevel;
            highScoreText.text = "High Score " + highScore;

            LoadPanels();
        }

        private void LoadPanels()
        {
            for (int i = 0; i < levelSO.Length; i++)
            {
                levelPanels[i].levelText.text = levelSO[i].level.ToString();
                levelPanels[i].descriptionText.text = levelSO[i].description.ToString();
                levelPanels[i].difficultyText.text = levelSO[i].difficulty.ToString();

                if (maxLevel >= levelSO[i].level)
                {
                    levelButtons[i].interactable = true;
                }
                else
                {
                    levelButtons[i].interactable = false;
                }
            }
        }

        public void SelectLevel(int levelNumber)
        {
            SceneManager.LoadScene(levelNumber);
        }
    }
}
