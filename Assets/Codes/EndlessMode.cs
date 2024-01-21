using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndlessMode : MonoBehaviour
{
    public List<GameObject> AllPatterns; // List of all pattern parent GameObjects
    [SerializeField] List<GameObject> CurrentPattern = new List<GameObject>(); // The current active pattern
    public List<Transform> PlayerPattern = new List<Transform>(); // The pattern the player is building
    private int nextStarIndex = 0; // Index to track the next expected star in the order
    public AudioSource Sound_Effects;
    public AudioClip Connect;
    public AudioClip Wrong;
    public AudioClip Test;
    public AudioClip OpenSound;
    public AudioClip CloseSound;
    public AudioClip LoseSound;
    public GameObject Interface;
    public GameObject GameOverScreen;
    public GameObject PausePanel;
    public GameObject HomePanel;
    public Transform lastPoints;
    private LineRenderer lr;
    public float timeLeft = 60.0f; // Starting time
    private bool isGameOver = false;
    public Slider timerSlider;
    public Text timeText;
    public Text coinCounter;
    private int coins = 0;
    private int balance;

    void Start()
    {
        balance = PlayerPrefs.GetInt("Total", balance);
        lr = GetComponent<LineRenderer>();
        LoadRandomPattern();
        GameOverScreen.SetActive(false);
        PausePanel.SetActive(false);
        HomePanel.SetActive(false);
    }

    void LoadRandomPattern()
    {
        timeLeft += 5f;
        lr.enabled = false;
        // Deactivate all patterns
        foreach (var pattern in AllPatterns)
        {
            pattern.SetActive(false);
        }

        // Choose a random pattern
        GameObject chosenPattern = AllPatterns[Random.Range(0, AllPatterns.Count)];
        chosenPattern.SetActive(true);

        // Clear previous patterns
        CurrentPattern.Clear();

        // Populate the CurrentPattern list with children of the chosen pattern
        foreach (Transform child in chosenPattern.transform)
        {
            CurrentPattern.Add(child.gameObject);
        }

        // Reset for new pattern
        PlayerPattern.Clear();
        nextStarIndex = 0;
        if (CurrentPattern.Count > 0)
        {
            lastPoints = CurrentPattern[0].transform;
        }
    }

    void Update()
    {
        coinCounter.text = coins.ToString();
        timeText.text = timeLeft.ToString("F2");
        if (!Interface.activeSelf)
        {
        return; // Ignore clicks if any of the conditions are met
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                AddStarToPlayerPattern(hit.collider.transform);
                Debug.Log(balance+coins);
            }
        }
        if (IsPatternCompleted())
        {
            LoadRandomPattern();
        }
        if (!isGameOver)
        {
            // Update timer
            timeLeft -= Time.deltaTime;
            timerSlider.value = timeLeft/60f;

            // Check for game over
            if (timeLeft <= 0)
            {
                Sound_Effects.PlayOneShot(LoseSound);
                balance += coins;
                PlayerPrefs.SetInt("Total", balance);
                Debug.Log(balance);
                GameOver();
            }

            // Rest of the update logic...
        }
    }
    private void SetupLine()
    {
        int pointLength = PlayerPattern.Count;
        lr.positionCount = pointLength;
        for (int i = 0; i < pointLength; i++)
        {
            lr.SetPosition(i, PlayerPattern[i].position);
        }
    }

    public void AddStarToPlayerPattern(Transform star)
    {
        if (star == CurrentPattern[nextStarIndex].transform)
        {
            if(lastPoints == null)
            {
                lastPoints = star;
                PlayerPattern.Add(lastPoints);
            }
            else
            {
                Sound_Effects.PlayOneShot(Connect);
                coins++;
                PlayerPattern.Add(star);
                lr.enabled = true;
                SetupLine();
            }
            nextStarIndex++;

            if (IsPatternCompleted())
            {
                LoadRandomPattern();
            }
        }
        else
        {
            timeLeft -= 1f;
            Sound_Effects.PlayOneShot(Wrong);
        }
    }
    public void OpenPanel(GameObject openable)
    {
        Sound_Effects.PlayOneShot(OpenSound);
        Interface.SetActive(false);
        openable.SetActive(true);
    }
    public void ClosePanel(GameObject closable)
    {
        Sound_Effects.PlayOneShot(CloseSound);
        Interface.SetActive(true);
        closable.SetActive(false);
    }
    public void Retry()
    {
        SceneManager.LoadScene("Infinite");
    }
    public void Quit()
    {
        balance += coins;
        PlayerPrefs.SetInt("Total", balance);
        SceneManager.LoadScene("LoadingMenu");
    }
    public void SoundTest()
    {
        Sound_Effects.PlayOneShot(Test);
    }
    void GameOver()
    {
        isGameOver = true;
        GameOverScreen.SetActive(true);
        Interface.SetActive(false);
    }

    bool IsPatternCompleted()
    {
        return nextStarIndex >= CurrentPattern.Count;
    }
}
