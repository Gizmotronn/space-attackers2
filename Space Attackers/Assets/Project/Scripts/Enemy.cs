using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Particle effects
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check for collision
    void OnTriggerEnter2D (Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerMissile") {
            GameObject explosionInstance = Instantiate (explosionPrefab);
            explosionInstance.transform.SetParent(transform.parent); // same as this.transfer.parent --> parent of the enemy
            explosionInstance.transform.position = transform.position;

            Destroy (explosionInstance, 1.5f); //1.5f = 1.5 seconds
            Destroy (gameObject);
            Destroy (otherCollider.gameObject);
        }
    }
}
