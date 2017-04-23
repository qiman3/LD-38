using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void StartGame()
	{
		SceneManager.LoadScene("_Test_");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
