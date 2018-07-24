using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	[Tooltip ( "In meters per second" )][SerializeField] float speed = 20f;
	[Tooltip ( "In meters" )][SerializeField] float xRange = 4f;
	[Tooltip ( "In meters" )] [SerializeField] float yRange = 2.75f;

	[SerializeField] float positionPitchFactor = -5f;
	[SerializeField] float controlPitchFactor = -20f;
	[SerializeField] float positionYawFactor = 5f;
	[SerializeField] float controlRollFactor = -20f;

	float xThrow, yThrow;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		Move ();
	}

	private void Move ()
	{
		ProssessTranslation ();
		ProcessRotation ();
	}

	private void ProssessTranslation ()
	{
		xThrow = CrossPlatformInputManager.GetAxis ( "Horizontal" );
		yThrow = CrossPlatformInputManager.GetAxis ( "Vertical" );

		float xOffset = speed * xThrow * Time.deltaTime;
		float yOffset = speed * yThrow * Time.deltaTime;

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
}
