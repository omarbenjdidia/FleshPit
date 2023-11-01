using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class menu : MonoBehaviour
{
    public GameObject Setting;
    public GameObject gameplay;
    public GameObject graphics;
    public GameObject audio;
    public GameObject select;
    public GameObject mode;
    public GameObject maps;

    public void openselect()
    {
        select.SetActive(true);
    }
    public void closeselect() 
    {
        select.SetActive(false);

    }
    public void openMode()
    {
        mode.SetActive(true);
        select.SetActive(false);
    }
    public void closeMode()
    {
        mode.SetActive(false);
        select.SetActive(true);
    }
    public void SelectRandom()
    {

    }
    public void openMaps()
    {
        mode.SetActive(false);
        maps.SetActive(true);
    }
    public void closeMaps()
    { 
    maps.SetActive(false);
    mode.SetActive(true);
    }
    public void RandomMaps()
    {
        int index = UnityEngine.Random.Range(2, 4);
        SceneManager.LoadScene(index);
    }
    public void loadHeart()
    {
        SceneManager.LoadScene("Heart Finale");
    }
    public void loadBrain()
    {
        SceneManager.LoadScene("Brain Finale");
    }
    public void loadStomac()
    {
        SceneManager.LoadScene("Stomach13");
    }

    public void Back()
    {
        SceneManager.LoadScene("mainmenufiras");
    }
    public void opensetting()
    {
        Setting.SetActive(true);
    }
    public void closesetting() 
    {
        Setting.SetActive(false); 
    }
    public void opengameplay()
    {
        gameplay.SetActive(true);
        Setting.SetActive(false);
    }
    public void closegameplay()
    {
        gameplay.SetActive(false);
        Setting.SetActive(true);
    }
    public void opengraphics()
    {
        graphics.SetActive(true);
        Setting.SetActive(false);
    }
    public void chooseMap()
    {
        SceneManager.LoadScene("selectmap");
    }
    public void closegraphics()
    {
        graphics.SetActive(false);
        Setting.SetActive(true);
    }
    public void openaudio()
    {
        audio.SetActive(true);
        Setting.SetActive(false);
    }
    public void closeaudio()
    {
        audio.SetActive(false);
        Setting.SetActive(true);
    }

    public void exit()
    {
        Application.Quit();
        Debug.Log("exit");
    }
}
