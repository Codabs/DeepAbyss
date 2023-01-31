using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private int sceneToLoadIndex;
    public void _Start()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
