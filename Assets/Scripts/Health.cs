
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

[System.Serializable]
public class Health : MonoBehaviour
{

    public float health = 100f;
    public bool isPlayer = false;
    public Slider healthBar;
    public GameObject playerFollowCam;
    private void Start()
    {
        if (isPlayer) 
        {
            healthBar.maxValue = health;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }


    void Update()
    {
        //update the player's health bar
        if (isPlayer) 
        {
            healthBar.value = health;
        }
        if (health <= 0f)
        {
            Die();
        }
    }

    //checks for if bullet enters the player's capsule collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" && isPlayer) 
        {
            //take damage by 10
            TakeDamage(10);
        }
    }

    //kill or destroy the gameobject
    void Die()
    {
        //enables the game camera if the player dies   
        if (isPlayer)
        {
            playerFollowCam.GetComponent<Camera>().enabled = true;
            playerFollowCam.GetComponent<SmoothFollow>().target = null;
        }
        Destroy(gameObject);
    }


}
