using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class gameManage : NetworkBehaviour
{
    
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        spawnplayer();
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }
    void spawnplayer() {

        GameObject player = Instantiate(cube);
        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
      
    }
    
}
