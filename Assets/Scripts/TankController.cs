using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankController : NetworkBehaviour
{
	public float MoveSpeed = 150.0f;
	public float RotateSpeed = 3.0f;
    public Color LocalPlayerColor;
    public GameObject ShotPrefab;
    public Transform ShotSpawnTransform;
    public float ShotSpeed;
    public float ReloadRate = 0.5f;

    private float _nextShotTime;

    public override void OnStartLocalPlayer()
    {
        var tankParts = GetComponentsInChildren<MeshRenderer>();
        foreach (var part in tankParts)
        {
            part.material.color = LocalPlayerColor;
        }
    }    
    
	void Update ()
    {
        if (!isLocalPlayer) { return; }

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * RotateSpeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextShotTime )
        {
            CmdFire();
        }
	}

    /// <summary>
    /// A command is a method that's called on the client but executed on the server.
    /// </summary>
    [Command]
    void CmdFire()
    {
        _nextShotTime = Time.time + ReloadRate;
        var bullet = Instantiate(ShotPrefab, ShotSpawnTransform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * ShotSpeed;

        NetworkServer.Spawn(bullet);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        try
        {
            var causeDamageScript = other.GetComponent<CauseDamage>();
            var totalDamage = causeDamageScript.GetDamage();
            var healthScript = GetComponent<Health>();
            healthScript.TakeDamage(totalDamage);
        }
        catch
        {
            Debug.Log("Something hit us but didn't do any damage.");
        }
    }

}
