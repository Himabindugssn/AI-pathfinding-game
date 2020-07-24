using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Heap<T> where T : I_heap_item<T>
{
    T[] items;
    int cur_item_count=0;
    public Heap(int max_heap_size){
        items=new T[max_heap_size];
    }
    public void Add(T item){
        item.Heap_index=cur_item_count;
        items[cur_item_count]=item;
        sort_up(item);
        cur_item_count++;
    }
    public void update_item(T item){
        sort_up(item);
    }

    public T top(){
        T top_item=items[0];
        cur_item_count--;
        items[0]=items[cur_item_count];
        items[0].Heap_index=0;
        sort_down(items[0]);
        return top_item;
    }
    public int Count(){
       return cur_item_count;
    }
    public bool Contains(T item){
        return Equals(items[item.Heap_index],item);
    }
    public void sort_down(T item){
        while(true){
            int left_child_index=item.Heap_index * 2+1;
            int right_child_index=item.Heap_index * 2+2;
            int swap_index=0;

            if(left_child_index<cur_item_count){
                swap_index=left_child_index;
                if(right_child_index<cur_item_count){
                    if(items[left_child_index].CompareTo(items[right_child_index])<0){
                        swap_index=right_child_index;
                    }
                }
                if(item.CompareTo(items[swap_index])<0){
                    swap(item,items[swap_index]);
                }
                else{
                    return;
                }
            }
            else{
                return;
            }
        }
    }
    public void sort_up(T item){
        int parent_index=(item.Heap_index - 1)/2;
        while(true){
            T parent_item=items[parent_index];
            //compare the priority (in this case, fcost is the priority)
            if(item.CompareTo(parent_item)>0){
                swap(item,parent_item);
            }
            else{
                break;
            }
            parent_index=(item.Heap_index - 1)/2;
        
        }
    }
    public void swap(T item1, T item2){
        items[item1.Heap_index] =item2;
        items[item2.Heap_index] =item1;
        int temp=item2.Heap_index;
        item2.Heap_index=(item1.Heap_index);
        item1.Heap_index=(temp);
    }
}
public interface I_heap_item<T>: IComparable<T>{
    int Heap_index { get; set;}

}
