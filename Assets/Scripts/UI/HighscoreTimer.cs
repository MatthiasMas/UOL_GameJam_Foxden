using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreTimer : MonoBehaviour
{
    private Player playerObject;
    private float highscoreTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        highscoreTimer += Time.deltaTime;
        UpdateTimer();
        if (this.playerObject.getInventory().isFull())
        {
            //@TODO: Add correct Finished scene
            AddHighscoreEntry(highscoreTimer, "AAA");
            FindObjectOfType<GameManager>().PlaySound("victory", GameManager.MixerGroup.SFX);
            SceneManager.LoadScene(2);
        }
    }

    private void UpdateTimer()
    {
        Text text = gameObject.GetComponent<Text>();
        text.text = FormatTime(highscoreTimer);
    }

    public void AddHighscoreEntry(float score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores();
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Save updates Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
