using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;

    // Rigidbody of the player.
    private Rigidbody rb;

    /// Movement along X and Y axes.
    private float movementX;
    private float movementY;
    
    // Score count
    private int count = 0;

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created.
    /// </summary>
    void Start()
    {
        winTextObject.SetActive(false);
        SetCountText();

        // Get and store the attached rigidbody component.
        rb = GetComponent<Rigidbody>();

    }

    /// <summary>
    /// Called when a player presses movement keys.
    /// </summary>
    /// <param name="movementValue">Movement input value</param>
    void OnMove(InputValue movementValue)
    {
        // Convert movementValue into Vector2 for axes
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store X and Y axes
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    /// <summary>
    /// When colliding object triggers the event
    /// </summary>
    /// <param name="other">other object</param>
    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            count = count + 1;

            SetCountText();
        }
    }

    /// <summary>
    /// Function to update the displayed count of "PickUp" objects collected.
    /// </summary>
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 15)
        {
            winTextObject.SetActive(true);
        }
    }

}
