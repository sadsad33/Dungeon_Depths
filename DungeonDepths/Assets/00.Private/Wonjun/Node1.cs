//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Node1 : Stage1
//{
//    void Start()
//    {

//        spawnPoints = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

//        newNode1.isClear = false;
//        if (spawnPoints.Length > 0)
//        {
//            //���� ���� �ڷ�ƾ �Լ� ȣ��
//            StartCoroutine(this.CreateMonster());
//        }

//    }

//    IEnumerator CreateMonster()
//    {
//        //���� ���� �ñ��� ���� ����
//        while (!newNode1.isClear)
//        {
//            //���� ������ ���� ���� ����
//            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;
//            if (monsterCount < maxMonster)
//            {
//                //������ ���� �ֱ� �ð���ŭ ���
//                yield return new WaitForSeconds(createTime);

//                //�ұ�Ģ���� ��ġ ����
//                int idx = Random.Range(1, spawnPoints.Length);
//                //������ ���� ����
//                Instantiate(monsterPrefab, spawnPoints[idx].position, spawnPoints[idx].rotation);
//            }
//            else
//            {
//                yield return null;
//            }
//        }
//    }

//    void Update()
//    {
      
//    }
//    public void Clear()
//    {
//        newNode1.isClear = true;
//        CallPortal();
//    }

//    public void call()
//   {

//    }

//}
