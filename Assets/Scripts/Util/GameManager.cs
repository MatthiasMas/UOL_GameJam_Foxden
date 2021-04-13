using UnityEngine.Audio;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum MixerGroup {Master, Music, SFX};
    public AudioMixerGroup masterAudioMixerGroup;
    public AudioMixerGroup musicAudioMixerGroup;
    public AudioMixerGroup sfxAudioMixerGroup;
    public Sound[] sounds;
    public GameObject[] prefabs;
    public Sprite[] sprites;

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // this is to make sure that the gamemanager exists in all scenes
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        // add theme here
    }

    public void PlaySound(string name, MixerGroup group)
    /* This method is called to play a sound
     * Sounds can be added to the sounds array in the inspector
     * 
     * example usage: 
     * FindObjectOfType<GameManager>().PlaySound("ExampleSound");
     */

    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound with name " + name + " can not be found!");
            return;
        }

        switch (group)
        {
            case MixerGroup.Master:
                {
                    s.source.outputAudioMixerGroup = masterAudioMixerGroup;
                    break;
                }
            case MixerGroup.Music:
                {
                    s.source.outputAudioMixerGroup = musicAudioMixerGroup;
                    break;
                }
            case MixerGroup.SFX:
                {
                    s.source.outputAudioMixerGroup = sfxAudioMixerGroup;
                    break;
                }
            default:
                {
                    Debug.Log("No audio mixer with that name found, using default one");
                    s.source.outputAudioMixerGroup = masterAudioMixerGroup;
                    break;
                }
        }

        if (!s.source.isPlaying)
        {

            s.source.Play();
        }
    }

    public GameObject GetPrefab(string name)
    /* This method is called to load a prefab
    * Prefabs can be added to the prefabs array in the inspector
    * 
    * example usage: 
    * FindObjectOfType<GameManager>().GetPrefab("ExamplePrefab");
    */
    {
        GameObject p = Array.Find(prefabs, prefab => prefab.name == name);
        if (p == null)
        {
            Debug.Log("prefab with name " + name + " can not be found!");
        }
        return p;
    }

    public Sprite GetSprite(string name)
    /* This method is called to load a sprite
    * Sprites can be added to the sprites array in the inspector
    * 
    * example usage: 
    * FindObjectOfType<GameManager>().GetSprite("ExampleSprite");
    */
    {
        Sprite p = Array.Find(sprites, sprite => sprite.name == name);
        if (p == null)
        {
            Debug.Log("sprite with name " + name + " can not be found!");
        }
        return p;
    }
}
