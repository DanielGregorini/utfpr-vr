using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashlightController : MonoBehaviour
{
    public Light flashlight;

    void Awake()
    {
        // = GetComponent<Light>();
        flashlight.enabled = true; 
    }

    void Update()
    {
        // Quando apertar a tecla F, alterna o estado da lanterna
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
