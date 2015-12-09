﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_SyncRotation : NetworkBehaviour {

    [SyncVar]
    Quaternion syncPlayerRotation;
    [SyncVar]
    Quaternion syncCamRotation;

    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform camTransform;

    [SerializeField]
    float lerpRate = 15;

    Quaternion lastPlayerRot;
    Quaternion lastCamRot;
    float threshold = 5;

    void Update()
    {
        LerpRotation();
    }

	void FixedUpdate () {
        TransmitRotations();        
	}

    void LerpRotation()
    {
        playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
        camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot)
    {
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }

    [Client]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            if ((Quaternion.Angle(playerTransform.rotation, lastPlayerRot) > threshold || Quaternion.Angle(camTransform.rotation, lastCamRot) > threshold))
            {
                CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);
                lastPlayerRot = playerTransform.rotation;
                lastCamRot = camTransform.rotation;
            }            
        }
    }
}
