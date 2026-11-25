using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    private static int placar = 0; 
    [SerializeField] private Text placarText;  // pode atribuir via Inspector ou será buscado em Awake

    private void Awake()
    {
        // Caso não tenha sido atribuído pelo Inspector, faz Find por “score”
        if (placarText == null)
        {
            var scoreGO = GameObject.Find("score");
            if (scoreGO != null)
                placarText = scoreGO.GetComponent<Text>();
            else
                Debug.LogError("GameObject 'score' não encontrado na cena!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        var hitTarget = collision.gameObject;

        // Instancia o prefab “iceCream” dentro de Resources
        var ice = Instantiate(Resources.Load<GameObject>("iceCream"));
        if (ice != null)
        {
            float randomX = Random.Range(-15f, 15f);
            float randomZ = Random.Range(-15f, 15f);

            ice.transform.position = new Vector3(
                hitTarget.transform.position.x + randomX,
                1f,
                hitTarget.transform.position.z + randomZ
            );
            ice.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
        else
        {
            Debug.LogError("Não foi possível instanciar o prefab 'iceCream'. Verifique Resources/iceCream.");
        }

        // Atualiza o placar
        placar++;
        if (placarText != null){
            placarText.text = $"Pontos: {placar}";
        }else {
            Debug.LogError("Text component 'placarText' não encontrado!");
        }
            

        Destroy(hitTarget);
        Destroy(gameObject);
    }
}
