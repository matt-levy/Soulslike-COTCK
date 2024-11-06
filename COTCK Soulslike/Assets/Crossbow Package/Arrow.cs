using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject model;
    [Header("Arrow Stats")]
    public float speed = 20f;
    public float rotationSpeed = 90f;
    [HideInInspector] public float damage;
    private Rigidbody rb;

    // Flags
    private bool inFlight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        // Simulate spinning
        if (inFlight)
        {
            model.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inFlight)
        {
            inFlight = false;
            rb.Sleep();
        }
    }
}
