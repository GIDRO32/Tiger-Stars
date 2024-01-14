using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SiriusFlash : MonoBehaviour
{
    private LineRenderer lr;
    public Button SiriusButton;
    public GameObject[] StarOrder; // Array storing the points in the correct order
    public float displayTime = 5.0f; // Time in seconds for how long the line is displayed
    private Coroutine timerCoroutine; // Coroutine for handling the timer
    public bool canUseSiriusFlash = true; // Bool to control if Sirius Flash can be used

    void Start()
    {
        SiriusButton.enabled = true;
        lr = GetComponent<LineRenderer>();
        lr.enabled = false; // Initially, the line renderer is disabled
        canUseSiriusFlash = true; // Initially, Sirius Flash can be used
    }

    public void OnButtonPress()
    {
        if (!canUseSiriusFlash) return; // Check if Sirius Flash can be used

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine); // Stop any existing timer coroutine
        }
        DrawLine();
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    public void OnButtonRelease()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine); // Stop the timer
        }
        HideLine();
        canUseSiriusFlash = false; // Disable Sirius Flash after use
    }

    private void DrawLine()
    {
        lr.positionCount = StarOrder.Length;
        for (int i = 0; i < StarOrder.Length; i++)
        {
            lr.SetPosition(i, StarOrder[i].transform.position);
        }

        lr.enabled = true; // Enable the line renderer to make the line visible
    }

    private void HideLine()
    {
        lr.enabled = false; // Disable the line renderer to hide the line
    }

    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(displayTime);
        HideLine(); // Hide the line after the timer expires
        canUseSiriusFlash = false; // Disable Sirius Flash after time runs out
        SiriusButton.enabled = false;
    }
}
