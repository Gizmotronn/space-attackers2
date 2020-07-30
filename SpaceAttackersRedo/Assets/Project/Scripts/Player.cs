using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.5f;
    public float horizontalLimit = 2.5f;
    public float firingSpeed = 3f;
    public GameObject missilePrefab;

    private bool fired = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        GetComponent<Rigidbody2D> ().velocity = new Vector2 (
            Input.GetAxis("Horizontal") * speed, // x
            0 // y
        );

        // Keep the player within bounds
        if (transform.position.x > horizontalLimit) {
            transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
        } if (transform.position.x < -horizontalLimit) {
            transform.position = new Vector3(-horizontalLimit, transform.position.y, transform.position.z);
        } 

        // Fire missile
        if (Input.GetAxis ("Fire1") == 1f) {
            if (fired == false) {
                fired = true;
                //Debug.Log ("Fire!");

                GameObject missileInstance = Instantiate (missilePrefab);
                missileInstance.transform.SetParent (transform.parent);
                missileInstance.transform.position = transform.position;
                missileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, firingSpeed);
                Destroy(missileInstance, 2f);
            }            
        } else {
            fired = false;
        }
    }
}
