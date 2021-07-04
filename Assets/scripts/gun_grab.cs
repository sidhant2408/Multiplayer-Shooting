/*using UnityEngine;

public class gun_grab : Bolt.EntityBehaviour<Iperson>
{
    public Rigidbody guns, gm;
    public Transform GunGrab;
    public Camera cam;
    private bool have_gun = false;

    public override void Attached()
    {
        state.Onperson_gungrab = check_for_gun;
    }


    void Update()
    {
        //RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.F) && entity.IsOwner)
        {
            state.person_gungrab();
            /*
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f))
            {
                if(hit.collider.tag == "gun")
                {
                    foreach (GameObject gun in guns)
                    {
                        go = gun;
                        
                        break;
                    }
                }
            }
        }
    }

    void check_for_gun()
    {
        gm = Instantiate(guns, GunGrab.position, GunGrab.rotation, GunGrab);
        gm.GetComponent<Rigidbody>().isKinematic = true;
    }
}
*/