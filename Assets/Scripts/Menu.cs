using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private int sceneToLoadIndex;
    [SerializeField]
    private Animator transitionAnimator;
    private void Awake()
    {
        FindObjectOfType<AudioManager>().PlaySound("Menu");
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }
    public void _Start()
    {
        StartCoroutine(transitionEnd());
    }
    public void Quit()
    {
        Application.Quit();
    }
    private IEnumerator transitionEnd()
    {
        transitionAnimator.Play("EndOfTheScene");
        yield return new WaitForSeconds(5f);
        FindObjectOfType<AudioManager>().StopSound("Menu");
        SceneManager.LoadScene(sceneToLoadIndex);
        FindObjectOfType<AudioManager>().PlaySound("Ambiance1");
        FindObjectOfType<AudioManager>().PlaySound("InWater");
        FindObjectOfType<AudioManager>().PauseSound("InWater");
    }
}
