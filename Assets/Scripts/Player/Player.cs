using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool shieldLeft;
    public bool shieldRight;
    public bool core;
    public bool weapon;

    private GameObject shieldLeftEquipped;
    private GameObject shieldRightEquipped;

    // Start is called before the first frame update
    void Start()
    {
        shieldLeftEquipped = GameObject.Find("ShieldLeft");
        shieldLeftEquipped.SetActive(false);
        shieldRightEquipped = GameObject.Find("ShieldRight");
        shieldRightEquipped.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        transform.DetachChildren();
        Destroy(gameObject);
    }

    public void HitShield(string shield)
    {
        if (shield == "ShieldLeft")
        {
            shieldLeft = false;
            shieldLeftEquipped.SetActive(false);
        }
        if (shield == "ShieldRight")
        {
            shieldRight = false;
            shieldRightEquipped.SetActive(false);
        }
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
                shieldLeftEquipped.SetActive(true);
                return true;
            }
            if (!shieldRight)
            {
                shieldRight = true;
                shieldRightEquipped.SetActive(true);
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
