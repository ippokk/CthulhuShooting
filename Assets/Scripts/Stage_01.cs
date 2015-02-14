using UnityEngine;
using System.Collections;

public class Stage_01 : MonoBehaviour {

	public GameObject enemy_01;
	public int counter;

	void Start () {
		Application.targetFrameRate = 60;
		counter = 0;
	}
	
	void Update () {
		Emmiter ();
		counter++;
	}

	void Emmiter () {
		if (counter % 200 == 0) {
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
			GameObject enemy = Instantiate (enemy_01, new Vector2(Random.Range(min.x, max.x), max.y), transform.rotation) as GameObject;
			enemy.rigidbody2D.velocity = new Vector2(0, -200);
		}

	}
}
