using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ZombieController : MonoBehaviour
{
    [Header("Referências ao NavMesh")]
    public float moveSpeed = 3.5f;
    public GameObject zombiePrefab;

    private Transform target;
    private NavMeshAgent agent;
    private Animation anim;
    private AudioSource audioSource;

    [Header("Configurações de Vida")]
    public int hitsToDie = 1;
    private bool isDead = false;

    [Header("Configurações de Respawn")]
    public float minSpawnDistance = 5f;
    public float navMeshSampleRadius = 2f;
    public float spawnRange = 10f;

    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();

        if (agent != null)
        {
            agent.speed = moveSpeed;

            if (!agent.isOnNavMesh)
            {
                Debug.LogError("ZombieController: Zumbi não está em cima de um NavMesh.");
                agent.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (target == null || agent == null || !agent.isOnNavMesh || isDead) return;
        agent.SetDestination(target.position);
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet") || isDead) return;

        Destroy(other.gameObject);
        hitsToDie--;

        if (hitsToDie <= 0)
        {
            isDead = true; 

            agent.isStopped = true;

            KillContController controller = Object.FindFirstObjectByType<KillContController>();
            if (controller != null)
            {
                controller.AddKill();
            }

            if (audioSource != null)
            {
                audioSource.Play();
            }

            if (anim != null && anim["back_fall"] != null)
            {
                anim.Play("back_fall");
                Destroy(gameObject, anim["back_fall"].length);
            }
            else
            {
                Debug.LogWarning("ZombieController: Animação 'back_fall' não encontrada.");
                Destroy(gameObject, 1f);
            }
        }
    }
}
