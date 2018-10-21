using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thruster : thrusterSettings{

	Rigidbody theRb;
	[SerializeField] private ParticleSystem[] thrusterEffect;

	void Start () {
		keyCode = defaultKeyCode;	

		theRb = this.transform.root.GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		if (theRb.isKinematic)
		{
			setParticles(false);
			return;
		}

		if (Input.GetKey(keyCode))
		{
			setParticles(true);
			Vector3 _theForce =  - this.transform.forward * thrusterForce;
			theRb.AddForceAtPosition(_theForce , this.transform.position, ForceMode.Force);
		}
		else
		{
			setParticles(false);
		}
	}

	void setParticles(bool enabled)
	{
		foreach (ParticleSystem ps in thrusterEffect)
		{
			ParticleSystem.EmissionModule _em = ps.emission;
			_em.enabled = enabled;
		}
	}

}
