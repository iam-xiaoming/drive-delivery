using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Car : MonoBehaviour
{
    [SerializeField] float steerSpeed = 10f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float boostSpeed = 12f;
    [SerializeField] float regularSpeed = 5f;
    [SerializeField] TMP_Text boostText;
    ParticleSystem carParticleSystem;
    bool isPickedup = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carParticleSystem = GetComponent<ParticleSystem>();
        boostText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // using UnityEngine.InputSystem;
        if (Keyboard.current.wKey.isPressed)
        {
            transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        }

        if (Keyboard.current.aKey.isPressed)
        {
            transform.Rotate(0, 0, steerSpeed * Time.deltaTime);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            transform.Rotate(0, 0, -steerSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package") && !isPickedup)
        {
            isPickedup = true;
            carParticleSystem.Play();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Customer") && isPickedup)
        {
            // Debug.Log("Delivered package to customer");
            isPickedup = false;
            carParticleSystem.Stop();
        }
        else if (other.CompareTag("boost"))
        {
            moveSpeed = boostSpeed;
            boostText.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        moveSpeed = regularSpeed;
        boostText.gameObject.SetActive(false);
    }
}
