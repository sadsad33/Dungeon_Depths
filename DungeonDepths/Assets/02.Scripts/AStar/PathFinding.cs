using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {
    PathRequestManager requestManager;
    public Grid grid;

    void OnEnable() {
        requestManager = GetComponent<PathRequestManager>();
        grid = GameObject.Find("GridMaker").GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {

        Vector3[] waypoints = new Vector3[0];
        bool pathFound = false;
        //Debug.Log("시작 노드 :");
        Node startNode = grid.GetNodeFromWorldPoint(startPos);
        //Debug.Log("도착 노드 :");
        Node targetNode = grid.GetNodeFromWorldPoint(targetPos);
        
        if (startNode.isWalkable && targetNode.isWalkable) {
            Heap<Node> openList = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedList = new HashSet<Node>();
            openList.Add(startNode);

            while (openList.Count > 0) {
                Node currentNode = openList.RemoveFirst();
                closedList.Add(currentNode);

                if (currentNode == targetNode) {
                    pathFound = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
                    if (!neighbour.isWalkable || closedList.Contains(neighbour)) continue;

                    int newCurrentToNeighbourCost = currentNode.gCost + GetDistanceCost(currentNode, neighbour) + neighbour.movementPenalty;
                    if (newCurrentToNeighbourCost < neighbour.gCost || !openList.Contains(neighbour)) {
                        neighbour.gCost = newCurrentToNeighbourCost;
                        neighbour.hCost = GetDistanceCost(neighbour, targetNode);
                        neighbour.parentNode = currentNode;

                        if (!openList.Contains(neighbour)) openList.Add(neighbour);
                        else openList.UpdateItem(neighbour);
                    }
                }
            }
        }
        yield return null;
        if (pathFound) {
            waypoints = RetracePath(startNode, targetNode);
            //Debug.Log("경로 길이 : " + waypoints.Length);
        }
        requestManager.FinishedProcessingPath(waypoints, pathFound);
    }

    Vector3[] RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        grid.path = path;
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++) {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
                waypoints.Add(path[i].worldPos);
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistanceCost(Node nodeA, Node nodeB) {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY) return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }
}
