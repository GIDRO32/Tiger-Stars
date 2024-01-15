using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LineController : MonoBehaviour
{
    public Text health_show;
    public Text Connect_Counter;
    public AudioSource Sound_Effects;
    public AudioClip Connect;
    public AudioClip Wrong;
    public AudioClip Test;
    public AudioClip OpenSound;
    public AudioClip CloseSound;
    public AudioClip StageClear;
    public AudioClip LoseSound;
    private LineRenderer lr;
    public List<Transform> points = new List<Transform>();
    public GameObject[] StarOrder; // Array storing the correct order of stars
    public GameObject Interface;
    public GameObject ClearPanel;
    public GameObject GameOverScreen;
    public GameObject PausePanel;
    public GameObject HomePanel;
    private int nextStarIndex = 0; // Index to track the next expected star in the order
    public Transform lastPoints;
    public string LevelTag;
    public int ProgressTag;
    private int health = 3;
    private int isBeaten;
    public int autoConnectUses = 3;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        Interface.SetActive(true);
        GameOverScreen.SetActive(false);
        ClearPanel.SetActive(false);
        PausePanel.SetActive(false);
        HomePanel.SetActive(false);
    }

    private void makeLine(Transform finalPoint)
    {
        if (finalPoint == StarOrder[nextStarIndex].transform)
        {
            if (lastPoints == null)
            {
                lastPoints = finalPoint;
                points.Add(lastPoints);
            }
            else
            {
                points.Add(finalPoint);
                lr.enabled = true;
                SetupLine();
            }
            nextStarIndex++;
            CheckWinCondition(); // Check if the player has won
        }
        else
        {
            Sound_Effects.PlayOneShot(Wrong);
            health--;
            Debug.Log("WRONG!");
        }
    }

    private void CheckWinCondition()
    {
        // Check if the player's list is equal to the correct list
        if (nextStarIndex >= StarOrder.Length)
        {
            Sound_Effects.PlayOneShot(StageClear);
            // Player has connected all points correctly
            Interface.SetActive(false);
            ClearPanel.SetActive(true); // Show the ClearPanel
            isBeaten = 1;
            PlayerPrefs.SetInt("Level" + ProgressTag.ToString(),isBeaten);
        }
    }

    private void SetupLine()
    {
        int pointLength = points.Count;
        lr.positionCount = pointLength;
        for (int i = 0; i < pointLength; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }

    void Update()
    {
        Connect_Counter.text = autoConnectUses.ToString();
        health_show.text = health.ToString();
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
                Sound_Effects.PlayOneShot(Connect);
                makeLine(hit.collider.transform);
                print(hit.collider.name);
            }
        }
        if(health == 0)
        {
            GameOverScreen.SetActive(true);
            Interface.SetActive(false);
            Sound_Effects.PlayOneShot(LoseSound);
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
        SceneManager.LoadScene(LevelTag);
    }
    void OnDisable()
    {
        // Reset the line renderer and other state when the script is disabled
        if (lr != null)
        {
            lr.positionCount = 0;
        }

        points.Clear();
        lastPoints = null;
        nextStarIndex = 0;
    }
    public void SoundTest()
    {
        Sound_Effects.PlayOneShot(Test);
    }
    public void UseAutoConnect()
    {
        if (autoConnectUses > 0 && nextStarIndex < StarOrder.Length)
        {
            Sound_Effects.PlayOneShot(Connect);
            makeLine(StarOrder[nextStarIndex].transform);
            autoConnectUses--; // Decrease the usage counter
        }
    }
}
