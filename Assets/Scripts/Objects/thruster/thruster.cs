using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thruster : thrusterSettings{

	Rigidbody theRb;
	[SerializeField] private ParticleSystem[] thrusterEffect;
	protected bool toggled = false;

	void Start () {
		keyCode = defaultKeyCode;
		toggled = false;
		theRb = this.transform.root.GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		if (theRb.isKinematic)
		{
			setParticles(false);
			return;
		}

		if (!toggleMode) {    // If not on toggled mode
			if (Input.GetKey(keyCode))
			{
				setParticles(true);
				Vector3 _theForce = -this.transform.forward * thrusterForce;
				theRb.AddForceAtPosition(_theForce, this.transform.position, ForceMode.Force);
			}
		}
		else { // IF IN TOGGLE MODE
			if (Input.GetKeyDown(keyCode))
				toggled = !toggled;
			if (toggled)
			{
				setParticles(true);
				Vector3 _theForce = -this.transform.forward * thrusterForce;
				theRb.AddForceAtPosition(_theForce, this.transform.position, ForceMode.Force);
			}
		}

		if((!toggleMode && !Input.GetKey(keyCode)) || (toggleMode && !toggled))
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
