using System.Collections;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    [Header("Configurações da Explosão")]
    [Tooltip("Tempo (em segundos) até a granada explodir.")]
    public float timeToExplode = 3f;

    [Tooltip("Novo raio do Collider após a explosão.")]
    public float explodedRadius = 15f;

    [Header("Referências de Áudio")]
    [Tooltip("O AudioClip de explosão (arraste pelo Inspector).")]
    public AudioClip explosionClip;

    private AudioSource audioSource;
    private Light explosionLight;
    private Collider grenadeCollider;

    void Start()
    {
        // 1) Configura o AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("GrenadeController: não havia AudioSource. Um foi adicionado automaticamente.");
        }
        audioSource.clip = explosionClip;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Se quiser som 3D

        // 2) Configura o Collider existente (SphereCollider, BoxCollider ou CapsuleCollider)
        grenadeCollider = GetComponent<Collider>();
        if (grenadeCollider == null)
        {
            Debug.LogWarning("GrenadeController: não encontrou Collider na granada.");
        }

        // 3) Cria um componente Light para o “flash” vermelho da explosão
        explosionLight = gameObject.AddComponent<Light>();
        explosionLight.color = Color.red;
        explosionLight.intensity = 0f;
        explosionLight.range = explodedRadius;
        explosionLight.enabled = false;

        // 4) Agenda o som para tocar 1 segundo antes da explosão
        float playAudioDelay = Mathf.Max(0f, timeToExplode - 1f);
        Invoke(nameof(PlayExplosionAudio), playAudioDelay);

        // 5) Agenda a explosão completa após timeToExplode segundos
        Invoke(nameof(Explode), timeToExplode);
    }

    private void PlayExplosionAudio()
    {
        if (audioSource != null && explosionClip != null)
        {
            audioSource.Play();
        }
    }

    private void Explode()
    {
        // 1) Aumenta o Collider para o raio especificado
        if (grenadeCollider is SphereCollider sc)
        {
            sc.radius = explodedRadius;
        }
        else if (grenadeCollider is BoxCollider bc)
        {
            bc.size = new Vector3(explodedRadius * 2f, explodedRadius * 2f, explodedRadius * 2f);
        }
        else if (grenadeCollider is CapsuleCollider cc)
        {
            cc.radius = explodedRadius;
            cc.height = explodedRadius * 2f;
        }
        else if (grenadeCollider != null)
        {
            Debug.LogWarning("GrenadeController: tipo de Collider não suportado para redimensionar automaticamente.");
        }

        // 2) Liga o Light vermelho para o “flash”
        explosionLight.enabled = true;
        explosionLight.intensity = 5f;

        // 3) Torna o modelo da granada invisível, se houver MeshRenderer
        var mr = GetComponent<MeshRenderer>();
        if (mr != null)
            mr.enabled = false;

        // 4) Destrói a granada depois de alguns segundos
        Destroy(gameObject, 4f);
    }
}
