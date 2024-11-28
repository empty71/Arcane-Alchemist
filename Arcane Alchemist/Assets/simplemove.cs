using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplemove : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public GameObject reset;
   

    void Update()
    {
        // Move the bullet forward continuously
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check for player input to rotate the bullet
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the bullet based on player input
        transform.Rotate(verticalInput * rotationSpeed * Time.deltaTime, horizontalInput * rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = reset.transform.rotation;
        }
    }
}
