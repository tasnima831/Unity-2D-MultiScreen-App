using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AppManager : MonoBehaviour
{
    [Header("Screen 1 Elements")]
    public TMP_InputField nameInputField;

    [Header("Screen 2 Elements")]
    public TMP_Text counterText;

    [Header("Screen 3 Elements")]
    public TMP_Text congratsText;

    private int currentCount = 0;

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "WelcomeScene")
        {
            if (PlayerPrefs.HasKey("Username"))
            {
                SceneManager.LoadScene("CounterScene");
            }
        }
        else if (currentScene == "CounterScene")
        {
            currentCount = PlayerPrefs.GetInt("CounterValue", 0);
            UpdateCounterUI();
        }
        else if (currentScene == "CongratsScene")
        {
            string savedName = PlayerPrefs.GetString("Username", "User");
            congratsText.text = "Congratulations, " + savedName + "! You reached 10!";
        }
    }

    public void OnContinueButtonPressed()
    {
        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text.Trim()))
        {
            PlayerPrefs.SetString("Username", nameInputField.text.Trim());
            PlayerPrefs.Save();

            SceneManager.LoadScene("CounterScene");
        }
        else
        {
            Debug.LogWarning("Please enter a valid name!");
        }
    }

    public void IncrementCounter()
    {
        currentCount++;
        SaveAndCheckCounter();
    }

    public void DecrementCounter()
    {
        if (currentCount > 0)
        {
            currentCount--;
            SaveAndCheckCounter();
        }
    }

    private void SaveAndCheckCounter()
    {
        PlayerPrefs.SetInt("CounterValue", currentCount);
        PlayerPrefs.Save();

        UpdateCounterUI();

        if (currentCount >= 10)
        {
            SceneManager.LoadScene("CongratsScene");
        }
    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = currentCount.ToString();
        }
    }
}