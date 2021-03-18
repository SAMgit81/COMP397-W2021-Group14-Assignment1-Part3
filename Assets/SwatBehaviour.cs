using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwatState
{
    IDLE,
    RUN,
    ROUNDKICK,
    UPPERCUTJAB
}

public class SwatBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
    public bool HasLOS;

    public GameObject player;

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    public float health = 70f;

    [Header("Attack")]
    public float distance;
    public PlayerBehaviour playerBehaviour;

    public HealthBarScreenSpaceController healthBar;

    public int delay = 30;
    public bool stopDealDamage;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
        }

        if (HasLOS && Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            // could be an attack
            animator.SetInteger("AnimeState", (int)SwatState.ROUNDKICK);
            transform.LookAt(transform.position - player.transform.forward);

            if (stopDealDamage == false)
            {
                stopDealDamage = true;
                StartCoroutine(DoKickDamage());
            }

            if (agent.remainingDistance < 2.5f)
            {
                // could be an attack
                animator.SetInteger("AnimeState", (int)SwatState.UPPERCUTJAB);
                transform.LookAt(transform.position - player.transform.forward);

                if (stopDealDamage == false)
                {
                    stopDealDamage = true;
                    StartCoroutine(DoKickDamage());
                }
            }
        }
       
        else if (HasLOS)
        {
            animator.SetInteger("AnimeState", (int)SwatState.RUN);
        }
        else
        {
            animator.SetInteger("AnimeState", (int)SwatState.IDLE);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator DoKickDamage()
    {
        yield return new WaitForSeconds(1);
        healthBar.TakeDamage(5);
        stopDealDamage = false;
    }

}