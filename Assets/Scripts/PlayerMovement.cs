using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 force;
    Rigidbody rigi;
    public float speed;
    public float mouseSpeed;
    public float topSpeed;
    public float jumpPower;
    bool canJump;
    public AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.playing)
        {
            return;
        }

        force.x = Input.GetAxis("Horizontal");
        force.z = Input.GetAxis("Vertical");
        force *= speed;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;

            Vector3 temp = rigi.velocity;
            temp.y = 0;
            rigi.velocity = temp;

            rigi.AddForce(Vector3.up * jumpPower);
            jumpSound.Play();
        }

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSpeed);
    }

    private void FixedUpdate()
    {
        if (!GameManager.playing)
        {
            rigi.isKinematic = true;
            return;
        }

        rigi.AddRelativeForce(force);
        if (rigi.velocity.magnitude > topSpeed)
        {
            rigi.velocity = rigi.velocity.normalized * topSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint x in collision.contacts)
        {
            if(x.point.y < transform.position.y -0.1f)
            {
                canJump = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Jump")
        {
            Destroy(other.gameObject);
        }

        if(other.tag == "Jump")
        {
            canJump = true;
        }

        if (other.tag == "ScoreUp")
        {
            GameManager.score += 100;
        }

        if (other.tag == "TimeUp")
        {
            GameManager.currentTimer += 5;
        }

        if (other.tag == "ScoreDown")
        {
            GameManager.score -= 500;
        }

        if (other.tag == "TimeDown")
        {
            GameManager.currentTimer -= 10;
        }
    }
}
