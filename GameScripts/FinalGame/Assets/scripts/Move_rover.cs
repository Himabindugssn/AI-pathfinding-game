using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
public class Move_rover: MonoBehaviour {

     
     // flag to determine if we are waiting for uset input to start game
    private bool waitingToStartGame = true;
	public Transform target;
	float speed = 3;//speed of the rover in the animation 
	Vector3[] path;
	int targetIndex;
	NavMeshAgent navMeshAgent;
	void Start(){
		//navMeshAgent = GetComponent<NavMeshAgent> ();
	}
    
	void Update() {
		
		float horizontalInput = Input.GetAxis("Horizontal");
		//Get the value of the Horizontal input axis.

		float verticalInput = Input.GetAxis("Vertical");
		//Get the value of the Vertical input axis.

		transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime);
		//Move the object to XYZ coordinates defined as horizontalInput, 0, and verticalInput respectively.

		if (Input.GetKeyDown("s")){
			Multiple_path.Request_path(transform.position,target.position,path_found);
			//Multiple_path.Request_path(navMeshAgent.transform.position,target.position,path_found);
		}
	}

	public void path_found(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;

			StopCoroutine("Follow_path");
			StartCoroutine("Follow_path");
		}
	}

	IEnumerator Follow_path() {
		Vector3 cur_waypoint = path[0];
		while (true) {
			if (transform.position == cur_waypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				cur_waypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position,cur_waypoint,speed * Time.deltaTime);
			yield return null;

		}
	}

//for editor mode - draw and debugging purpose
	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}


}
