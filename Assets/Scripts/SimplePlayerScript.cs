using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SimplePlayerScript : NetworkBehaviour{

    struct CubeState
    {
        public int x;
        public int y;
    }

    [SyncVar]
    CubeState state;

    void Awake()
    {
        InitState();
    }

    [Server] 
    void InitState()
    {
        state = new CubeState
        {
            x = 0,
            y = 10
        };
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        KeyCode[] arrowKeys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow };
        foreach (KeyCode arrowKey in arrowKeys)
        {
            if (!Input.GetKeyDown(arrowKey)) continue;
            CmdMoveOnServer(arrowKey);
        }
     
        SyncState();
    }

    void SyncState()
    {
        transform.position = new Vector2(state.x, state.y);
    }
    
    [Command]
    void CmdMoveOnServer(KeyCode arrowKey)
    {
        state = Move(state, arrowKey);
    }

    CubeState Move(CubeState previous, KeyCode arrowKey)
    {
        int dx = 0;
        int dy = 0;
        switch (arrowKey)
        {
            case KeyCode.UpArrow:
                dy = 1;
                break;
            case KeyCode.DownArrow:
                dy = -1;
                break;
            case KeyCode.RightArrow:
                dx = 1;
                break;
            case KeyCode.LeftArrow:
                dx = -1;
                break;
        }
        return new CubeState
        {
            x = dx + previous.x,
            y = dy + previous.y
        };
    }


}
