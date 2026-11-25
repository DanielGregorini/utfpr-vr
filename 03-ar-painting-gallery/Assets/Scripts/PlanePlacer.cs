using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlanePlacer : MonoBehaviour
{
    [SerializeField] private XROrigin origin;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject[] paints;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        // Habilita detecção de planos horizontais e verticais (paredes)
        planeManager.requestedDetectionMode =
            PlaneDetectionMode.Horizontal | PlaneDetectionMode.Vertical;
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        // Faz o raycast contra qualquer plano detectado (chão ou parede)
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            // Instancia a planta alinhada à rotação do plano (frente "grudada" na parede)
            GameObject plant = Instantiate(
                paints[Random.Range(0, paints.Length)],
                hitPose.position,
                hitPose.rotation
            );

            // Opcional: afasta um pouco da parede para evitar clipping
            // Vector3 offset = hitPose.rotation * Vector3.forward * 0.05f;
            // plant.transform.position += offset;

            // Desliga todos os planos para limpar a cena
            foreach (var plane in planeManager.trackables)
                plane.gameObject.SetActive(false);

            planeManager.enabled = false;
        }
    }
}
