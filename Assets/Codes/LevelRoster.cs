using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRoster : MonoBehaviour
{
    public AudioSource Sounds;
    public AudioClip Scrolling;
    public GameObject[] levelPages;
    private int currentPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        ShowPage(currentPage);
    }
    void ShowPage(int pageIndex)
    {
        // Виключаємо всі сторінки
        foreach (GameObject panel in levelPages)
        {
            panel.SetActive(false);
        }

        // Перевіряємо, чи індекс не виходить за межі масиву
        if (pageIndex >= 0 && pageIndex < levelPages.Length)
        {
            // Включаємо тільки обрану сторінку
            levelPages[pageIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid page index");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextPage()
    {
        currentPage++;
        Sounds.PlayOneShot(Scrolling);
        if (currentPage >= levelPages.Length)
        {
            currentPage = 0; // Зациклюємо на першу сторінку, якщо досягнуто кінця
        }
        ShowPage(currentPage);
    }
    public void PreviousPage()
    {
        Sounds.PlayOneShot(Scrolling);
        currentPage--;
        if (currentPage < 0)
        {
            currentPage = levelPages.Length - 1; // Переходимо на останню сторінку, якщо досягнуто початку
        }
        ShowPage(currentPage);
    }
}
