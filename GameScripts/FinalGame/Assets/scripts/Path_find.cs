using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Path_find : MonoBehaviour {
	
	Multiple_path request_manage;
	Grid_mesh grid_mesh;
	
	void Awake() {
        grid_mesh = GetComponent<Grid_mesh>();
        request_manage = GetComponent<Multiple_path>();
	}
	/*void Update(){
		if(Input.GetButtonDown("Jump")){
			request_manage = GetComponent<Multiple_path>();
		}
	}*/
	
	public void Start_find_path(Vector3 start_pos, Vector3 end_pos) {
		StartCoroutine(Get_path(start_pos,end_pos));
	}
	
	IEnumerator Get_path(Vector3 start_pos, Vector3 end_pos) {

		Vector3[] waypoints = new Vector3[0];
		bool path_success = false;
		
		Node start_node = grid_mesh.node_from_position_vector_rover(start_pos);
		Node end_node = grid_mesh.node_from_position_vector_rover(end_pos);
		
		
		if (start_node.walkable && end_node.walkable) {
			Heap<Node> open_set = new Heap<Node>(grid_mesh.max_heap_size);
			HashSet<Node> closed_set = new HashSet<Node>();
			open_set.Add(start_node);
			
			while (open_set.Count() > 0) {
				Node cur_node = open_set.top();
				closed_set.Add(cur_node);
				
				if (cur_node == end_node) {
					path_success = true;
					break;
				}
				
				foreach (Node neighbour in grid_mesh.extract_neighbours(cur_node)) {
					if (!neighbour.walkable || closed_set.Contains(neighbour)) {
						continue;
					}
					
					double cur_node_to_neigbour_cost = cur_node.g_cost + cost(cur_node, neighbour);

					//if the node is not explored or the node has less cost 
					if (cur_node_to_neigbour_cost< neighbour.g_cost || !open_set.Contains(neighbour)) {
						neighbour.g_cost = cur_node_to_neigbour_cost;
						neighbour.h_cost = distance_between(cur_node,neighbour,end_node);
						neighbour.parent = cur_node;
						
						//if the open set doesn't contain the node, then add the explored node into the list
						if (!open_set.Contains(neighbour))
							open_set.Add(neighbour);
					}
				}
			}
		}
		yield return null;

		//if there was any path, then return the path
		if (path_success) {
			waypoints = Retrace_path(start_node,end_node);
		}
		request_manage.Finished_processing_path(waypoints,path_success);
		
	}
	double cost(Node node_cur, Node node_dest){
		int dx_1=Mathf.Abs(node_cur.x-node_dest.x);
        int dy_1=Mathf.Abs(node_cur.y-node_dest.y);
		return (14*(Mathf.Max(dx_1,dy_1)+ 10*(Mathf.Abs(dx_1-dy_1))));
	}
	Vector3[] Retrace_path(Node start_node, Node end_node) {
		List<Node> path = new List<Node>();
		Node cur_node = end_node;
		
		while (cur_node != start_node) {
			path.Add(cur_node);
			cur_node = cur_node.parent;
		}
		Vector3[] waypoints = Simplify_path(path);
		Array.Reverse(waypoints);
		return waypoints;
		
	}
	
	Vector3[] Simplify_path(List<Node> path) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 prev_direc = Vector2.zero;
		
		for (int i = 1; i < path.Count; i ++) {
			Vector2 cur_direc = new Vector2(path[i-1].x - path[i].x,path[i-1].y - path[i].y);
			if (cur_direc != prev_direc) {
				waypoints.Add(path[i].worldPosition);
			}
			prev_direc= cur_direc;
		}
		return waypoints.ToArray();
	}
	
	double distance_between(Node node_cur ,Node node_dest, Node node_src){

        //diagonal moves = min(nodes on x axis, nodes on y axis)

        //horizontal/ vertical moves = min(x,y)

        //taking diagonal cost as 14 and horizontal or vertical cost as 10(it takes time to rotate the wheels and change direction)
        int dx_1=Mathf.Abs(node_cur.x-node_dest.x);
        int dy_1=Mathf.Abs(node_cur.y-node_dest.y);
        int dx_2= Mathf.Abs(node_src.x-node_dest.x);
        int dy_2=Mathf.Abs(node_src.y-node_dest.y);

        double tie= Mathf.Abs(dx_1*dy_2- dy_1*dx_2);//to break the tie p
        return Math.Abs((tie*(14*(Mathf.Max(dx_1,dy_1)+ 10*(Mathf.Abs(dx_1-dy_1))))));

        //return -(dx_1+dy_1)+(1.41-2)*(dx_1-dy_1);
		//return tie*((dx_1+dy_1)+(Math.Sqrt(2)-2)*Mathf.Min(dx_1,dy_1));
    }
	
	
}