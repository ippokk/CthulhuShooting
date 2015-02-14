using UnityEngine;
using System.Collections;

public class Stage_01 : MonoBehaviour {

	public Enemy enemy_01;
	public int counter;

	GameObject enemy_plant;

	void Start () {
		counter = 0;
		Application.targetFrameRate = 60;
		enemy_plant = new GameObject ("Emmiter");
		enemy_plant.transform.parent = transform;
	}
	
	void Update () {
		Emmiter ();
		counter++;
	}

	void Emmiter () {
		if (counter % 200 == 0) {
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
			Enemy enemy_clone = Instantiate (enemy_01, new Vector2(Random.Range(min.x, max.x), max.y), transform.rotation) as Enemy;
			enemy_clone.transform.parent = enemy_plant.transform;
			enemy_clone.life = 10;
			enemy_clone.rigidbody2D.velocity = new Vector2(0, -200);
		}

	}
}
