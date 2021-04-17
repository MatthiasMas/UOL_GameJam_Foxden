using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform playerPosition;
    private Player player;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private FieldOfView fov;

    [HideInInspector]
    public bool canAttack = false;
    [SerializeField]
    private float attackRate = 1f;
    [SerializeField]
    private float chargeTime1 = 0.45f;
    [SerializeField]
    private float chargeTime2 = 0.05f;
    

    private float attackTimer = 0f;
    [SerializeField]
    private float projectileSpeed = 1f;

    private Vector3 directionVector;
    private float rotation_z;

    private Vector3 origin = new Vector3(0.35f, -0.20f, 0);


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            return;
        }
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition == null)
        {
            return;
        }

        attackTimer -= Time.deltaTime;
        if (canAttack && attackTimer <= 0f)
        {
            Charge();
        }
    }

    void Charge()
    {
        attackTimer = attackRate;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        GameObject projectilePrefab = FindObjectOfType<GameManager>().GetPrefab("Projectile");
        //FindObjectOfType<GameManager>().PlaySound("TestSFX", GameManager.MixerGroup.SFX);
        yield return new WaitForSeconds(chargeTime1);
        //TODO: SOUND CHARGE ATTACK
        if (playerPosition == null)
        {
            yield break;
        }
        Vector3 playerPos = playerPosition.position;
        yield return new WaitForSeconds(chargeTime2);

        if (!canAttack)
        {
            yield break;
        }

        Vector3 direction = new Vector3(
            playerPos.x - firePoint.position.x,
            playerPos.y - firePoint.position.y,
            playerPos.z - firePoint.position.z);

        directionVector = direction.normalized;
        rotation_z = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0f, 0f, rotation_z));
        projectile.GetComponent<Projectile>().StartProjectile(directionVector, projectileSpeed);

        //if (canAttack)
        //{
        //    Debug.DrawRay(origin + transform.position, playerPos - transform.position, Color.green, shootDistance);

        //    RaycastHit hit;
        //    if (Physics.Raycast(origin + transform.position, playerPos - transform.position, out hit, shootDistance))
        //    {
        //        print(hit.collider.gameObject.name);
        //    }
        //}
    }

}
