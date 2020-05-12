using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{ 
    // Variables relating to the shooting
    public float shootingInterval = 3f; // 3 seconds
    public float shootingSpeed = 1f; // How fast the missile is shot at the player
    public GameObject enemyMissilePrefab; // Reference to enemy missile prefab 
    private float shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        shootingTimer = shootingInterval; 
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer -= Time.deltaTime; // Code run every 3 seconds
        if (shootingTimer <= 0f) {
            shootingTimer = shootingInterval; // Shoot a missile at the player 

            // Setting which missile will be fired (random)
            Enemy[] enemies = GetComponentsInChildren<Enemy> (); // References to all the enemies that we have that are under the gameController in the Unity editor hierachy
            Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)]; // Fetch a new enemy, this enemy will fire a missile at the player / Index values # ensures that if we have 10 enemies, it will only give 0-9, this one is the one that fires an enemy missile

            GameObject missileInstance = Instantiate (enemyMissilePrefab); //Pass false to set the Object's position relative to its new parent.
            missileInstance.transform.SetParent (transform);
            missileInstance.transform.position = randomEnemy.transform.position;
            missileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -shootingSpeed); // -shootingSpeed because it is going to move down / 0 is how much it moves on the x plane

            // Destroy missile (prefab) after it fires
            Destroy (missileInstance, 3f); // Destroys the missile after 3 seconds            

        }
    }
}
