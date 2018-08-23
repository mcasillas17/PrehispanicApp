using UnityEngine;
using System.Collections;

public class MoveCharacterController : MonoBehaviour {

	public Transform[] targets;
	public float speed;
	private Transform target;

	bool isBeenDragged = false;

	void Start () {
		int n = Random.Range (0, 20);
		target = targets [n%2];
		Vector3 temp = new Vector3(target.position.x,target.position.y,transform.position.z);
		temp.z = transform.position.z;
		target.position = temp;
	}

	void Update () {
		if (!isBeenDragged) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);
		}
	}

	void OnMouseDown(){
		isBeenDragged = true;
	}

	void OnMouseUp(){
		isBeenDragged = false;
	}
}
