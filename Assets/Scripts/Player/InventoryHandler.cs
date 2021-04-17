using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject coreGameObject;
    [SerializeField]
    private GameObject leftShieldGameObject;
    [SerializeField]
    private GameObject rightShieldGameObject;
    [SerializeField]
    private GameObject speedModuleGameObject;
    [SerializeField]
    private GameObject weaponGameObject;
    [SerializeField]
    private GameObject driveGameObject;

    private bool core = false;
    private bool leftShield = false;
    private bool rightShield = false;
    private bool speedModule = false;
    private bool weapon = false;
    private bool drive = false;

    public void FixedUpdate()
    {
        this.coreGameObject.SetActive(this.core);
        this.leftShieldGameObject.SetActive(this.leftShield);
        this.rightShieldGameObject.SetActive(this.rightShield);
        this.speedModuleGameObject.SetActive(this.speedModule);
        this.weaponGameObject.SetActive(this.weapon);
        this.driveGameObject.SetActive(this.drive);
    }

    public int count()
    {
        int count = 0;

        if (this.core)
        {
            count += 1;
        }
        
        if (this.leftShield)
        {
            count += 1;
        }
        
        if (this.rightShield)
        {
            count += 1;
        }
        
        if (this.speedModule)
        {
            count += 1;
        }
        
        if (this.weapon)
        {
            count += 1;
        }
        
        if (this.drive)
        {
            count += 1;
        }
        
        return count;
    }

    public bool isFull()
    {
        return this.core && this.leftShield && this.rightShield && this.speedModule && this.weapon && this.drive;
    }
    
    public void setCore(bool core)
    {
        this.core = core;
    }
    
    public bool hasCore()
    {
        return this.core;
    }
    
    public void setLeftShield(bool shield)
    {
        this.leftShield = shield;
    }
    
    public bool hasLeftShield()
    {
        return this.leftShield;
    }
    
    public void setRightShield(bool shield)
    {
        this.rightShield = shield;
    }
    
    public bool hasRightShield()
    {
        return this.rightShield;
    }
    
    public void setSpeedModule(bool module)
    {
        this.speedModule = module;
    }
    
    public bool hasSpeedModule()
    {
        return this.speedModule;
    }
    
    public void setWeapon(bool weapon)
    {
        this.weapon = weapon;
    }
    
    public bool hasWeapon()
    {
        return this.weapon;
    }
    
    public void setDrive(bool drive)
    {
        this.drive = drive;
    }

    public bool hasDrive()
    {
        return this.drive;
    }
}
