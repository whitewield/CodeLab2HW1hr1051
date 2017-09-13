using System.Collections;
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
	// Use this for initialization
	void Awake () {
		
	}

	void Start () {

		myPatternManagerTransform = ((GameObject)Instantiate (myPatternManagerPrefab, this.transform)).transform;
		myPatternManagerTransform.localPosition = Vector3.zero;
		myPatternManager = myPatternManagerTransform.GetComponent<CS_PatternManager> ();

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

			myCurrentIndex++;
			GetCurrentSymbol ();
			Move ();
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
			return;
		}
			
		myCurrentSymbol = myPatternManager.GetMySymbol (myCurrentIndex).GetComponent<CS_Symbol> ().GetSymbol ();
	}

}
