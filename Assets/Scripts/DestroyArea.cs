using UnityEngine;

public class DestroyArea : MonoBehaviour
{
	void OnTriggerExit (Collider c){
		Debug.Log ("out");
		Destroy (c.gameObject);
	}
}