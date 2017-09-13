using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Pattern : MonoBehaviour {
	[SerializeField] int myRandomSeed = -1;
	[SerializeField] GameObject mySymbolPrefabs;
	[SerializeField] int myPatternSize = 32;
	private List<Transform> myPattern = new List<Transform> ();
	// Use this for initialization
	void Start () {
		CreatePattern ();
	}

	public void CreatePattern () {
		if (myRandomSeed == -1) {
			myRandomSeed = Random.Range (0, 1000000);
		}

		Random.InitState (myRandomSeed);

		for (int i = 0; i < myPatternSize; i++) {
			GameObject t_symbol = Instantiate (mySymbolPrefabs, this.transform) as GameObject;
			t_symbol.transform.position = new Vector3 (0, GlobalInfo.DISTANCE_SYMBOL * i, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
