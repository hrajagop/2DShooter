using UnityEngine;
using UnityEngine.Networking;

public class ActivePlayersDisplayer : NetworkBehaviour
{
    public int ActivePlayers { get; set; }

    [ServerCallback]
    void Update()
    {
        ActivePlayers = NetworkServer.connections.Count;
    }
}
