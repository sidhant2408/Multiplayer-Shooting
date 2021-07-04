using UnityEngine.UI;

public class health : Bolt.EntityBehaviour<Iperson>
{
    public int local_health  = 100;
    public Slider slider;

    private void Start()
    {
        slider.maxValue = local_health;
    }
    public override void Attached()
    {
        state.health = local_health;
        state.AddCallback("health", health_callback);
    }

    private void health_callback()
    {
        local_health = state.health;
        if (state.health <= 0)
            BoltNetwork.Destroy(gameObject);
    }

    public void Update()
    {
        slider.value = local_health;
    }
}
