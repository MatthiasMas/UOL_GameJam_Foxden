using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonHandler : MonoBehaviour
{
    public void restartGame()
    {
        
        Debug.Log("HAHAHAHAHAHAHAHAH");
        
        SceneManager.LoadScene(1);
    }
}
