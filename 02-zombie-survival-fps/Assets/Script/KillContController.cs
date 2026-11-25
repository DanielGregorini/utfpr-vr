using UnityEngine;
using TMPro;

public class KillContController : MonoBehaviour
{
    public int totalKills = 0;


    private TMP_Text killText;

    void Start()
    {
        totalKills = 0;

        // Pega automaticamente o TMP_Text no mesmo GameObject
        killText = GetComponent<TMP_Text>();

        if (killText == null)
        {
            Debug.LogError("KillContController nao encontrado");
            return;
        }

        UpdateKillText();
    }

    public void AddKill()
    {
        totalKills++;
        UpdateKillText();
    }

    private void UpdateKillText()
    {
        if (killText != null)
        {
            killText.text = "Zumbis mortos: " + totalKills;
        }
    }
}
