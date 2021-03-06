﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyJoystick;
using Global;

public class CS_PlayerController : MonoBehaviour {

	[SerializeField] int myController = 1;

	[SerializeField] GameObject myPatternManagerPrefab;
	[SerializeField] int myPatternSize = 32;
	private Transform myPatternManagerTransform;
	private CS_PatternManager myPatternManager;
	private Vector2 myTargetPatternPosition;
	[SerializeField] float myPatternMoveSpeed = 2;

	private int myCurrentIndex = 0;
	private Symbol myCurrentSymbol = Symbol.A;

	private bool isWinning = false;
	private float myTimer = 0;
	[SerializeField] TextMesh myTimerTextMesh;

	[SerializeField] AudioClip mySFX_Right;
	[SerializeField] AudioClip mySFX_Wrong;
	[SerializeField] AudioClip mySFX_Win;

	// Use this for initialization
	void Awake () {
		
	}

	void Start () {

	}

	public void Init (int g_controller, int g_patternSize, int g_seed) {
		myController = g_controller;

		myPatternManagerTransform = ((GameObject)Instantiate (myPatternManagerPrefab, this.transform)).transform;
		myPatternManagerTransform.localPosition = Vector3.zero;
		myPatternManager = myPatternManagerTransform.GetComponent<CS_PatternManager> ();

		myPatternManager.SetRandomSeed (g_seed);

		//test

		myPatternManager.CreatePattern (myPatternSize);

		//init
		myCurrentIndex = 0;
		myCurrentSymbol = myPatternManager.GetMySymbol (myCurrentIndex).GetComponent<CS_Symbol> ().GetSymbol ();

		myTimer = 0;
		myTimerTextMesh.text = "0";
	}
	
	// Update is called once per frame
	void Update () {

		UpdateMove ();

		if (isWinning)
			return;

		if (myCurrentIndex != 0) {
			myTimer += Time.deltaTime;
			myTimerTextMesh.text = myTimer.ToString ("##.###");
		}
		
		if ((JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myController, JoystickButton.A) && myCurrentSymbol == Symbol.A) ||
		    (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myController, JoystickButton.B) && myCurrentSymbol == Symbol.B) ||
		    (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myController, JoystickButton.X) && myCurrentSymbol == Symbol.X) ||
		    (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myController, JoystickButton.Y) && myCurrentSymbol == Symbol.Y)) {

			CS_AudioManager.Instance.PlaySFX (mySFX_Right);

			myCurrentIndex++;
			GetCurrentSymbol ();
			Move ();
		} else if ((JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myController, JoystickButton.A)) ||
		           (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myController, JoystickButton.B)) ||
		           (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myController, JoystickButton.X)) ||
		           (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, myController, JoystickButton.Y))) {
			CS_AudioManager.Instance.PlaySFX (mySFX_Wrong);
		}
	}

	private void Move () {
		myTargetPatternPosition = new Vector2 (0, GlobalInfo.DISTANCE_SYMBOL * myCurrentIndex * -1);
	}

	private void UpdateMove () {
		myPatternManagerTransform.localPosition = Vector2.Lerp (myPatternManagerTransform.localPosition, myTargetPatternPosition, Time.deltaTime * myPatternMoveSpeed);
	}

	private void GetCurrentSymbol () {
		if (myCurrentIndex >= myPatternSize) {
			isWinning = true;
			CS_AudioManager.Instance.PlaySFX (mySFX_Win);
			return;
		}
			
		myCurrentSymbol = myPatternManager.GetMySymbol (myCurrentIndex).GetComponent<CS_Symbol> ().GetSymbol ();
	}

}
