using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyNumberGenerator : NumberGenerator {

	void Start () {
		Random.InitState (System.DateTime.Now.DayOfYear);
	}
}
