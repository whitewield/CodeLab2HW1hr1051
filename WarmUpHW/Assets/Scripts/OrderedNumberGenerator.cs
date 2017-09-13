using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedNumberGenerator : NumberGenerator {

	public int[] order;
	int current = 0;

	public override int Next () {
		int result = order [current];
		current++;
		current %= order.Length;
		return result;
	}
}
