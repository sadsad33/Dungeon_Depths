//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace MonsterManagerState
//{
//    #region Wait ���� : �÷��̾ ó�� ��������(���)�� ������ ����
//    class Wait : State<MonsterManager>
//    {
//        public override void Enter(MonsterManager m)
//        {
//            //Debug.Log("Enter");
//            MonsterManager.Instance.isGameClear = false; // ?
//            //m.isPlayerDead = false;

//        }
//        public override void Execute(MonsterManager m)
//        {

//        }
//        public override void Exit(MonsterManager m)
//        {

//        }
//    }
//    #endregion

//    #region Spawn ���� : �� ���� ����
//    class Spawn : State<MonsterManager>
//    {

//        public override void Enter(MonsterManager m)
//        {
//            SpawnAtStart(m);

//        }

//        private static void SpawnAtStart(MonsterManager m)
//        {
//            for (int i = 0; i < m.curMapMonsterCount; i++)
//            {
//                var newMonster = PoolManager.Instance.Instantiate("Enemy", m.enemySpawnPoints[i], Quaternion.identity);

//            }
//        }

//        public override void Execute(MonsterManager m)
//        {
//            int iter = m.deadSpots.Count; // ���Ͱ� ���� �� ��ŭ �ݺ��� ��
//            for (int i = 0; i < iter; i++)
//            {
//                var newMonster = PoolManager.Instance.Instantiate("Enemy", m.deadSpots[0], Quaternion.identity);
//                m.deadSpots.Remove(m.deadSpots[0]); // ���͸� ���� �ҷ������Ƿ� �ϳ��� ����
//                m.meleeMonsters.Add(newMonster);
//            }
//            m.isRespawn = false;
//        }

//        public override void Exit(MonsterManager m)
//        {

//        }
//        #endregion
//    }

//    #region Finish ���� : ��������(���)Ŭ���� Ȥ�� �÷��̾� ���
//    class Finish : State<MonsterManager>
//    {
//        public override void Enter(MonsterManager m)
//        {
//            foreach (GameObject monster in m.monsterList)
//            {
//                monster.SetActive(false);
//            }
//            m.meleeMonsters.Clear();
//            //foreach(GameObject monster in m.rangeMonsters) {
//            //    monster.SetActive(false);
//        }
//        //m.rangeMonsters.Clear();
//    }
//    public override void Execute(MonsterManager m)
//    {
//    }
//    public override void Exit(MonsterManager m)
//    {

//    }
//}
//    #endregion
//}