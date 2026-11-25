using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerCamera;

    [SerializeField] private Text coinText;

    [SerializeField] private GameObject vitoriaMenu;

    private bool isDead = false;
    private Rigidbody rb;
    private Collider col;
    private float halfHeight;
    private float xRotation = 0f;
    private float verticalVelocity = 0f;

    [SerializeField]
    private int coins = 0;

    [SerializeField]
    private int maxCoins = 5;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        halfHeight = col.bounds.extents.y;



        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        if (coinText != null)
        {
            UpdateCoinText();
        }
        else
        {
            Debug.LogWarning("Coin Text não está atribuído!");
        }

        if (vitoriaMenu != null)
            vitoriaMenu.SetActive(false);
    }

    void Update()
    {
        if (!isDead)
            HandleLook();
    }

    void FixedUpdate()
    {
        if (!isDead)
            HandleMovement();
    }

    private void HandleLook()
    {
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mx);
        xRotation = Mathf.Clamp(xRotation - my, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, halfHeight + 0.1f);

        if (isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        verticalVelocity -= gravity * Time.fixedDeltaTime;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * moveX + transform.forward * moveZ) * walkSpeed;

        if (Input.GetButtonDown("Jump") && isGrounded)
            verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);

        rb.linearVelocity = new Vector3(move.x, verticalVelocity, move.z);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou no trigger de: " + other.gameObject.name);

        if (other.CompareTag("Monster"))
        {
            dead();
            Debug.Log("O monstro te pegou!");
        }

        if (other.CompareTag("Coin"))
        {
            addCoin();
            Destroy(other.gameObject);
        }
    }

    public void addCoin()
    {
        if (coins < maxCoins)
        {
            coins++;
            Debug.Log("Moedas coletadas: " + coins);
            UpdateCoinText();
            if (coins >= maxCoins)
            {
                if (vitoriaMenu != null)
                {
                    vitoriaMenu.SetActive(true);
                }

                StartCoroutine(ReturnToMenuAfterDelay());
            }
        }
        else
        {
            Debug.Log("Máximo de moedas alcançado!");
        }
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
            coinText.text = $"Moeda: {coins}/{maxCoins}";
    }

    void dead()
    {
        Debug.Log("Você morreu!");
        isDead = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(ReturnToMenuAfterDelay());
    }

    private IEnumerator ReturnToMenuAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Menu inicial
    }

    // 🔊 Função para o monstro chamar e aumentar o volume
    public void SetHighVolume()
    {
        AudioListener.volume = 2f;
        Debug.Log("Volume aumentado para 2x");
    }

    // 🔉 Função para o monstro chamar e restaurar o volume
    public void SetNormalVolume()
    {
        AudioListener.volume = 1f;
        Debug.Log("Volume restaurado para 1x");
    }
}
