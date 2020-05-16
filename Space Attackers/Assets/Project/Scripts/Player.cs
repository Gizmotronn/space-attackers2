using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 1.5f;
    public float horizontalLimit = 2.5f; // See update
    public float firingSpeed = 3f;
    public GameObject missilePrefab; // Game object (prefab) inserted into inspector tab for player
    public GameObject explosionPrefab; // https://github.com/Gizmotronn/space-attackers/issues/4

    //Cooldown effect
    public float cooldownDuration = 1f; // only 1 missile per second
    private float cooldownTimer;
    public float firingCooldownDuration = 1f;

    // Make it so that you have to release the key (left ctrl --> Fire1) to be able to shoot another missile
    private bool fired = false; // See fire missiles in void update

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player
        GetComponent<Rigidbody2D> ().velocity = new Vector2 (
            Input.GetAxis("Horizontal") * speed, // x
            0 // Was y, but since the player won't move up or down (at least not in the initial scene), we can just do 0 instead # ^
        );

        // Keep the player within bounds /#/ horizontalLimit
        if (transform.position.x > horizontalLimit) {
            transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
        } if (transform.position.x < -horizontalLimit) {
            transform.position = new Vector3(-horizontalLimit, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
        }

		// Fire missiles.
		cooldownTimer -= Time.deltaTime; // Time.deltaTime = how many seconds since last frame /#/ countdown clock
		if (Input.GetAxis ("Fire1") == 1f) {
			if (cooldownTimer <= 0 && fired == false) {
				fired = true;

				cooldownTimer = firingCooldownDuration;

				GameObject missileInstance = Instantiate (missilePrefab);
				missileInstance.transform.SetParent (transform.parent);
				missileInstance.transform.position = transform.position;
				missileInstance.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, firingSpeed);
				Destroy (missileInstance, 2f);
			}
		} else {
			fired = false;
		}

        void OnTriggerEnter2D (Collider2D otherCollider) {
            if (otherCollider.tag == "EnemyMissile") {
                GameObject explosionInstance = Instantiate (explosionPrefab);
                explosionInstance.transform.SetParent(transform.parent); // same as this.transfer.parent --> parent of the enemy
                explosionInstance.transform.position = transform.position;

                Destroy (explosionInstance, 1.5f); //1.5f = 1.5 seconds
                Destroy (gameObject);
                Destroy (otherCollider.gameObject);
        }
    }
	}
}
