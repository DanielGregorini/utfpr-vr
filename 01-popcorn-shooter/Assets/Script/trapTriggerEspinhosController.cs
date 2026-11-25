using UnityEngine;

public class trapTriggerEspinhosController : MonoBehaviour
{
    // Referência para o prefab de espinho
    public GameObject espinhoPrefab;
    // Velocidade dos espinhos
    public float velocity = 100f;
    // Referência para o som do trap
    public AudioSource trapSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Aqui você pode adicionar lógica extra se necessário, mas no caso, tudo ocorre ao colidir com o player
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproduz o som do trap (se existir)
            if (trapSound != null)
            {
                trapSound.Play();
            }

            // Gera espinhos em posições aleatórias ao redor do trap
            for (int i = 0; i < 10; i++)
            {
                // Gera posições aleatórias para o espinho ao redor do trap, mas com Y fixo para evitar que ele suba ou desça
                float randomX = UnityEngine.Random.Range(-5f, 5f);
                float randomZ = UnityEngine.Random.Range(-5f, 5f);

                // Instancia o espinho na posição especificada
                GameObject espinho = Instantiate(espinhoPrefab, new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ), Quaternion.identity);

                // Obtém o Rigidbody do espinho e configura a gravidade como falsa
                Rigidbody rb = espinho.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;  // Desativa a gravidade para que o espinho não caia

                    // Direções aleatórias para a força, evitando o movimento para baixo
                    Vector3 randomDirection = new Vector3(
                        UnityEngine.Random.Range(-1f, 1f),  // Direção no eixo X
                        0f,                               // Não queremos movimento no eixo Y
                        UnityEngine.Random.Range(-1f, 1f)   // Direção no eixo Z
                    ).normalized;  // Normaliza para garantir direção unitária

                    // Aplica uma grande força ao espinho para fazê-lo voar
                    rb.AddForce(randomDirection * velocity, ForceMode.Impulse);
                }

                // Destroi o espinho após 5 segundos
                Destroy(espinho, 5.0f);
            }
        }
    }
}
