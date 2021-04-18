using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody player;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float movementSpeed = 1f;    
    [SerializeField]
    private float boostSpeed = 10f;
    [SerializeField]
    private float boostLength = 0.2f;
    [SerializeField]
    private float timeBetweenBoosts = 2.5f;
    [SerializeField]
    private FogGeneration fog;

    private Player playerObject;
    private Vector2 lastPlayerPos = new Vector2(0f, 0f);
    private Vector2 deltaOffset = new Vector2(0f, 0f);
    private Vector3 input;
    private float speedMultiplier = 150f;
    private double lastBoost = 0;
    private float highscoreTimer = 0f;


    private void Start()
    {
        this.playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (this.playerObject.getInventory().isFull())
        {
            //@TODO: Add correct Finished scene
            AddHighscoreEntry(highscoreTimer, "AAA");
            SceneManager.LoadScene(2);
        }
        
        this.input.x = Input.GetAxisRaw("Horizontal");
        this.input.y = Input.GetAxisRaw("Vertical");
        this.input.z = 0;
        
        this.animator.SetFloat("Speed", this.player.velocity.sqrMagnitude);

        this.deltaOffset = this.lastPlayerPos - (Vector2)this.transform.position;
        this.lastPlayerPos = this.transform.position;


        if (this.input.sqrMagnitude > 0.01)
        {
            this.rotate(this.input);
        }
    }

    void FixedUpdate()
    {
        highscoreTimer += Time.fixedDeltaTime;

        float speed = this.movementSpeed * this.speedMultiplier;
        double time = Time.timeAsDouble;
        
        if (Input.GetKey("space") && time > (this.lastBoost + this.boostLength + this.timeBetweenBoosts))
        {
            this.lastBoost = time;
            this.animator.SetBool("Boost", true);
        }

        if (time >= this.lastBoost && time <= (this.lastBoost + this.boostLength))
        {
            speed = this.boostSpeed * this.speedMultiplier;
        }
        else
        {
            this.animator.SetBool("Boost", false);
        }

        speed -= 20 * this.playerObject.getInventory().count();

        Vector3 playerForce = this.input * (Time.fixedDeltaTime * speed);
        this.player.AddForce(playerForce);
    }
    
    private void rotate(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q, 100 * Time.fixedDeltaTime);
    }

    public Vector2 getDeltaOffset()
    {
        return this.deltaOffset;
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
}
