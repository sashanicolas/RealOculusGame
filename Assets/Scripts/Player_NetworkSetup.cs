﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_NetworkSetup : NetworkBehaviour{

    [SerializeField]
    Camera FPSCharacterCam;
    [SerializeField]
    AudioListener audioListener;

	void Start () {
        if (isLocalPlayer)
        {
            GameObject.Find("Main Camera").SetActive(false);
            
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCharacterCam.enabled = true;
            audioListener.enabled = true;
        }
	}
	
}
