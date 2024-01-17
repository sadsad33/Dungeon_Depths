using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִܰ�� Ž�� �˰��� ����� ��� Ŭ����
public class Node : IHeapItem<Node> {
    public bool isWalkable;
    public Vector3 worldPos;
    public int gridX, gridY;
    public int movementPenalty;

    public int gCost, hCost;
    public Node parentNode;
    int heapIndex;

    public Node(bool _isWalkable, Vector3 _worldPos, int _gridX, int _gridY, int _movementPenalty) {
        isWalkable = _isWalkable;
        worldPos = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _movementPenalty;
    }

    public int fCost {
        get { return gCost + hCost; }
    }

    public int HeapIndex {
        get { return heapIndex; }
        set { heapIndex = value; }
    }


    /* ��� ��.CompareTo(�� ��) �� ���
     * ��� �� < �� ���� ��� -1
     * ��� �� == �� ���� ��� 0
     * ��� �� > �� ���� ��� 1
     */
    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
            compare = hCost.CompareTo(nodeToCompare.hCost);
        return -compare;
    }
}