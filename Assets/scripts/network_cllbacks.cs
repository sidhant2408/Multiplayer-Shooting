using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class network_cllbacks : GlobalEventListener
{
    public GameObject player;
    public override void SceneLoadLocalDone(string scene)
    {
        var span_pos = new Vector3(0, 0.5f, 0);
        BoltNetwork.Instantiate(player, span_pos, Quaternion.identity);
    }
}
