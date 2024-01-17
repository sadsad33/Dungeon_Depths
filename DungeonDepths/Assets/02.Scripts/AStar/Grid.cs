using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    //public Transform playerPos;
    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    Node[,] grid;

    float nodeDiameter, correctionX, correctionY;
    int gridSizeX;
    int gridSizeY;

    void Awake() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        correctionX = gridSizeX / 2 - transform.position.x;
        correctionY = gridSizeY / 2 - transform.position.z;
        CreateGrid();
    }
    public int MaxSize {
        get { return gridSizeX * gridSizeY; }
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        //Debug.Log("맵 좌측최하단 좌표 : " + worldBottomLeft);
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y, 0);
            }
        }
        SetPenalty(grid);
    }

    // 보스의 면적이 넓기 때문에 장애물에 밀려 이동하지 못하는 경우를 방지하기 위해 장애물 주변에 패널티를 부여한다.
    void SetPenalty(Node[,] grid) {
        for (int x = 0; x < 100; x++) {
            for (int y = 0; y < 100; y++) {
                if (!grid[x, y].isWalkable) {

                    for (int i = -3; i <= 3; i++) {
                        for (int j = -3; j <= 3; j++) {
                            if (i == 0 && j == 0) continue;
                            int checkX = x + i;
                            int checkY = y + j;

                            if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                                grid[checkX, checkY].movementPenalty = 100;
                            }
                        }
                    }
                }
            }
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        // 주변 8방향을 탐색한다.
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }
        return neighbours;
    }

    // 맵의 월드 좌표가 (-500, 0, -500)
    // 이 경우 백분율을 구해 0과 1사이의 값으로 clamp하는것이 의미가 없다.

    //월드 좌표로부터 그리드 좌표를 구한다.
    public Node GetNodeFromWorldPoint(Vector3 worldPosition) {
        //월드좌표의 중심 (0, 0) 을 그리드 좌표의 중심(gridWorldSize.x / 2, gridWorldSize.y / 2) 과 맞춰주기 위해 x좌표와 y좌표에 값을 더한다.
        //이후 그리드의 크기로 나눠 백분율로 변환한다.
        float percentX = (worldPosition.x + correctionX) / gridWorldSize.x;
        float percentY = (worldPosition.z + correctionY) / gridWorldSize.y;

        //Debug.Log("백분율 : " + percentX + ", " + percentY);
        // x 좌표와 y좌표를 0과 1사이의 값으로 만든다.
        // 그리드 내의 좌표를 제외하고 전부 제외할 수 있다.
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //Debug.Log("0~1 : " + percentX + ", " + percentY);

        // 인덱스가 0부터 시작하므로 1을 빼줌
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> path;
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null && displayGridGizmos) {
            foreach (Node n in grid) {
                Gizmos.color = (n.isWalkable) ? Color.white : Color.red;
                if(path!= null) {
                    if (path.Contains(n)) Gizmos.color = Color.black;
                }
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
