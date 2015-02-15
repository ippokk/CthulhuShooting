using UnityEngine;
using System.Collections;

public class Stage_01 : MonoBehaviour {

	public Enemy enemy_01;
	public int counter;

	GameObject enemy_plant;
//	GameObject enemy_bullets;

	void Start () {
		counter = 0;
		Application.targetFrameRate = 60;
		enemy_plant = new GameObject ("Emmiter");
		enemy_plant.transform.parent = transform;
//		enemy_bullets = new GameObject ("Bullets");
//		enemy_bullets.transform.parent = transform;
	}
	
	void Update () {
		Emmiter ();
		counter++;
	}

	void Emmiter () {
		if (counter % 200 == 0) {
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.0375f, 0.05f));
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.68f, 0.72f));
			Enemy enemy_clone = Instantiate (enemy_01, new Vector3(Random.Range(min.x, max.x), max.y, 100.0f), transform.rotation) as Enemy;
			enemy_clone.transform.parent = enemy_plant.transform;
			enemy_clone.life = 10;
			enemy_clone.rigidbody.AddForce(new Vector3(0, -5000, 0));
		}
	}
}
