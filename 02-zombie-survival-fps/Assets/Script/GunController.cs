using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Referências para Tiro")]
    [Tooltip("Arraste aqui o prefab do Bullet (projétil) que vai ser instanciado.")]
    public GameObject bulletPrefab;

    [Tooltip("Arraste aqui o Transform do child 'spawn_bullet' da arma.")]
    public Transform spawnBullet;

    [Header("Configurações de Tiro")]
    [Tooltip("Velocidade inicial aplicada ao projétil.")]
    public float launchForce = 20f;

    [Tooltip("Tempo em segundos antes de a bala ser destruída automaticamente.")]
    public float bulletLifetime = 5f;

    [Header("Referências para Granada")]
    [Tooltip("Arraste aqui o prefab da granada que vai ser instanciada.")]
    public GameObject grenadePrefab;

    [Tooltip("Força de lançamento da granada.")]
    public float grenadeThrowForce = 15f;

    private AudioSource shootAudioSource;

    void Start()
    {
        shootAudioSource = GetComponent<AudioSource>();
        if (shootAudioSource == null)
            Debug.LogWarning("GunController: Nenhum AudioSource encontrado no GameObject da arma.");
    }

    void Update()
    {
        // Atira o projétil ao clicar com Fire1 (mouse)
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // Solta a granada ao pressionar a tecla G
        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowGrenade();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || spawnBullet == null)
        {
            Debug.LogWarning("GunController: bulletPrefab ou spawnBullet não está configurado no Inspector.");
            return;
        }

        GameObject newBullet = Instantiate(
            bulletPrefab,
            spawnBullet.position,
            spawnBullet.rotation
        );

        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = false;

            // Inverte a direção (caso o prefab de bullet esteja “de costas”)
            rb.linearVelocity = spawnBullet.forward * launchForce * -1f;
        }
        else
        {
            Debug.LogWarning("GunController: o prefab de bala não possui Rigidbody.");
        }

        Destroy(newBullet, bulletLifetime);

        if (shootAudioSource != null && shootAudioSource.clip != null)
            shootAudioSource.Play();
    }

    private void ThrowGrenade()
    {
        if (grenadePrefab == null || spawnBullet == null)
        {
            Debug.LogWarning("GunController: grenadePrefab ou spawnBullet não está configurado no Inspector.");
            return;
        }

        // 1) Instancia a granada na posição e rotação de spawnBullet
        GameObject newGrenade = Instantiate(
            grenadePrefab,
            spawnBullet.position,
            spawnBullet.rotation
        );

        // 2) Configura o Rigidbody para lançar para frente
        Rigidbody rb = newGrenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            // Aplica uma força explosiva (ou velocidade instantânea) para frente
            rb.AddForce(spawnBullet.forward * grenadeThrowForce * -1, ForceMode.VelocityChange );
        }
        else
        {
            Debug.LogWarning("GunController: o prefab de granada não possui Rigidbody.");
        }

      
        // (Opcional) -- Se quiser destruir a granada depois de X segundos:
        // Destroy(newGrenade, 10f);
    }

   

}
