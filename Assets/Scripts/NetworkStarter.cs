using UnityEngine;
using UnityEngine.Networking;

public class NetworkStarter : MonoBehaviour
{
    public NetworkManager manager;

    public void LanHost()
    {
        if (manager.IsClientConnected() || NetworkServer.active || manager.matchMaker != null)
            return;
        
        manager.StartHost();
    }

    public void StartServer()
    {
        if (manager.IsClientConnected() || NetworkServer.active || manager.matchMaker != null)
            return;

        manager.StartServer();
    }

    public void StartClient()
    {
        if (manager.IsClientConnected() || NetworkServer.active || manager.matchMaker != null)
            return;

        manager.StartClient();
    }
}
