using UnityEngine;

public class CombinedController : MonoBehaviour
{
    [Range(1, 4)]
    public int Player;

    public float MovementSpeed;
    public float Acceleration;


    public float RotationSpeed;
    public float RotationAccel;

    Rigidbody rb;
    Grabber grabber;

    Animator anim;

    bool wantGrab;
    bool holdingHeavy => grabber.HoldingHeavy;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabber = GetComponent<Grabber>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!wantGrab && Input.GetAxisRaw($"Player{Player}Fire") > 0.5f)
        {
            wantGrab = true;
            grabber.TryGrab();
            anim.SetBool("IsCarrying", true);
        }
        else if(wantGrab && Input.GetAxisRaw($"Player{Player}Fire") < 0.5f)
        {
            wantGrab = false;
            grabber.Release();
            anim.SetBool("IsCarrying", false);
        }
    }

    void FixedUpdate()
    {
        var movement =
            Vector3.forward * Input.GetAxisRaw($"Player{Player}Vertical") +
            Vector3.right * Input.GetAxisRaw($"Player{Player}Horizontal");

        movement = Vector3.ClampMagnitude(movement, 1);
        if (movement.magnitude < 0.2f) movement = Vector3.zero;

        rb.velocity = Vector3.MoveTowards(rb.velocity, movement * MovementSpeed, Acceleration * Time.fixedDeltaTime);

        anim.SetBool("IsWalking", movement != Vector3.zero);


        if (!holdingHeavy && movement.magnitude > 0.2f)
        {
            var dot = Vector3.Dot(movement.normalized, transform.right);
            var asin = Mathf.Asin(dot);

            var targetSpeed = 10 * Mathf.Clamp(asin, -.1f, .1f) * RotationSpeed;

            if(Mathf.Abs(asin) < Mathf.Abs(targetSpeed) * Time.fixedDeltaTime)
            {
                targetSpeed = asin / Time.fixedDeltaTime;
            }

            rb.angularVelocity = new Vector3(
                0,
                Mathf.MoveTowards(rb.angularVelocity.y, targetSpeed, RotationAccel * Time.fixedDeltaTime));
        }

        //Vector3 pos = rb.transform.forward;

        //float dot = Vector3.Dot(pos, movement);

        //float acos = Mathf.Acos(dot);

        //Vector3 cross = Vector3.Cross(pos, movement);

        //rb.AddRelativeTorque(cross * torque * acos - (rb.angularVelocity / 2), ForceMode.VelocityChange);

        //if (Input.GetAxis("Fire1") > 0)
        //    Debug.Log("P1 picks up");
    }
}