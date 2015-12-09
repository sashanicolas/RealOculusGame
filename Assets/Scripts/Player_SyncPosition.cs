using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_SyncPosition : NetworkBehaviour {

    [SyncVar]
    private Vector3 syncPos;
    [SerializeField]
    Transform myTransform;
    [SerializeField]
    float lerpRate = 15;

    Vector3 lastPos;
    float threshold = 0.5f;
    

	void FixedUpdate () {
        TransmitPosition();
        LerpPosition();
	}

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdprovidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshold)
        {
            CmdprovidePositionToServer(myTransform.position);
            lastPos = myTransform.position;
        }
            
    }

}
