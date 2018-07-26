using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
	[Header("General")]
	[Tooltip ( "In meters per second" )][SerializeField] float controlSpeed = 10f;
	[Tooltip ( "In meters" )][SerializeField] float xRange = 8f;
	[Tooltip ( "In meters" )] [SerializeField] float yRange = 4f;
	[SerializeField] GameObject [] guns;

	[Header ( "Screen-position Based" )]
	[SerializeField] float positionPitchFactor = -5f;
	[SerializeField] float positionYawFactor = 6.5f;

	[Header ( "Control-throw Based" )]
	[SerializeField] float controlRollFactor = -30f;
	[SerializeField] float controlPitchFactor = -20f;

	float xThrow, yThrow;
	bool isControllable = true;

	void Update ()
	{
        if (isControllable)
        {
            ProssessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

	private void ProssessTranslation ()
	{
		xThrow = CrossPlatformInputManager.GetAxis ( "Horizontal" );
		yThrow = CrossPlatformInputManager.GetAxis ( "Vertical" );

		float xOffset = controlSpeed * xThrow * Time.deltaTime;
		float yOffset = controlSpeed * yThrow * Time.deltaTime;

		float rawXPos = transform.localPosition.x + xOffset;
		float rawYPos = transform.localPosition.y + yOffset;

		float clampedXPos = Mathf.Clamp ( rawXPos, -xRange, xRange );
		float clampedYPos = Mathf.Clamp ( rawYPos, -yRange, yRange );

		transform.localPosition = new Vector3 ( clampedXPos, clampedYPos, transform.localPosition.z );
	}

	private void ProcessRotation ()
	{
		float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;

		float yaw = transform.localPosition.x * positionYawFactor;

		float roll = xThrow * controlRollFactor;

		transform.localRotation = Quaternion.Euler ( pitch, yaw, roll );
	}

	private void ProcessFiring ()
	{
		if ( CrossPlatformInputManager.GetButton ( "Fire" ) )
		{
			SetGunsActive (true);
		}
		else
		{
            SetGunsActive (false);
		}
	}

	private void SetGunsActive (bool isActive)
	{
		foreach ( GameObject gun in guns )
		{
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
	}

	private void OnPlayerDeath () // Called by string reference
	{
		isControllable = false;
		print ( "controls frozen" );
	}
}
