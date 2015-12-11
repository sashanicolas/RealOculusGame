using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CubePlayerMove : NetworkBehaviour {

    public GameObject bulletPrefab;

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

	void Update () {
        if (!isLocalPlayer) return;

        float x = Input.GetAxis("Horizontal") * 1.0f;
        float z = Input.GetAxis("Vertical") * 1.0f;

        transform.Translate(x, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
	}

    [Command]
    void CmdFire()
    {
        GameObject bullet = (GameObject) Instantiate(bulletPrefab, transform.position - transform.forward, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = -transform.forward * 4;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }
}
