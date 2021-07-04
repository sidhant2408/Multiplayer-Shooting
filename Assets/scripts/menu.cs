using UnityEngine;
using Bolt;
using Bolt.Matchmaking;
using UdpKit;
using System;

public class menu : GlobalEventListener
{
    // Start is called before the first frame update
    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public override void BoltStartDone()
    {
        BoltMatchmaking.CreateSession(sessionID: "test", sceneToLoad: "game");
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach( var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            if(photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
    }
}
