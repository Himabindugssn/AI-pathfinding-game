using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Multiple_path : MonoBehaviour {

	Queue<Path_request> path_request_queue = new Queue<Path_request>();
	Path_request cur_path_request;

	static Multiple_path instance;
	Path_find pathfinding;

	bool is_processing_path;

	void Awake() {
		instance = this;
		pathfinding = GetComponent<Path_find>();
	}

	public static void Request_path(Vector3 path_start, Vector3 path_end, Action<Vector3[], bool> path_found) {
		Path_request new_request = new Path_request(path_start,path_end,path_found);
		instance.path_request_queue.Enqueue(new_request);
		instance.next_process();
	}

	void next_process() {
		if (!is_processing_path && path_request_queue.Count > 0) {
			cur_path_request = path_request_queue.Dequeue();
			is_processing_path = true;
			pathfinding.Start_find_path(cur_path_request.path_start, cur_path_request.path_end);
		}
	}

	public void Finished_processing_path(Vector3[] path, bool success) {
		cur_path_request.path_found(path,success);
		is_processing_path = false;
		next_process();
	}

	struct Path_request {
		public Vector3 path_start;
		public Vector3 path_end;
		public Action<Vector3[], bool> path_found;

		public Path_request(Vector3 _start, Vector3 _end, Action<Vector3[], bool> yes_no) {
			path_start = _start;
			path_end = _end;
			path_found = yes_no;
		}

	}
}
