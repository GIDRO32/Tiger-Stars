using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public AudioSource Sounds;
    public AudioClip Test;
    public AudioClip Open;
    public AudioClip Close;
    public GameObject MainMenu;
    public GameObject[] MenuArray;
    public GameObject[] CompleteMarks;
    private int isBeaten;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        MenuArray[0].SetActive(false);
        MenuArray[1].SetActive(false);
        MenuArray[2].SetActive(false);
        MenuArray[3].SetActive(false);
    }
    public void OpenPanel(int openable)
    {
        Sounds.PlayOneShot(Open);
        MainMenu.SetActive(false);
        MenuArray[openable].SetActive(true);
    }
    public void ClosePanel(int closable)
    {
        Sounds.PlayOneShot(Close);
        MainMenu.SetActive(true);
        MenuArray[closable].SetActive(false);
    }
    public void SoundTest()
    {
        Sounds.PlayOneShot(Test);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i < CompleteMarks.Length; i++)
        {
            isBeaten = PlayerPrefs.GetInt("Level" + i.ToString(),isBeaten);
            if(isBeaten == 1)
            {
                CompleteMarks[i].SetActive(true);
            }
            else
            {
                CompleteMarks[i].SetActive(false);
            }
        }
    }
}
