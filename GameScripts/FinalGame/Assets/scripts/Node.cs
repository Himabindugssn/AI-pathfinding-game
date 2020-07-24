using UnityEngine;
using System.Collections;
using System;
public class Node:I_heap_item<Node> 
{
    public bool walkable;//layer mask - walkable node or not
    public Vector3 worldPosition;//position vector in the plane 
    public double g_cost;//movement cost 
    public double h_cost;//heuristic cost
    public int x;// x coordinate of node
    public int y;//y coordinate of node 
    public Node parent; //parent of the node (in a path)

    //public int Heap_index { get => ((I_heap_item<Node>)parent).Heap_index; set => ((I_heap_item<Node>)parent).Heap_index = value; }

    public Node(bool walk, Vector3 position_vector, int x_temp, int y_temp){
        walkable=walk; //whether the node is walkable or not
        worldPosition=position_vector; //position of node in the plane
        x=x_temp;
        y=y_temp;
    }
    public double f_cost 
    //total cost
    {
        get{
            return g_cost+h_cost;
        }
    }
    private int heap_index;

    public int Heap_index 
    // heap index of each node
    {
        get{
            return heap_index;
        }
        set
        {
            this.heap_index = value;
        }
    }

    public int CompareTo(Node a) 
    //to compare the f cost of two nodes and return the one with lower f cost
    {
        int compare=f_cost.CompareTo(a.f_cost);
        if(compare==0){
            compare=h_cost.CompareTo(a.h_cost);
        }
        return -compare; // higher fcost => lower priority
    }

}
