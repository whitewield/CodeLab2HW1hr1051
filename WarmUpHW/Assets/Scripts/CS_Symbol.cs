using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CS_Symbol : MonoBehaviour {
	[SerializeField] Symbol mySymbol;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Symbol GetSymbol () {
		return mySymbol;
	}
}
