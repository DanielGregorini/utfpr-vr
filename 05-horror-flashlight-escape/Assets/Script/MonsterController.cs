using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public Light flashlight;

    [Header("Audio")]
    public AudioSource step;         // 🎵 Som de passos (loop)
    public AudioSource rage_monster; // 🎵 Som de raiva (uma vez)

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private PlayerController playerScript;

    private float checkInterval = 4f;
    private float lostSightTimer = 0f;
    private float timeToForget = 5f;
    private float lastUpdateTime = 0f;

    private Vector3 lastKnownPlayerPosition;
    private bool isChasing = false;
    private bool ragePlayed = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerScript = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player com tag 'Player' não encontrado.");
        }

        // Toca som de passos continuamente
        if (step != null && !step.isPlaying)
        {
            step.loop = true;
            step.Play();
        }

        lastUpdateTime = Time.time - checkInterval;
    }

    void Update()
    {
        if (player == null || playerScript == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool seesPlayer = CanSeePlayer(distanceToPlayer);
        bool flashlightOn = flashlight != null && flashlight.enabled;

        // Volume dinâmico
        if (distanceToPlayer <= 10f)
            playerScript.SetHighVolume();
        else
            playerScript.SetNormalVolume();

        // Detecção do jogador
        if (seesPlayer || (flashlightOn && distanceToPlayer <= 30f) || distanceToPlayer <= 7f)
        {
            if (!isChasing)
            {
                // Toca som de raiva uma vez
                if (rage_monster != null && !rage_monster.isPlaying && !ragePlayed)
                {
                    rage_monster.Play();
                    ragePlayed = true;
                }
            }

            isChasing = true;
            lostSightTimer = 0f;
            lastKnownPlayerPosition = player.position;
            agent.SetDestination(player.position);
            animator.Play("root|Anim_monster_scavenger_walk");
        }
        else if (isChasing)
        {
            lostSightTimer += Time.deltaTime;

            if (lostSightTimer >= timeToForget || distanceToPlayer > 30f)
            {
                isChasing = false;
                ragePlayed = false; // libera som de raiva para próxima vez
                lostSightTimer = 0f;
            }
            else
            {
                agent.SetDestination(player.position);
                animator.Play("root|Anim_monster_scavenger_walk");
            }
        }

        // Patrulha aleatória se não estiver perseguindo
        if (!isChasing)
        {
            if (Time.time - lastUpdateTime >= checkInterval)
            {
                lastKnownPlayerPosition = GetRandomPointNear(player.position, 10f);
                agent.SetDestination(lastKnownPlayerPosition);
                lastUpdateTime = Time.time;
            }

            animator.Play("root|Anim_monster_scavenger_walk");
        }
    }

    bool CanSeePlayer(float distance)
    {
        if (distance > 15f)
            return false;

        Vector3 direction = (player.position - transform.position).normalized;
        Ray ray = new Ray(transform.position + Vector3.up * 1.5f, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, 15f))
        {
            return hit.transform.CompareTag("Player");
        }

        return false;
    }

    Vector3 GetRandomPointNear(Vector3 origin, float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        return new Vector3(origin.x + randomPoint.x, origin.y, origin.z + randomPoint.y);
    }
}
