using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class SpaceshipMovement : MonoBehaviour
{
    public bool canMove = false;

    [SerializeField] GameController gameController;

    [SerializeField] float originSpeed = 50f;
    [SerializeField] float sideSpeed = 5f;
    [SerializeField] float rotateSpeed = 1000f;

    [SerializeField] float leftBorderPos = -6f;
    [SerializeField] float rightBorderPos = 6f;

    private float x;
    private float rotateShipZ;
    private Vector3 moveVector;
    private float speed;

    private Quaternion originShipRotation;

    void Start()
    {
        moveVector = new Vector3(0, 0, 1);
        originShipRotation = transform.rotation;
    }

    
    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        if (canMove)
        {
            moveVector.z += speed * Time.deltaTime;

            if (Mathf.Abs(input) > .1f)
            {
                // ship tilts in different direction
                rotateShipZ += Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
                rotateShipZ = Mathf.Clamp(rotateShipZ, -40, 40);

                Quaternion newRotateShipZ = Quaternion.AngleAxis(-rotateShipZ, Vector3.forward);
                transform.rotation = originShipRotation * newRotateShipZ;

                // movement along x-axis
                x = transform.position.x + input;
                x = Mathf.Clamp(x, leftBorderPos, rightBorderPos);
            }
            
            // acceleration movement
            if (Input.GetKey(KeyCode.Space))
            {
                speed = originSpeed * 2;
            } else
            {
                speed = originSpeed;
            }

            transform.position = new Vector3(x, 0, moveVector.z);
        }
    }

    // Game over from crash with asteroid
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Asteroid")
        {
            Destroy(collision.gameObject);

            gameController.StopGame();
            gameObject.SetActive(false);
            moveVector = new Vector3(0, 0, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            gameController.score += 5;
            gameController.passedAsteroids++;
        }
    }

}
