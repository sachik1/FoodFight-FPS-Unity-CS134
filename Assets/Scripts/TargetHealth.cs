using UnityEngine;
using System.Collections;

public class TargetHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().Invoke("CheckTargets", 0.05f);
        }
    }

    IEnumerator CheckAfterDestroy()
    {
        yield return null; // wait 1 frame
        FindObjectOfType<GameManager>().CheckTargets();
    }
}