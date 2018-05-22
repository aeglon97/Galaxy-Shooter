using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float speed = 5.0f;

	// Use this for initialization
	private void Start ()
    { 
        transform.position = new Vector3(0, 0, 0);
	}

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //moving the player
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        Debug.Log(transform.position);

        //setting player bounds
        if (transform.position.x < -7.9f)
        {
            transform.position = new Vector3(-7.9f, transform.position.y, 0);
        }
        else if (transform.position.x > 7.9f)
        {
            transform.position = new Vector3(7.9f, transform.position.y, 0);
        }

        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        else if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
    }
}
