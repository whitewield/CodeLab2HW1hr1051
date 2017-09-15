using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_PatternManager : MonoBehaviour {
	[SerializeField] int myRandomSeed = -1;
	[SerializeField] GameObject[] mySymbolPrefabs;
	private List<Transform> myPattern = new List<Transform> ();
	// Use this for initialization
	void Start () {
//		CreatePattern ();
	}

	public void SetRandomSeed (int g_seed) {
		if (g_seed >= GlobalInfo.NUMBER_MAX_SEED) {
			myRandomSeed = -1;
		} else {
			myRandomSeed = g_seed;
		}
	}

	public void CreatePattern (int g_size) {
		if (myRandomSeed == -1) {
			myRandomSeed = Random.Range (0, GlobalInfo.NUMBER_MAX_SEED);
		}

		Random.InitState (myRandomSeed);

		for (int i = 0; i < g_size; i++) {
			GameObject t_symbol = Instantiate (mySymbolPrefabs [Random.Range (0, 4)], this.transform) as GameObject;
			t_symbol.transform.localPosition = new Vector3 (0, GlobalInfo.DISTANCE_SYMBOL * i, 0);
			myPattern.Add (t_symbol.transform);
		}
	}

	public List<Transform> GetMyPattern () {
		return myPattern;
	}

	public Transform GetMySymbol (int g_index) {
		return myPattern [g_index];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
