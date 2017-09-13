using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	
	[SerializeField] Sprite[] sprites;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpriteTime", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SpriteTime () {
		int t_nextNum = GetComponent<NumberGenerator> ().Next ();
		MakeSprite (t_nextNum);
	}

	private void MakeSprite (int num) {
		GameObject t_gameObject = new GameObject ();
		t_gameObject.AddComponent<SpriteRenderer> ().sprite = sprites [num];
		t_gameObject.AddComponent<Rigidbody2D> ();
	}
}
