using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Configurações de movimento")]
    public float speed = 5f;
    public float gravity = 9.81f;
    public float jumpHeight = 1.5f;

    [Header("Configurações de câmera / mira")]
    public Vector2 mouseSensitivity = new Vector2(350f, 350f);
    public Vector2 verticalLookLimits = new Vector2(-60f, 60f);

    [Header("Sistema de Vida")]
    [Tooltip("Vida máxima do jogador.")]
    public int maxHealth = 100;
    [Tooltip("Dano que o zumbi causa ao colidir.")]
    public int zombieDamage = 20;

    private TMP_Text healthText;

    [Header("Áudio do Player")]
    [Tooltip("Som que toca quando o jogador aperta G.")]
    public AudioClip throwClip;
    [Tooltip("Som que toca quando o jogador morre.")]
    public AudioClip deathClip;

    private CharacterController controller;
    private Transform cameraTransform;
    private AudioSource audioSource;

    private Quaternion cameraRotationOriginal;
    private Vector3 cameraPositionOriginal;

    // Variáveis de rotação
    private float rotationX = 0f;
    private float rotationY = 0f;

    private float verticalVelocity = 0f;
    private int currentHealth;

    // Controle de intervalo entre danos
    private bool canTakeDamage = true;
    private float damageCooldown = 1f; // 1 segundo de intervalo

    void Start()
    {
        controller = GetComponent<CharacterController>();

        cameraTransform = GetComponentInChildren<Camera>()?.transform;
        if (cameraTransform == null)
        {
            Debug.LogError("Não encontrou uma Camera como filho do Player!");
            enabled = false;
            return;
        }

        cameraRotationOriginal = cameraTransform.localRotation;
        cameraPositionOriginal = cameraTransform.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotationY = transform.eulerAngles.y;
        float pitchAtual = cameraTransform.localEulerAngles.x;
        if (pitchAtual > 180f) pitchAtual -= 360f;
        rotationX = pitchAtual;

        // Configura o AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Procura o TextMeshPro no objeto chamado "vida"
        GameObject vidaObj = GameObject.Find("vida");
        if (vidaObj != null)
        {
            healthText = vidaObj.GetComponent<TMP_Text>();
            if (healthText == null)
                Debug.LogWarning("Encontrou objeto 'vida', mas ele não tem TMP_Text.");
        }
        else
        {
            Debug.LogWarning("Não encontrou nenhum GameObject com nome 'vida'.");
        }

        // Inicializa vida
        currentHealth = maxHealth;
        UpdateHealthText();

        // Parenting da arma (se existir)
        GameObject gunObj = GameObject.FindGameObjectWithTag("Gun");
        if (gunObj != null)
        {
            gunObj.transform.SetParent(cameraTransform);
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com Tag 'Gun' foi encontrado na cena!");
        }
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();

        // Tocar som de "throw" ao apertar G
        if (Input.GetKeyDown(KeyCode.G) && throwClip != null)
        {
            audioSource.PlayOneShot(throwClip);
        }
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity.y * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, verticalLookLimits.x, verticalLookLimits.y);

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        Quaternion rotPitch = Quaternion.Euler(rotationX, 0f, 0f);
        cameraTransform.localRotation = cameraRotationOriginal * rotPitch;
        cameraTransform.localPosition = cameraPositionOriginal;
    }

    private void HandleMovement()
    {
        if (controller.isGrounded)
        {
            if (jumpHeight > 0f && Input.GetButtonDown("Jump"))
                verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);
            else
                verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX * -1 + transform.forward * moveZ * -1;
        move.Normalize();

        Vector3 horizontalVelocity = move * speed;
        horizontalVelocity.y = verticalVelocity;

        controller.Move(horizontalVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Zombie")) return;
        if (!canTakeDamage) return;

        StartCoroutine(ApplyDamageWithCooldown());
    }

    private IEnumerator ApplyDamageWithCooldown()
    {
        canTakeDamage = false;
        audioSource.PlayOneShot(deathClip);

        // Aplica o dano
        currentHealth -= zombieDamage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (deathClip != null)
            {
                audioSource.PlayOneShot(deathClip);
                float delay = deathClip.length;
                Invoke(nameof(ReturnToSceneZero), delay);
            }
            else
            {
                ReturnToSceneZero();
            }

            controller.enabled = false;
            enabled = false;
        }

        // Aguarda cooldown antes de permitir novo dano
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    private void ReturnToSceneZero()
    {
        SceneManager.LoadScene(0);
    }
}
