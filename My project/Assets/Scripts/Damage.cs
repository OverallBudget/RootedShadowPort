using Unity.Mathematics;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int health = 100; // Player's health  
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the object when health is 0
            return; // Exit the method to avoid further execution
        }

        // Smoothly move the object toward the player's position
        float speed = 18f; // Adjust this value to control the movement speed
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Optional: Add rotation logic if needed
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce health by the damage amount
        gameObject.transform.position += new Vector3(UnityEngine.Random.Range(-2,5), UnityEngine.Random.Range(-1, 3), 0); // Move the player up by 1 unit
        Debug.Log("Enemy took damage: " + damage + ", remaining health: " + health);
    }
}
