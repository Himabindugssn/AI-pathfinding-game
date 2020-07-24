using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid_mesh : MonoBehaviour {

	public bool displayGridGizmos; //used in editor mode for drawing 
	public LayerMask unwalk_mask; //it's like a bitmask, would adjust the area as walkable or not
	public Vector2 grid_dim; //dimensions (position vectors) of grid_mesh
	public float node_radius; //area covered by node if a circle encircles the node approximately
 
	Node[,] grid_mesh;// create a 2D array of nodes 
	int grid_x,grid_y;
	void Awake(){
        grid_x= Mathf.RoundToInt(grid_dim.x/(2*node_radius)); //no.of grids on x axis that can fit on given node radius
        grid_y=Mathf.RoundToInt(grid_dim.y/(2*node_radius));//no. of grids on y axis that can fit on given node radius
        generate_grid();
    }

	// max size of heap = number of nodes inside the grid made
	public int max_heap_size{
		get {
			return grid_x*grid_y;
		}
    }

	void generate_grid()
    {
        grid_mesh = new Node[grid_x,grid_y]; // a grid of nodes with above calculated no. of nodes


        Vector3 grid_bottom_left= transform.position- Vector3.right* (grid_dim.x/2)- Vector3.forward*(grid_dim.y/2);

        //check for nodes in grid - walkable or not
        for(int x=0; x<grid_x;x++){
            for(int y=0;y<grid_y;y++){

                //location vector for the node calculated using the bottom left of the grid for easier calculation
                Vector3 node_in_grid= grid_bottom_left+ Vector3.right*(x*node_radius*2+node_radius) +Vector3.forward*(y*node_radius*2+node_radius);
                
                //unwalable mask is passed onto the parameter unwalk_mask so the entire area which is walkable is marked as true
                bool isWalkable = !(Physics.CheckSphere(node_in_grid,node_radius,unwalk_mask));
                grid_mesh[x,y]= new Node(isWalkable,node_in_grid,x,y);
            }
        }

    }
	public List<Node> extract_neighbours(Node node){
        List<Node> nbrs= new List<Node>();// a list to store the neighbours

        //search the 8 adjacent nodes (3 x 3 matrix of nodes) in which the "node" is at centre
        /*int [] dir_row= {0,-1,-1,-1,0,1,1,1};
        int [] dir_col= {-1,-1,0,1,1,1,0,-1};
        */
        for(int x=-1;x< 2;x++){
            for(int y=-1;y< 2;y++){
                int neigh_x=node.x+x;
                int neigh_y=node.y+y;
				
				// it would be the node itself so we have to skip it
				//if (x == 0 && y == 0)
					//continue;

				//if the nodes are inside the grid mesh then add it to the neighbours list
                if(is_safe(neigh_x,neigh_y)){
                    nbrs.Add(grid_mesh[neigh_x,neigh_y]);
                }
            }
        }
        return nbrs;
    }
	// to check if the node is inside the grid mesh or not
    bool is_safe(int x,int y){
        return (x>=0 && x<grid_x && y>=0 && y<grid_y);
    }

	// this would give the node using the position vector of the rover 
    public Node node_from_position_vector_rover(Vector3 pos_vector){
        //to determine if it's on extreme left, or in middle or extreme right

        //gives what %  of the portion of node is it in.. right..mid..left ?
        float portion_x=(pos_vector.x+grid_dim.x/2)/grid_dim.x;
        
        //game is build in x-z axis so "z" coordinates would be used as "y coordinates"
        float portion_y=(pos_vector.z+grid_dim.y/2)/grid_dim.y;

        // clamp value between 0 and 1
        portion_x=Mathf.Clamp01(portion_x);
        portion_y=Mathf.Clamp01(portion_y);

        int x= Mathf.RoundToInt((grid_x-1)*portion_x);
        int y= Mathf.RoundToInt((grid_y-1)*portion_y);
        
        return grid_mesh[x,y];
    }
    //to check how the algo is working in editor mode
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(grid_dim.x,1,grid_dim.y));
		if (grid_mesh != null && displayGridGizmos) {
			foreach (Node n in grid_mesh) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (2*node_radius-.1f));
			}
		}
	}
	
}