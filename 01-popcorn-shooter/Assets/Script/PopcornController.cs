using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class PopcornController : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    // Referências para os sons de disparo e morte
    public AudioSource fireSound;  // Som do disparo
    public AudioSource deathSound; // Som da morte

    void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * 150 * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * 3 * Time.deltaTime;    

        transform.Rotate(0, horizontal, 0);
        transform.Translate(0, 0, vertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * 6;

        // Reproduz o som do disparo
        if (fireSound != null)
        {
            fireSound.Play();
        }
        else
        {
            Debug.LogError("AudioSource para disparo não encontrado.");
        }

        Destroy(bullet, 4.0f);
    }

    // Detecta colisão com o trap
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Colidiu com: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colidiu com o trap ou inimigo!");
            // Reproduz o som de morte **somente** quando o player colidir com o trap
            if (deathSound != null)
            {
                deathSound.Play();
            }
            else
            {
                Debug.LogError("AudioSource para morte não encontrado.");
            }

            // Espera 0,4 segundos antes de destruir o personagem
            Invoke("DestroyPopcorn", 0.4f);

            // Espera 0,5 segundos antes de destruir toda a cena e voltar ao menu
            Invoke("DestroySceneAndReturnToMenu", 0.5f);
        }
    }

    // Função para destruir o Popcorn após o atraso
    private void DestroyPopcorn()
    {
        Destroy(gameObject); // Destrói o personagem (Popcorn)
    }

    // Função chamada após 0,5 segundos para destruir a cena e retornar ao menu
    private void DestroySceneAndReturnToMenu()
    {
        // Destrói todos os objetos na cena **antes** de carregar o menu
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            Destroy(obj);
        }

        // Agora, carrega a cena do menu
        SceneManager.LoadScene("MenuIniciar");
    }
}
