using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class select : MonoBehaviour
{
    public int currentchapindex;
    public GameObject[] champModels;
    // Start is called before the first frame update
    void Start()
    {
        currentchapindex = PlayerPrefs.GetInt("selectedchamp", 0);  
        foreach (GameObject champ in champModels)
            champ.SetActive(false);
        champModels[currentchapindex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void next()
    {
        champModels[currentchapindex].SetActive(false);
        
        currentchapindex++;
        if(currentchapindex == champModels.Length)
            currentchapindex = 0;

        champModels[currentchapindex].SetActive(true);
        PlayerPrefs.SetInt("selectedchamp", currentchapindex);  
    }
    public void prev()
    {
        champModels[currentchapindex].SetActive(false);

        currentchapindex--;
        if (currentchapindex == -1)
            currentchapindex = champModels.Length -1;

        champModels[currentchapindex].SetActive(true);
        PlayerPrefs.SetInt("selectedchamp", currentchapindex);
    }
    public void start()
    {
    }
}
