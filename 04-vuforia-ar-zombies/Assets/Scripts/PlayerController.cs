using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    private Animation anim;
    private Joystick joystick;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animation>();
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody>();

        // Garante que o Collider seja trigger
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        Vector3 mov = new Vector3(x, 0, y);
        rb.linearVelocity = mov * 1f;

        if (x != 0 || y != 0)
        {
            anim.Play("Walk");
        }
        else
        {
            anim.Play("Wait");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entrou em: {other.name} (tag={other.tag})");

        if (other.CompareTag("Zombie"))
        {
            // 1) Player faz ataque
            anim.Play("Attack");

            // 2) Zombie faz animação de queda e depois some
            var enemyAnim = other.GetComponent<Animation>();
            if (enemyAnim != null && enemyAnim["back_fall"] != null)
            {
                float fallLength = enemyAnim["back_fall"].length;
                enemyAnim.Play("back_fall");
                StartCoroutine(DestroyAfterFall(other.gameObject, fallLength));
            }
            else
            {
                // fallback caso não tenha animação
                Destroy(other.gameObject, 2f);
                Debug.Log($"Zombie destruído (fallback): {other.name}");
            }
        }
    }

    private IEnumerator DestroyAfterFall(GameObject zombie, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log($"Zombie destruído: {zombie.name}");
        Destroy(zombie);
    }
}
