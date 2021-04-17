using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [HideInInspector]
    public bool canAttack = false;
    [SerializeField]
    private float attackRate = 1f;

    private float attackTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (canAttack && attackTimer <= 0f)
        {
            Charge();
            Attack();
        }
    }

    void Charge()
    {

    }

    void Attack()
    {
        // TODO: ENEMY ATTACK SOUND
        attackTimer = 1 / attackRate;
    }
}
