using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
	public Transform Pivot;
	public static int CurrentChar;//static variable to pass character to next scene
	// Use this for initialization
	void Start () {
		CurrentChar = 0;//oubeid
		Pivot.transform.eulerAngles = new Vector3 (0, 0, 0);
	}
	public void nextChar()
	{
		CurrentChar += 1;//younes
		if (CurrentChar == 3)
			CurrentChar = 0;
	}
	public void StartGame()
	{
		if(CurrentChar == 0)
		SceneManager.LoadScene (0);
		if(CurrentChar == 1)
        SceneManager.LoadScene(1);
        if (CurrentChar == 2)
        SceneManager.LoadScene(2);

    }
    public void prevChar()
	{
		CurrentChar -= 1;
		if (CurrentChar == -1)
			CurrentChar = 2;//omar
	}
	// Update is called once per frame
	void Update () {

		Pivot.transform.eulerAngles = Vector3.MoveTowards(Pivot.transform.eulerAngles,new Vector3 (0,CurrentChar*120 , 0),Time.deltaTime*360);

	}
}
