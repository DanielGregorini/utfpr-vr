using UnityEngine;

public class trapBallController : MonoBehaviour
{
    // Referência para o prefab do cubo
    public GameObject cubeTrap;
    // Velocidade da bola
    public float velocity = 10000f;  // Aumentei a velocidade para fazer a bola voar com mais força

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproduz o som do trap (se existir)
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

            for (int i = 0; i < 10; i++)
            {
                // Gera posições aleatórias para o cubo
                float randomX = UnityEngine.Random.Range(-17f, 17f);
                float randomZ = UnityEngine.Random.Range(-17f, 17f);

                // Instancia o cubo na posição especificada
                GameObject cube = Instantiate(cubeTrap, new Vector3(randomX, 1f, randomZ), Quaternion.identity);

                // Obtém o Rigidbody do cubo e configura a gravidade como falsa
                Rigidbody rb = cube.GetComponent<Rigidbody>();  // Obtém o Rigidbody do cubo
                if (rb != null)
                {
                    rb.useGravity = false;  // Desativa a gravidade para que o cubo não caia

                    // Direções aleatórias para a força
                    Vector3 randomDirection = new Vector3(
                        UnityEngine.Random.Range(-1f, 1f), // Direção no eixo X
                        0f,                              // Não queremos movimento no eixo Y
                        UnityEngine.Random.Range(-1f, 1f)  // Direção no eixo Z
                    ).normalized;  // Normaliza para garantir direção unitária

                    // Aplica uma grande força ao cubo para fazê-lo voar
                    rb.AddForce(randomDirection * velocity, ForceMode.Impulse);
                }

                // Destroi o cubo após 5 segundos
                Destroy(cube, 5.0f);
            }
        }
    }
}
