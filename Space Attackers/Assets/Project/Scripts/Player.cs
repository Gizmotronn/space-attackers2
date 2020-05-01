using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 1.5f;
    public float horizontalLimit = 2.5f; // See update

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
    }
}
