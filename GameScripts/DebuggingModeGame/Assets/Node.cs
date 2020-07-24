using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node:I_heap_item<Node> 
{
    public bool walkable;
    public Vector3 worldPosition;
    public double g_cost;
    public double h_cost;
    public int x;
    public int y;
    public Node parent;

    //public int Heap_index { get => ((I_heap_item<Node>)parent).Heap_index; set => ((I_heap_item<Node>)parent).Heap_index = value; }

    public Node(bool walk, Vector3 position_vector, int x_, int y_){
        walkable=walk; //whether the node is walkable or not
        worldPosition=position_vector; //position of node in the plane
        x=x_;
        y=y_;
    }
    //int heap_index;
    
    public double get_f_cost(){
        return g_cost+h_cost;
    }
    private int heap_index;

    public int Heap_index{
        get{
            return heap_index;
        }
        set
        {
            this.heap_index = value;
        }
    }

    public int CompareTo(Node a){
        int compare=get_f_cost().CompareTo(a.get_f_cost());
        if(compare==0){
            compare=h_cost.CompareTo(a.h_cost);
        }
        return -compare; // higher fcost => lower priority
    }

}
