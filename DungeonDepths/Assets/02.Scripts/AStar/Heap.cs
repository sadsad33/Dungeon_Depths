using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 최단 경로를 빨리 찾기위해 힙 자료구조를 배열로 구현해 사용한다.
// 자식노드의 값은 부모노드보다 커야한다.
// 최소값을 가진 노드는 뿌리노드이다.
// 부모노드의 인덱스가 k라면 자식노드의 인덱스는 2k+1 과 2k+2 이다.
// 노드 삽입 연산 : 새로 삽입한 노드에서부터 트리를 거슬러 올라가며 힙 순서 속성을 만족시킨다.
// 최소값 제거 연산 : 가장 뒤에 있던 노드를 뿌리 노드로 옮겨온다음 아래로 내려가며 힙 순서 속성을 만족시킨다.
// + 힙에 특정 항목이 있는지 없는지 결과를 출력하는 함수
// + 힙의 사이즈를 출력하는 함수
public class Heap<T> where T : IHeapItem<T> {
    T[] items;
    int currentItemCount;

    public Heap(int maxSize) {
        items = new T[maxSize];
    }

    public void Add(T item) {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public T RemoveFirst() {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }
    public void UpdateItem(T item) {
        SortUp(item);
    }

    public int Count {
        get { return currentItemCount; }
    }
    public bool Contains(T item) {
        return Equals(items[item.HeapIndex], item);
    }
    void SortDown(T item) {
        while (true) {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount) {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount) {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                        swapIndex = childIndexRight;
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                    Swap(item, items[swapIndex]);
                else
                    return;
            }
            else return;
        }
    }

    void SortUp(T item) {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true) {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else
                break;

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB) {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int temp = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = temp;
    }
}

public interface IHeapItem<T> : IComparable<T> {
    int HeapIndex {
        get; set;
    }
}