using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float acceleration = 5.0f;
    public float maxSpeed = 10.0f;
    public float drag = 2.0f;

    private float playerPlatformAcceleration = 30f;
    private float playerPlatformMaxSpeed = 25f;
    private bool isPlayerOnThePlatform = false;
    
    public float zReverseDelay = 0.5f; // Z deðeri tersine dönme gecikmesi süresi
    private float reverseTimer = 0f;

    private CheckpointManager _checkpointManager;
    private LevelManager _levelManager;
    private HealthManager _healthManager;
    private TweenManager _tweenManager;

    public static int numberOfCoins;

    public GameObject[] playerPrefabs;
    private int ballIndex;

    // Yeni deðiþken
    bool isMovingBackward = false;
    private Transform currentPlatform;

    private void Awake()
    {
        ballIndex = PlayerPrefs.GetInt("SelectedBall", 0);
        numberOfCoins = PlayerPrefs.GetInt("numberOfCoins", 0);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _checkpointManager = gameObject.GetComponent<CheckpointManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _healthManager = FindObjectOfType<HealthManager>();
        _tweenManager = FindObjectOfType<TweenManager>();
        if (_healthManager == null)
        {
            Debug.LogError("HealthManager not found!");
        }
    }

    void Update()
    {
        if (isPlayerOnThePlatform == true && _tweenManager.isBallsUIActive == false && _tweenManager.isBackgroundUIActive == false)
        {
            PlayerMovementOnPlatform();
        }
        else
        {
            Movement();
            if (currentPlatform != null)
            {
                // Player'ýn hýzýný platformun hýzýna göre güncelle
                Vector3 platformVelocity = currentPlatform.GetComponent<MovingPlatform>().GetVelocity();
                rb.velocity += platformVelocity;
            }
        }
    }

    #region Movements
    private void PlayerMovementOnPlatform()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition.normalized;
                Vector3 movement;

                if (rb.velocity.z >= 0) // Z ekseni pozitif ise normal hareket
                {
                    movement = new Vector3(touchDeltaPosition.x, 0.0f, touchDeltaPosition.y) * playerPlatformAcceleration * Time.deltaTime;
                }
                else // Z ekseni negatif ise ters hareket
                {
                    movement = new Vector3(-touchDeltaPosition.x, 0.0f, -touchDeltaPosition.y) * playerPlatformAcceleration * Time.deltaTime;
                }

                Vector3 newVelocity = rb.velocity + movement;
                newVelocity = Vector3.ClampMagnitude(newVelocity, playerPlatformMaxSpeed);
                rb.velocity = newVelocity;

                rb.velocity *= Mathf.Clamp01(1.0f - drag * Time.deltaTime);
            }
        }

        if (rb.velocity.z >= 0)
        {
            reverseTimer = 0f;
            isMovingBackward = false; // Z ekseni pozitif olduðunda geri gitme durumunu sýfýrla
        }

        if (rb.velocity.z < 0)
        {
            reverseTimer += Time.deltaTime;
            if (reverseTimer >= zReverseDelay)
            {
                isMovingBackward = true;
            }
        }
    }

    private void Movement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition.normalized;
                Vector3 movement;

                if (rb.velocity.z >= 0) // Z ekseni pozitif ise normal hareket
                {
                    movement = new Vector3(touchDeltaPosition.x, 0.0f, touchDeltaPosition.y) * acceleration * Time.deltaTime;
                }
                else // Z ekseni negatif ise ters hareket
                {
                    movement = new Vector3(-touchDeltaPosition.x, 0.0f, -touchDeltaPosition.y) * acceleration * Time.deltaTime;
                }

                Vector3 newVelocity = rb.velocity + movement;
                newVelocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);
                rb.velocity = newVelocity;

                rb.velocity *= Mathf.Clamp01(1.0f - drag * Time.deltaTime);
            }
        }

        if (rb.velocity.z >= 0)
        {
            reverseTimer = 0f;
            isMovingBackward = false; // Z ekseni pozitif olduðunda geri gitme durumunu sýfýrla
        }

        if (rb.velocity.z < 0)
        {
            reverseTimer += Time.deltaTime;
            if (reverseTimer >= zReverseDelay)
            {
                isMovingBackward = true;
            }
        }
    }
    #endregion

    #region Collisions and Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            _healthManager.health--;

            if (_checkpointManager.isPlayerPassCheckpoint == true)
            {
                transform.position = _checkpointManager.spawnPoint;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                transform.position = _levelManager.playerFirstPos;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            if (_healthManager.health <= 0)
            {
                transform.position = _levelManager.playerFirstPos;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ramp"))
        {
            isPlayerOnThePlatform = true;
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = collision.transform; // Platforma temas ettiðinde referansý güncelle
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = null; // Platformdan ayrýldýðýnda referansý temizle
        }

        if (collision.gameObject.CompareTag("Ramp"))
        {
            isPlayerOnThePlatform = false;
        }
    }
    #endregion
}