using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : EnemyMovement
{
    public static GameObject gate;
    public HealthBar healthBar;
    //public int gateHP;

    bool reachedGate = false;
    bool startedAttacking = false;

    private void Awake()
    {
        //reachedGate = false;
        //startedAttacking = false;
        //nextWaypoint = 0;
    }

    private void Start()
    {
        //reachedGate = false;
        //startedAttacking = false;
        //nextWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print("Waypoint: " + nextWaypoint + "/" + waypointsArray.Length);
        if (nextWaypoint == waypointsArray.Length)
            reachedGate = true;

        if (reachedGate && !startedAttacking)
        {
            StartCoroutine(damageGate(100));
            startedAttacking = true;
        }

        if (healthBar.getHealth() <= 0)
        {
            Destroy(gate);
            animator.Play("battlecry");
        }
    }

    IEnumerator damageGate(int damage)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        healthBar.decreaseHealth(damage);

        yield return new WaitForSecondsRealtime(0.4f);

        startedAttacking = false;

        StopAllCoroutines();
    }
}
