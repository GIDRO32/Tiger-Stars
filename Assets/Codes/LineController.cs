using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineController : MonoBehaviour
{
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
    private int isBeaten;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        Interface.SetActive(true);
        ClearPanel.SetActive(false);
        PausePanel.SetActive(false);
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
            Debug.Log("WRONG!");
        }
    }

    private void CheckWinCondition()
    {
        // Check if the player's list is equal to the correct list
        if (nextStarIndex >= StarOrder.Length)
        {
            // Player has connected all points correctly
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                makeLine(hit.collider.transform);
                print(hit.collider.name);
            }
        }
    }
    public void OpenPanel(GameObject openable)
    {
        Interface.SetActive(false);
        openable.SetActive(true);
    }
    public void ClosePanel(GameObject closable)
    {
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
}
