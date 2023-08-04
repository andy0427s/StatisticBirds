using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int requiredNumberCount;

    public List<int> numbers = new List<int>();

    public float targetMean;
    public float targetMedian;
    public float targetMode;
    public TextMeshProUGUI playerNumberList;
    public TextMeshProUGUI playerResult;

    public TextMeshProUGUI finalNumbersText;

    // Game mode setting
    public enum GameMode
    {
        Mean,
        Median,
        Mode
    }

    public GameMode gameMode;

    // UI controller
    public TextMeshProUGUI targetText;
    public GameObject gamePlayPanel;
    public GameObject resultPanel;
    public GameObject successUI;
    // public GameObject failUI;
    // public GameObject tryAgainUI;

    // Level controller
    private static int _nextLevelIndex = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        resultPanel.SetActive(false);
        successUI.SetActive(false);
        // failUI.SetActive(false);
        // tryAgainUI.SetActive(false);
        switch (gameMode)
        {
            case GameMode.Mean:
                targetText.text = "Level Target:\nFind " + requiredNumberCount.ToString() + " numbers with a mean of " + targetMean.ToString();
                break;
            case GameMode.Median:
                targetText.text = "Level Target:\nFind " + requiredNumberCount.ToString() + " numbers with a median of " + targetMedian.ToString();
                break;
            case GameMode.Mode:
                targetText.text = "Level Target:\nFind " + requiredNumberCount.ToString() + " numbers with a mode of " + targetMode.ToString();
                break;
        }
    }


    public void AddNumber(int number)
    {
        numbers.Add(number);
        numbers.Sort();
        playerNumberList.text = "[" + string.Join(", ", numbers) + "]";
        switch (gameMode)
        {
            case GameMode.Mean:
                playerResult.text = numbers.Average().ToString();
                break;
            case GameMode.Median:
                playerResult.text = FindMedian(numbers).ToString();
                break;
            case GameMode.Mode:
                playerResult.text = FindMode(numbers).ToString();
                break;
        }
    }

    public void CheckNumbers()
    {
        if (numbers.Count >= requiredNumberCount)
        {
            ShowResult();
        }
    }

    public void ShowResult()
    {
        // Assign the contents of numbersText to the new text element
        finalNumbersText.text = targetText.text;
        // Hide the numbersText element
        gamePlayPanel.SetActive(false);
        ShowQuestion();
        resultPanel.SetActive(true);
        float playerResult = CalculateStatistics();
        switch (gameMode)
        {
            case GameMode.Mean:
                if (Mathf.Approximately(playerResult, targetMean))
                {
                    successUI.SetActive(true);
                }
                else
                {
                    // failUI.SetActive(true);
                }
                break;
            case GameMode.Median:
                if (Mathf.Approximately(playerResult, targetMedian))
                {
                    successUI.SetActive(true);
                }
                else
                {
                    // failUI.SetActive(true);
                }
                break;
            case GameMode.Mode:
                if (Mathf.Approximately(playerResult, targetMode))
                {
                    successUI.SetActive(true);
                }
                else
                {
                    // failUI.SetActive(true);
                }
                break;
        }
    }

    private void ShowQuestion()
    {
        
    }

    private float CalculateStatistics()
    {
        switch (gameMode)
        {
            case GameMode.Mean:
                return (float)numbers.Average();
            case GameMode.Median:
                return FindMedian(numbers);
            case GameMode.Mode:
                return FindMode(numbers);
            default:
                return 0f;
        }
    }

    private float FindMedian(List<int> numbers)
    {
        numbers.Sort();
        if (numbers.Count % 2 == 0)
            return (numbers[numbers.Count / 2 - 1] + numbers[numbers.Count / 2]) / 2.0f;
        else
            return numbers[numbers.Count / 2];
    }

    private int FindMode(List<int> numbers)
    {
        return numbers.GroupBy(n => n)
                      .OrderByDescending(g => g.Count())
                      .Select(g => g.Key).FirstOrDefault();
    }
    public void LoadNextLevel()
    {
        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
    }
}
