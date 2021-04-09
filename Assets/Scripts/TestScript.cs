using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    Vector3 pos;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
        FindObjectOfType<GameManager>().PlaySound("Test");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float posX = Mathf.Sin(timer) * 5;
        float posY = Mathf.Cos(timer) * 5;

        pos.x = posX;
        pos.y = posY;

        gameObject.transform.position = pos;
        
    }
}
