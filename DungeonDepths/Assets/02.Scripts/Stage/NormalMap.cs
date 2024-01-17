using System.Collections.Generic;
using UnityEngine;

public class NormalMap : Map
{
    [SerializeField]
    private MapCore core;
    public List<Vector3> boxSpawnPoints = new List<Vector3>();    // ���� ���� ���� Points
    public List<Vector3> EnemySpawnPoints = new List<Vector3>();  // ���� ���� Points

    public MapCore Core     //�ھ�
    {
        get;
        private set;
    }

    public List<Vector3> GetWorldSpawnPoints()
    {
        List<Vector3> _pointList = new List<Vector3>();
        foreach (var _point in EnemySpawnPoints)
        {
            _pointList.Add(transform.TransformPoint(_point));
        }

        return _pointList;
    }

    public List<Vector3> GetWorldBoxSpawnPoints()
    {
        List<Vector3> _pointList = new List<Vector3>();
        foreach (var _point in boxSpawnPoints)
        {
            _pointList.Add(transform.TransformPoint(_point));
        }

        return _pointList;
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //editor
        for (int i = 0; i < EnemySpawnPoints.Count; i++) // �� ���� ����Ʈ �����
        {
            Gizmos.DrawSphere(transform.TransformPoint(EnemySpawnPoints[i]), 0.5f);
        }

        Gizmos.color = Color.yellow;
        for (int i = 0; i < boxSpawnPoints.Count; i++) // �������� �����
        {
            Gizmos.DrawSphere(transform.TransformPoint(boxSpawnPoints[i]), 0.5f);
        }
    }
    public override void Awake()
    {
        base.Awake();
        core = GetComponentInChildren<MapCore>();
        /* TODO ����
         * ����, ���� ���� ��ġ ���� �� �Ľ�
         * ���� ��ġ ���� �� �Ľ�
         * core �ı� �̺�Ʈ ����
        */
        //core.OnEvent += () => { IsClear = true; Debug.Log("Map �̺�Ʈ ȣ�� : " + IsClear); };
    }
    // TODO ���� ���� ���� �Լ� ����?

}
