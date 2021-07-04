using UnityEngine;

public class firstst_person_movement : Bolt.EntityBehaviour<Iperson>
{
    public Camera cam;
    //public CharacterController controller;
    public float speed;
    float sprint_speed;
    public float Mouse_sensitivity;
    float x_rotation = 0, x, y;
    public float gravity = 30f;
    public float jump_force = 10f;
    public GameObject ground_check;
    public LayerMask what_is_ground;
    Vector3 velocity;
    float vertical_recoil, horizontal_recoil;
    float radii = 0.1f;
    public GameObject canvs;
    public Rigidbody rb;
    public bool is_grounded;
    public Animator animator;

    void Start()
    { 
        //Cursor.lockState = CursorLockMode.Locked;
        sprint_speed = speed * 2f;
        rb = gameObject.GetComponent<Rigidbody>();
        state.SetAnimator(animator);
    }

    public override void Attached()
    {
        state.SetTransforms(state.person_transform, transform);

        if (entity.IsOwner)
        {
            canvs.SetActive(true);
            cam.enabled = true;
            transform.tag = "Untagged";
        }
        else
        {
            //canvs.enabled = false;
            cam.enabled = false;
            transform.tag = "Player";
        }

        state.Animator.applyRootMotion = entity.IsOwner;
    }

    public override void SimulateOwner()
    {
        is_grounded = Physics.CheckSphere(ground_check.transform.position, radii, what_is_ground);
        float bb = Random.Range(-horizontal_recoil, horizontal_recoil);
        float mouse_x = Input.GetAxis("Mouse X") * Mouse_sensitivity * BoltNetwork.FrameDeltaTime + bb * BoltNetwork.FrameDeltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * Mouse_sensitivity * BoltNetwork.FrameDeltaTime + vertical_recoil * BoltNetwork.FrameDeltaTime;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        var walking = state.is_walking;
        var jump = state.is_grounded;
        
        
        vertical_recoil = 0;
        horizontal_recoil = 0;

        //this rotate camera vertically
        x_rotation -= mouse_y;
        x_rotation = Mathf.Clamp(x_rotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f);

        //This rotate horizontally
        transform.Rotate(Vector3.up * mouse_x);
        

        //movement
        Vector3 MoveTowards = transform.right * x + transform.forward * y;

        //sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //controller.Move(MoveTowards * sprint_speed * BoltNetwork.FrameDeltaTime);
            rb.velocity = MoveTowards.normalized * sprint_speed * BoltNetwork.FrameDeltaTime;
        }
        else
        {
            //controller.Move(MoveTowards * speed * BoltNetwork.FrameDeltaTime);
            rb.velocity = MoveTowards.normalized * speed * BoltNetwork.FrameDeltaTime;
        }


        jump = is_grounded;


        if (x == 0 && y == 0)
        {
            walking = false;
        }
        else
        {
            walking = true;
        }
    


        //jump
        /*if (is_grounded && Input.GetButtonDown("Jump"))
            rb.AddForce(transform.up * jump_force * BoltNetwork.FrameDeltaTime);
        if (!is_grounded)
            Physics.gravity = new Vector3(0, -gravity, 0);*/
       /* if (is_grounded && velocity.y <= 0)
        {
            rb.velocity = -2f*rb.transform.up;
        }*/

        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            rb.AddForce(rb.transform.up * jump_force, ForceMode.Force);
        }

        rb.AddForce(rb.mass * gravity * BoltNetwork.FrameDeltaTime * -rb.transform.up);
        //rb.velocity += -4 * gravity * rb.transform.up * BoltNetwork.FrameDeltaTime;
        //controller.Move(velocity * BoltNetwork.FrameDeltaTime);

        state.is_walking = walking;
        state.is_grounded = jump;
    }

    public void addrecoil(float v, float x_recoil)
    {
        vertical_recoil = v;
        horizontal_recoil = x_recoil;
    }

}
