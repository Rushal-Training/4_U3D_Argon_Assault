using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[Tooltip( "In seconds" )][SerializeField] float levelLoadDelay = 1f;
	[Tooltip ( "Effect prefab on player" )][SerializeField] GameObject deathFX;

	private void OnTriggerEnter ( Collider other )
	{
		StartDeathSequence ();
	}

	private void StartDeathSequence ()
	{
		SendMessage ( "OnPlayerDeath" );
		deathFX.SetActive ( true );
		Invoke ( "RestartLevel", levelLoadDelay );
	}

	private void RestartLevel () // string reference
	{
		SceneManager.LoadScene ( 1 );
	}
}