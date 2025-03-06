using Unity.VisualScripting;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private float enginePower = 20f;
    [SerializeField] private float liftBooster = 0.5f;
    [SerializeField] private float drag = 0.001f;
    [SerializeField] private float angularDrag = 0.001f;

    [SerializeField] private float yawPower = 500f;
    [SerializeField] private float pitchPower = 50f;
    [SerializeField] private float rollPower = 30f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.forward * enginePower);
        }

        Vector3 lift = Vector3.Project(rb.velocity, transform.forward);
        rb.AddForce(transform.up * lift.magnitude * liftBooster);

        rb.linearDamping = rb.velocity.magnitude * drag;
        rb.angularDamping = rb.velocity.magnitude * angularDrag;

        float yaw = Input.GetAxis("Horizontal") * yawPower * Time.deltaTime;  // Left/Right (Q/E)
        float pitch = Input.GetAxis("Vertical") * pitchPower * Time.deltaTime; // Nose Up/Down (W/S)
        float roll = Input.GetAxis("Roll") * rollPower * Time.deltaTime; // Roll (A/D)
       
        rb.AddTorque(transform.up * yaw);       // Yaw (Turn left/right)
        rb.AddTorque(transform.right * pitch);  // Pitch (Nose up/down)
        rb.AddTorque(transform.forward * -roll); // Roll (Tilting)
    }
}
