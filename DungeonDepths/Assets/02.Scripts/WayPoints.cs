using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class WayPoints : MonoBehaviour
{
    //local Point -> world Point
    public List<Vector3> localList = new List<Vector3>(); // �ν�����â���� ������ ��ǥ
    List<Vector3> worldList = new List<Vector3>();        // �ν����� â���� ������ ��ǥ�� ����� ������Ű�� ���� ����Ʈ
    int index = -1;
    public Vector3 destinationPoint; // ��ǥ ���ް�

    public void SetWayPoints()
    {
        worldList.Clear();

        if (Random.Range(0f, 1f) <= 0.5f) // 50����Ȯ���� ���⼳��   
        {
            for (int i = 0; i < localList.Count; i++) //0 ���� ����Ʈ���� �̵�
            {
                worldList.Add(transform.TransformPoint(localList[i]));
                //���� ��ǥ�� ������ ����Ʈ�� ������ǥ�� �����ؼ� ����
            }
        }
        else
        {
            for (int i = localList.Count - 1; i >= 0; i--) //������ ����Ʈ���� �Ųٷ� �̵�
            {
                worldList.Add(transform.TransformPoint(localList[i]));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (Application.isPlaying)
        {
            for (int i = 0; i < worldList.Count - 1; i++)
            {
                Gizmos.DrawLine(worldList[i], worldList[i + 1]);
            }
        }
        else
        {
            //editor
            for (int i = 0; i < localList.Count - 1; i++)
            {
                Gizmos.DrawLine(
                    transform.TransformPoint(localList[i]),
                    transform.TransformPoint(localList[i + 1]));
            }
            for (int i = 0; i < localList.Count; i++)
            {
                Gizmos.DrawSphere(transform.TransformPoint(localList[i]), 0.5f);
            }
        }
    }
    public void MoveNextPoint()
    {
        index = (index + 1) % worldList.Count;
        destinationPoint = worldList[index];
    }

    public bool CheckDestination(float _stopDistance, Vector3 _targetPos) // ���������˻�
	{
        float _stopDistanceDouble = _stopDistance * _stopDistance;
        Vector3 _dir = destinationPoint - _targetPos;
		if (_dir.sqrMagnitude < _stopDistanceDouble)
		{
            return true;
		}
        return false;
	}

}