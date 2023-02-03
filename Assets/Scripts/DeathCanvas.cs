using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathCanvas : Singleton<DeathCanvas>
{
    [SerializeField] private GameObject _deathGameObject;
    [SerializeField] private float Timer1;
    [SerializeField] private float Timer2;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioClip son;
    [SerializeField] private AudioClip son2;
    [SerializeField] private AudioSource _audioSource;
    public void Death()
    {
        _deathGameObject.SetActive(true);
        FindObjectOfType<AudioManager>().StopAllSounds();
        FindObjectOfType<AudioManager>().PlaySound("PlayerDeath");
        FindObjectOfType<AudioManager>().PlaySound("Menu");

        StartCoroutine(_Death());
    }

    private IEnumerator _Death()
    {
        yield return new WaitForSeconds(Timer1);

        yield return new WaitForSeconds(0.1f);
        text.text = "G";
        _audioSource.PlayOneShot(son);
        yield return new WaitForSeconds(0.1f);
        text.text = "Ga";
        _audioSource.PlayOneShot(son);
        yield return new WaitForSeconds(0.1f);
        text.text = "Gam";
        text.fontStyle = FontStyles.Italic;
        _audioSource.PlayOneShot(son);
        yield return new WaitForSeconds(0.1f);
        text.text = "Game";
        text.fontStyle = FontStyles.Bold;
        _audioSource.PlayOneShot(son);
        yield return new WaitForSeconds(0.5f);
        text.text = "Game O";
        _audioSource.PlayOneShot(son);
        yield return new WaitForSeconds(0.1f);
        text.text = "Game Ov";
        _audioSource.PlayOneShot(son);
        yield return new WaitForSeconds(0.1f);
        text.text = "Game Ove";
        text.fontStyle = FontStyles.Italic;
        _audioSource.PlayOneShot(son);
        yield return new WaitForSeconds(0.1f);
        text.text = "Game Over";
        _audioSource.PlayOneShot(son);
        yield return new WaitForSeconds(0.1f);
        text.fontStyle = FontStyles.Bold;
        text.text = "Game Over";
        _audioSource.PlayOneShot(son);

        yield return new WaitForSeconds(0.8f);
        _audioSource.PlayOneShot(son2);

        yield return new WaitForSeconds(Timer2);
        SceneManager.LoadScene(0);
    }
}
