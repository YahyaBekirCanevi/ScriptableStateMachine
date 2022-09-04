using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagerService : MonoBehaviour
{
    private static GameManagerService instance;
    public static GameManagerService Instance { get => instance; private set => instance = value; }

    [SerializeField] private Text timeScaleText;
    [SerializeField] private float gravity = -9.8f;
    public float Gravity { get => gravity; }
    private void Awake()
    {
        instance = this;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TimeFluency()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale += .2f;
        }
        else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale -= .2f;
        }
        string timeScale = Mathf.Abs(Time.timeScale).ToString("0.0");
        timeScaleText.text = "x" + (Time.timeScale < 0 ? $"(- {timeScale})" : timeScale);
    }
}