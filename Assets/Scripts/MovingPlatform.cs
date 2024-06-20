using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float forceSpeed;

    public float time;
    public float interval;
    public bool timer;

    // Eklenen deðiþken
    private Vector3 platformVelocity;
    // Start is called before the first frame update
    void Start()
    {
        forceSpeed = speed;
        rb = GetComponent<Rigidbody>(); // Rigidbody'yi burada al
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.right * forceSpeed;

        if (timer)
        {
            time += Time.deltaTime;
            if (time > interval)
            {
                if (forceSpeed > 0)
                {
                    forceSpeed = -speed;
                    timer = false;
                    time = 0;
                }
                else if (forceSpeed < 0)
                {
                    forceSpeed = speed;
                    timer = false;
                    time = 0;
                }
            }
        }
        platformVelocity = rb.velocity;
    }

    public Vector3 GetVelocity()
    {
        return platformVelocity; // Platformun hýzýný döndür
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            timer = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        }
    }
}
