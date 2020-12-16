
using UnityEngine;


[System.Serializable]
public class Health : MonoBehaviour
{

    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }


    void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


}
