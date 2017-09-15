using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyJoystick;
using Global;
using UnityEngine.UI;

public class CS_GameManager : MonoBehaviour {

	[SerializeField] int myPatternSize = 32;
	[SerializeField] GameObject[] myPlayerReady;
	private int myRandomSeed = -1;

	[SerializeField] GameObject myPlayerPrefab;
	private List<GameObject> myPlayers = new List<GameObject> ();

	[SerializeField] InputField mySeedInput;
	[SerializeField] Text mySeedDisplay;
	[SerializeField] GameObject myLobby;
	// Use this for initialization
	void Start () {
		foreach (GameObject t_ready in myPlayerReady) {
			t_ready.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 2; i++) {
			if (JellyJoystickManager.Instance.GetButton (ButtonMethodName.Down, i + 1, JoystickButton.A)) {
				myPlayerReady [i].SetActive (true);
			}
		}
	}

	public void StartRandomGame () {
		CreateRandomSeed ();
		StartGame ();
	}

	public void StartSeedGame () {
		myRandomSeed = int.Parse (mySeedInput.text);
		StartGame ();
	}

	private void StartGame () {
		mySeedDisplay.text = "Seed: " + myRandomSeed.ToString ("000000");

		for (int i = 0; i < 2; i++) {
			if (myPlayerReady [i].activeSelf == true) {
				GameObject t_player = Instantiate (myPlayerPrefab, this.transform);
				t_player.GetComponent<CS_PlayerController> ().Init (i + 1, myPatternSize, myRandomSeed);
				myPlayers.Add (t_player);
			}
		}

		if (myPlayers.Count == 0) {
			GameObject t_player = Instantiate (myPlayerPrefab, this.transform);
			t_player.GetComponent<CS_PlayerController> ().Init (0, myPatternSize, myRandomSeed);
			myPlayers.Add (t_player);
		}

		if (myPlayers.Count == 2) {
			myPlayers [0].transform.position = new Vector2 (-2, myPlayers [0].transform.position.y);
			myPlayers [1].transform.position = new Vector2 (2, myPlayers [0].transform.position.y);
		} else {
			myPlayers [0].transform.position = new Vector2 (0, myPlayers [0].transform.position.y);
		}

		myLobby.SetActive (false);
	}
		
	private void CreateRandomSeed () {
		myRandomSeed = Random.Range (0, GlobalInfo.NUMBER_MAX_SEED);
	}

}
