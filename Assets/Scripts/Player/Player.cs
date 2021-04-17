using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool shieldLeft;
    public bool shieldRight;
    public bool core;
    public bool weapon;

    public void TakeDamage()
    {
        if (shieldLeft)
        {
            //TODO: SOUND FOR HITTING SHIELD
            shieldLeft = false;
            return;
        }
        if (shieldRight)
        {
            //TODO: SOUND FOR HITTING SHIELD
            shieldRight = false;
            return;
        }
        transform.DetachChildren();
        
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool CollectDebris(string part)
    {
        if (part == "Core")
        {
            if (!core)
            {
                core = true;
                return true;
            }
        }
        if (part == "Shield")
        {
            if (!shieldLeft)
            {
                shieldLeft = true;
                return true;
            }
            if (!shieldRight)
            {
                shieldRight = true;
                return true;
            }
        }
        if (part == "Weapon")
        {
            if (!weapon)
            {
                weapon = true;
                return true;
            }
        }
        return false;
    }
}
