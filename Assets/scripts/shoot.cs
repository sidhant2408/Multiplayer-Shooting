using UnityEngine;
using System.Collections;

public class shoot : Bolt.EntityBehaviour<Iperson>
{
    public Camera cam;
    public GameObject gun;
    public float hip_radius;
    public bool aim_down = false;
    Vector3 dir;
    public GameObject bullet_effect, gun_flash;
    public Transform muzzel;
    public float maxxammo;
    float currentammo = -1;
    public float damage_amount;
    public float impact_force;
    public float range;
    public float fire_rate = 15f;
    public float reload_time;
    float next_time_to_fire = 0;
    public float vertical_recoil, horizontal_recoil;
    public Vector3 m;
    bool isreloading = false;

    public Vector3 hip_position, ads_position;

    private void Start()
    {
        if(currentammo == -1)
            currentammo = maxxammo;
    }
    private void OnEnable()
    {
        isreloading = false;
    }
    public override void Attached()
    {
        state.Onperson_shoot = fire;
        state.person_gun_hit_point = m;
        state.AddCallback("person_gun_hit_point", hit_point_callback);
    }

    void Update()
    {
        if (isreloading)
            return;

        if(currentammo <= 0)
        {
            StartCoroutine(reload());
            return;
        }
        if (Input.GetMouseButton(0) && Time.time >= next_time_to_fire && entity.IsOwner)
        {
            next_time_to_fire = Time.time + 1f / fire_rate;
            state.person_shoot();
            transform.GetComponent<firstst_person_movement>().addrecoil(vertical_recoil, horizontal_recoil);
        }
        if (Input.GetMouseButtonDown(1) && entity.IsOwner)
        {
            aim_down = !aim_down;
            if(aim_down) gun.transform.localPosition = ads_position;
            else gun.transform.localPosition = hip_position;

        }
    }

    private void hit_point_callback()
    {
        m = state.person_gun_hit_point;
    }

    void fire()
    {
        RaycastHit hit;
        float s = Random.Range(-hip_radius, hip_radius);
        Vector3 m = new Vector3(s, s, 0);
        if (!aim_down)
        {
            dir = cam.transform.forward + m;
        }
        if (aim_down)
        {
            dir = cam.transform.forward;
        }
        if (Physics.Raycast(cam.transform.position, dir, out hit, range))
        {
            health Phealth = hit.transform.GetComponent<health>();
            if (Phealth != null)
            {
                Phealth.state.health -= (int)damage_amount;
            }
        }

        if(hit.collider.tag != "Player")
        {
            state.person_gun_hit_point = hit.point;
            GameObject b = Instantiate(bullet_effect, state.person_gun_hit_point, Quaternion.LookRotation(hit.normal));
            Destroy(b, 3f);
        }
        GameObject muzz = Instantiate(gun_flash, muzzel.transform.position, muzzel.transform.rotation);
        Destroy(muzz, .1f);
        currentammo--;
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impact_force * damage_amount);
        }
    }

    IEnumerator reload()
    {
        isreloading = true;
        currentammo = maxxammo;
        yield return new WaitForSeconds(reload_time);
        isreloading = false;
    }
}
