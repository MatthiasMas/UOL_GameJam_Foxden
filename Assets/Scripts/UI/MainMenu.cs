using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer masterMixer;

    private void Start()
    {
        GameObject.Find("HighscoresMenu").SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        FindObjectOfType<GameManager>().PlaySound("button", GameManager.MixerGroup.SFX);
        Application.Quit();
    }

    public void SetMasterAudioLevel(System.Single audioLevel)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(audioLevel) * 20);
    }

    public void SetMusicAudioLevel(System.Single audioLevel)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(audioLevel) * 20);
    }

    public void SetSFXAudioLevel(System.Single audioLevel)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(audioLevel) * 20);
        FindObjectOfType<GameManager>().PlaySound("blaster2", GameManager.MixerGroup.SFX);
    }

    public void ButtonPressed()
    {
        FindObjectOfType<GameManager>().PlaySound("button", GameManager.MixerGroup.SFX);
    }
}
