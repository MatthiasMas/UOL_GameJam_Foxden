using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InventoryHandler inventory;
    [SerializeField]
    private Transform firePoint;

    private GameObject shieldLeftEquipped;
    private GameObject shieldRightEquipped;

    [SerializeField]
    private float attackCooldown = 1f;
    [SerializeField]
    private float projectileSpeed = 7f;
    private float attackTimer = 0f;
    [SerializeField]
    private int maxWeaponUses = 3;
    private int weaponUses;

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
        attackTimer -= Time.deltaTime;
        if (this.inventory.hasWeapon() && Input.GetMouseButtonDown(0) && attackTimer <= 0f)
        {
            attackTimer = attackCooldown;
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
        Vector3 direction = new Vector3(
            transform.position.x - firePoint.position.x,
            transform.position.y - firePoint.position.y,
            transform.position.z - firePoint.position.z);

        Vector3 directionVector = direction.normalized;
        float rotation_z = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;

        GameObject projectilePrefab = FindObjectOfType<GameManager>().GetPrefab("Projectile");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0f, 0f, rotation_z));
        projectile.GetComponent<Projectile>().StartProjectile(gameObject, -directionVector, projectileSpeed);
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
                weaponUses = maxWeaponUses;
                return true;
            }
            else
            {
                if (weaponUses != maxWeaponUses)
                {
                    weaponUses = maxWeaponUses;
                    return true;
                }
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
