using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsInGame : Singleton<OptionsInGame>
{
    [SerializeField] private GameObject optionGameObject;
    [SerializeField] private GameObject cinemachineGameObjectToStopInPause;
    private bool optionEnable;
    public void CallOption()
    {
        if (!optionEnable)
        {
            Time.timeScale = 0f;
            optionGameObject.SetActive(true);
            optionEnable = true;
            cinemachineGameObjectToStopInPause.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            optionGameObject.SetActive(false);
            optionEnable = false;
            cinemachineGameObjectToStopInPause.SetActive(true);
        }
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
