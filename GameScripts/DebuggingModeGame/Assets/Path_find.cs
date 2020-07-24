using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;
public class Path_find : MonoBehaviour
{
    
    public Transform rover, destination;
    //public Transform way;
    Grid_mesh grid_mesh; 
    Heap<Node> open_set; 
    void Awake(){
        // initialize the gameobject - grid mesh
        grid_mesh= GetComponent<Grid_mesh>();
    }
    void Update(){ 
        //if(Input.GetButtonDown("Jump")){
        get_path(rover.position,destination.position);
        //}
    }
    double distance_between(Node node_cur ,Node node_dest, Node node_src){

        //diagonal moves = min(nodes on x axis, nodes on y axis)

        //horizontal/ vertical moves = min(x,y)

        //taking diagonal cost as 14 and horizontal or vertical cost as 10(it takes time to rotate the wheels and change direction)
        int dx_1=Mathf.Abs(node_cur.x-node_dest.x);// dx1
        int dy_1=Mathf.Abs(node_cur.y-node_dest.y);// dy1
        int dx_2= Mathf.Abs(node_src.x-node_dest.x);
        int dy_2=Mathf.Abs(node_src.y-node_dest.y);

        double tie= Mathf.Abs(dx_1*dy_2- dy_1*dx_2);
        //return tie*(14*(Mathf.Min(dx_1,dy_1)- 10*(Mathf.Max(dx_1,dy_1))));
        //return -(dx_1+dy_1)+(1.41-2)*(dx_1-dy_1);
        //return ((dx_1+dy_1)+(Mathf.Sqrt(2)-2)*Mathf.Min(dx_1,dy_1));
        return tie*(14*(Mathf.Max(dx_1,dy_1)+ 10*(Mathf.Abs(dx_1-dy_1))));

    }
    double cost_dist(Node node_cur, Node node_dest){
        int dx_1=Mathf.Abs(node_cur.x-node_dest.x);// dx1
        int dy_1=Mathf.Abs(node_cur.y-node_dest.y);// dy1
        return (14*(Mathf.Max(dx_1,dy_1)+ 10*(Mathf.Abs(dx_1-dy_1))));
    }
/*
dx1 = current.x - goal.x
dy1 = current.y - goal.y
dx2 = start.x - goal.x
dy2 = start.y - goal.y
cross = abs(dx1*dy2 - dx2*dy1)
heuristic += cross*0.001
*/
    void retrace(Node start_node, Node end_node){
        List<Node>path = new List<Node>();
        Node temp_node= end_node;

        while(temp_node!=start_node){
            path.Add(temp_node);
            temp_node= temp_node.parent;
        }
        //reverse the path as currently path has "end to start node"
        path.Reverse();

        grid_mesh.path=path;
    }
    public void get_path(Vector3 src_pos_vector, Vector3 dest_pos_vector){
        Stopwatch sw= new Stopwatch();
        sw.Start();
        
        //obtain nodes from the positions of source, destination
        Node src_node= grid_mesh.node_from_position_vector_rover(src_pos_vector);
        Node dest_node= grid_mesh.node_from_position_vector_rover(dest_pos_vector);

        int heap_size=grid_mesh.max_heap_size();
        //list of "yet to be visited" nodes
        //Heap<Node> 
        open_set= new Heap<Node>(heap_size);
        
        // set having explored nodes
        HashSet<Node> closed_set= new HashSet<Node>();
        open_set.Add(src_node);
        
        //iterate till the open set is not empty
        while(open_set.Count()>0)
        {
            Node cur_node= open_set.top();
            //Node cur_node= open_set[0];
            /*for(int i=1;i<open_set.Count; i++){
                // node with lower fcost would be traversed for getting shortest path
                if(open_set[i].get_f_cost()<cur_node.get_f_cost()){
                    cur_node=open_set[i];
                }
                //when cur node and node from open set have same f cost, the one closer to target (more h cost) is taken as cur node
                else if((open_set[i].get_f_cost()==cur_node.get_f_cost()) && (open_set[i].h_cost<cur_node.h_cost)){
                    cur_node=open_set[i];
                }
            }
            // remove the node as it has been explored
            open_set.Remove(cur_node);
        */
            // add it to the closed set as it has been explored
            closed_set.Add(cur_node);

            //if destination has been reached, exit the loop
            if (cur_node == dest_node)
            {
                sw.Stop();
                print("time taken to find path"+ sw.ElapsedMilliseconds+"ms");
                //retrace to get the path 
                retrace(src_node,dest_node);
            }

            //else check the adjacent nodes
            else{
                foreach (Node neighbour in grid_mesh.extract_neighbours(cur_node)){
                    // if node not walkable or has already been explored, skip it
                    if((!neighbour.walkable) || closed_set.Contains(neighbour))
                        continue;

                    // g cost = parent g cost + cost between node and it's parent move
                    double cur_node_to_neigbour_cost= cur_node.g_cost+ cost_dist(cur_node,neighbour);

                    if(cur_node_to_neigbour_cost< neighbour.g_cost || ! open_set.Contains(neighbour)){
                        neighbour.g_cost=cur_node_to_neigbour_cost;
                        neighbour.h_cost= distance_between(neighbour,dest_node,src_node);
                        neighbour.parent=cur_node;

                        //if the neighbour node isn't in open set, add to it
                        if(!open_set.Contains(neighbour)){
                            open_set.Add(neighbour);
                        }
                        
                    }   
                }
            } 
        } 
    }

}



//return tie+(14*(Mathf.Max(dx_1,dy_1)+ 10*(Mathf.Abs(dx_1-dy_1))));
        //return tie+(dx_1+dy_1)-(1.41-2)*Mathf.Min(dx_1,dy_1);
        //D * (dx + dy) + (D2 - 2 * D) * min(dx, dy)
        //Chebyshev distance D=1, D2=1*/
        //return (dist_x+dist_y)-Mathf.Min(dist_x,dist_y);
        
        //octile distance D=1, D2= sqrt(2)
        //return (dist_x+dist_y)+(1.41-2)*Mathf.Min(dist_x,dist_y);
