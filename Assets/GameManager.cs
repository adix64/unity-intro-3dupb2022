using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
    void Start()
    {
        if(instance == null) 
		{
			instance = this;
		}
    }
	public void RestartGame(float delay) 
	{
		StartCoroutine(ReloadLevelWithDelay(delay));
	}
	IEnumerator ReloadLevelWithDelay(float delay) 
	{
		yield return new WaitForSeconds(delay);
		ReloadLevel();
	}
	void ReloadLevel() 
	{
		SceneManager.LoadScene("SampleScene");
	}
    
}
