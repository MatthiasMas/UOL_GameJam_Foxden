using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InventoryHandler inventory;

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

    void Update()
    {
        if (this.inventory.hasWeapon() && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void TakeDamage()
    {
        transform.DetachChildren();
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HitShield(string shield)
    {
        if (shield == "ShieldLeft")
        {
            this.inventory.setLeftShield(false);
            shieldLeftEquipped.SetActive(false);
        }
        if (shield == "ShieldRight")
        {
            this.inventory.setRightShield(false);
            shieldRightEquipped.SetActive(false);
        }
    }

    private void Attack()
    {

    }

    public bool CollectDebris(string part)
    {
        if (part == "Core")
        {
            if (!this.inventory.hasCore())
            {
                this.inventory.setCore(true);
                return true;
            }
        }
        if (part == "Shield")
        {
            if (!this.inventory.hasLeftShield())
            {
                this.inventory.setLeftShield(true);
                shieldLeftEquipped.SetActive(true);
                return true;
            }
            if (!this.inventory.hasRightShield())
            {
                this.inventory.setRightShield(true);
                shieldRightEquipped.SetActive(true);
                return true;
            }
        }
        if (part == "Weapon")
        {
            if (!this.inventory.hasWeapon())
            {
                this.inventory.setWeapon(true);
                return true;
            }
        }
        if (part == "SpeedModule")
        {
            if (!this.inventory.hasSpeedModule())
            {
                this.inventory.setSpeedModule(true);
                return true;
            }
        }
        if (part == "Drive")
        {
            if (!this.inventory.hasDrive())
            {
                this.inventory.setDrive(true);
                return true;
            }
        }
        return false;
    }

    public InventoryHandler getInventory()
    {
        return this.inventory;
    }
}
