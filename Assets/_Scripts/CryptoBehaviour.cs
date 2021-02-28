using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN,
    JUMP
}

public class CryptoBehaviour : MonoBehaviour
{
    [Header("Line Of Sight")]
    public bool HasLos;
    /*public Vector3 playerLocation;*/

    public GameObject player;

    private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLos)
        {
            agent.SetDestination(player.transform.position);
            

            if (HasLos && Vector3.Distance(transform.position, player.transform.position) < 2.5)
            {
                //attack
                animator.SetInteger("AnimeState", (int)CryptoState.IDLE);
                transform.LookAt(transform.position - player.transform.forward);

                if (agent.isOnOffMeshLink)
                {
                    animator.SetInteger("AnimeState", (int)CryptoState.RUN);
                }
            }
            else
            {
                animator.SetInteger("AnimeState", (int)CryptoState.RUN);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLos = true;
            player = other.transform.gameObject;
        }
    }
}

