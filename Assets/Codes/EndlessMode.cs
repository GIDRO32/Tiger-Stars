using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessMode : MonoBehaviour
{
    public AudioSource Sound_Effects;
    public AudioClip Connect;
    public AudioClip Test;
    public AudioClip OpenSound;
    public AudioClip CloseSound;
    public AudioClip LoseSound;
    public AudioClip Wrong;
    private LineRenderer lr;
    public List<Transform> points = new List<Transform>();
    public List<GameObject[]> StarOrder; // Array storing the correct order of stars
    public GameObject[] PatternParents;
    public GameObject Interface;
    public GameObject GameOverScreen;
    public GameObject PausePanel;
    public GameObject HomePanel;
    private int currentPatternIndex = 0;
    private int nextStarIndex = 0; // Index to track the next expected star in the order
    public Transform lastPoints;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        Interface.SetActive(true);
        GameOverScreen.SetActive(false);
        PausePanel.SetActive(false);
        HomePanel.SetActive(false);
    }

private void makeLine(Transform finalPoint)
{
    // Ensure that the current pattern index is within bounds
    if (currentPatternIndex >= StarOrder.Count) return;

    // Access the current pattern
    GameObject[] currentPattern = StarOrder[currentPatternIndex];

    // Ensure that the next star index is within bounds for the current pattern
    if (nextStarIndex >= currentPattern.Length) return;

    if (finalPoint == currentPattern[nextStarIndex].transform)
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
    }
    else
    {
        Sound_Effects.PlayOneShot(Wrong);
        Debug.Log("WRONG!");
    }

    // Check if the current pattern is complete
    if (nextStarIndex >= currentPattern.Length)
    {
       LoadNewPattern();
    }
}
private bool IsPatternCompleted()
{
    // Get the current pattern
    GameObject[] currentPattern = StarOrder[nextStarIndex];

    // Check if all points in the pattern have been connected
    if (points.Count == currentPattern.Length)
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i] != currentPattern[i].transform)
            {
                return false; // Point mismatch
            }
        }
        return true; // All points match
    }

    return false; // Not all points have been connected yet
}

private void LoadNewPattern()
{
    // Deactivate the current pattern
    if (currentPatternIndex >= 0)
    {
        PatternParents[currentPatternIndex].SetActive(false);
    }

    // Select a random new pattern
    currentPatternIndex = Random.Range(0, PatternParents.Length);
    PatternParents[currentPatternIndex].SetActive(true);

    // Reset relevant variables for the new pattern
    nextStarIndex = 0;
    lastPoints = null;
    points.Clear();
    lr.positionCount = 0; // Clear the LineRenderer
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
}
