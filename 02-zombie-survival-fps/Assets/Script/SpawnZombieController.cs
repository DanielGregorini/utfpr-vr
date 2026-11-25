using System.Collections;
using UnityEngine;

public class SpawnZombieController : MonoBehaviour
{
    [Header("Referências")]
    [Tooltip("Arraste aqui o prefab do Zombie que será instanciado.")]
    public GameObject zombiePrefab;

    [Header("Configurações de Spawn")]
    [Tooltip("Tempo, em segundos, entre cada spawn de zombie.")]
    public float spawnInterval = 1f;

    private void Start()
    {
        if (zombiePrefab == null)
        {
            Debug.LogWarning("SpawnZombieController: zombiePrefab não está configurado no Inspector.");
            return;
        }

        // Inicia a coroutine que vai spawnar zumbis repetidamente
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        
        while (true)
        {
            
            Instantiate(zombiePrefab, transform.position, transform.rotation);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
