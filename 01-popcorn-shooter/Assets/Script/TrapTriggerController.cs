using UnityEngine;

public class TrapTriggerController : MonoBehaviour
{
    public GameObject cubeTrap;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

            for (int i = 0; i < 7; i++){
                float ramdomX = UnityEngine.Random.Range(-17, 17);
                float ramdomZ = UnityEngine.Random.Range(-17, 17);

                GameObject cube = Instantiate(cubeTrap, new Vector3(ramdomX, 15, ramdomZ), Quaternion.identity);

                Destroy(cube, 5.0f);
            }
        }
    }
}
