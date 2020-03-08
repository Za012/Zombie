using Assets.Objects.ObjectPooler;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : PooledObject
{
    private NavMeshAgent agent = null;
    private Animator m_animator = null;
    public float walkSpeed = 4f;
    private bool isDamagingPlayer;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        agent.speed = walkSpeed;
    }

    private bool IsPlayerWithinDistance(float distance)
    {
        float d = Vector3.Distance(transform.position, Game.PLAYER.transform.position);
        if (d < distance)
            return true;
        return false;
    }
    private void Update()
    {
        float speed = agent.speed;
        if (Game.PLAYER != null)
        {
            if (IsPlayerWithinDistance(9.5f))
            {
                StopCoroutine(IdleWalk());
                agent.SetDestination(Game.PLAYER.transform.position);
                m_animator.SetTrigger("TargetDetected");

                if (IsPlayerWithinDistance(1.5f))
                {
                    if (!isDamagingPlayer)
                        StartCoroutine("DoDamage");
                    agent.speed = 0.1f;
                    m_animator.SetTrigger("Hit");
                }
                else
                {
                    StopCoroutine("DoDamage");
                    isDamagingPlayer = false;
                    agent.speed = walkSpeed;
                }
            }
            else
            {
                StartCoroutine(IdleWalk());
                m_animator.SetTrigger("TargetLost");
            }
        }
    }
    public IEnumerator DoDamage()
    {
        isDamagingPlayer = true;
        yield return new WaitForSeconds(1f);
        Game.PLAYER.Damage(4);
        isDamagingPlayer = false;
    }
    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private IEnumerator IdleWalk()
    {
        agent.speed = walkSpeed / 2;
        agent.SetDestination(RandomNavmeshLocation(20f));
        yield return new WaitForSeconds(5);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullets"))
        {
            Game.POOL.DespawnObject("zombie", this);
            WaveManager.Instance.NotifyDeath();
        }
    }
    public override void OnObjectSpawn()
    {

    }

    public override void OnObjectDespawn()
    {

    }
}
