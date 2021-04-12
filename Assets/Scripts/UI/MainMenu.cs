using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetAudioLevel(System.Single audioLevel)
    {
        masterMixer.SetFloat("Volume", Mathf.Log10(audioLevel) * 20);
        FindObjectOfType<GameManager>().PlaySound("Test");
    }
}
