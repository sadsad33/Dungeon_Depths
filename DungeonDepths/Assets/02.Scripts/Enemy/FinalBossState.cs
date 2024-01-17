using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalBossState
{
    #region ���� : ���
    class Idle : State<FinalBoss>
    {
        float stateDuration;
        float stateEnterTime;

        public override void Enter(FinalBoss f)
        {
            //Debug.Log("��� ���� ����");
            stateEnterTime = Time.time;
            f.animator.SetFloat("MoveSpeed", 0);
            if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.AttackIdle))
                stateDuration = 1.2f;
            else
                stateDuration = 3f;
            for(int i = 1; i <= 3; i++)
                f.animator.SetBool("Combo" + i, false);
        }
        public override void Execute(FinalBoss f)
        {
            //Debug.Log("��� ���� ��");
            if(Time.time - stateEnterTime < stateDuration) return;
            f.CheckTraceState();
        }
        public override void Exit(FinalBoss f)
        {
            //Debug.Log("��� ���� ����");
        }
    }
    #endregion

    #region �ļ�Ÿ ���� ����
    class AttackIdle : State<FinalBoss>
    {
        int comboIndex;
        float firstAtkTime, decisionTime;
        public override void Enter(FinalBoss f)
        {
            firstAtkTime = Time.time;
            if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.MeleeAttack1))
                decisionTime = 0.8f;
            //else if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.MeleeAttack2))
            else
                decisionTime = 0.5f;
        }
        public override void Execute(FinalBoss f)
        {
            // 0.7�� + @ ���� ������
            if(Time.time - firstAtkTime < decisionTime) return;

            //f.Rotation();

            // �޺� ������ �Ҽ� �ִ� �ð� ����, ������ �÷��̾ ��Ÿ� �ȿ� �ְ�
            // �̸� ������ �����ߴٸ�
            if(Time.time - firstAtkTime <= f.comboDuration && f.ShouldCombo(out comboIndex))
            {
                //Debug.Log("�޺� ���� : " + comboIndex);
                //f.animator.SetTrigger("Combo" + comboIndex);
                if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.MeleeAttack1))
                    f.animator.SetBool("Combo1", true);
                else if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.MeleeAttack2))
                    f.animator.SetBool("Combo2", true);
                else if(f.stateMachine.PreviousState == f.stateMachine.GetState((int)FinalBoss.FinalBossStates.MeleeAttack3))
                    f.animator.SetBool("Combo3", true);
                else
                    f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.Idle));
            }
            else
            {
                //Debug.Log("���� �޺� ���� ����");
                f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.Idle));
            }
        }
        public override void Exit(FinalBoss f)
        {

        }
    }
    #endregion
    class Trace : State<FinalBoss>
    {
        public override void Enter(FinalBoss f)
        {
            Debug.Log("�߰ݻ��� ����");
            f.animator.SetFloat("MoveSpeed", 9f);
        }
        public override void Execute(FinalBoss f)
        {
            //Debug.Log("�߰ݻ��� ����");
            f.Rotation();
            f.CheckAttackState();
            f.Trace();
        }
        public override void Exit(FinalBoss f)
        {
            //Debug.Log("�߰ݻ��� ����");
        }
    }
    /// <summary>
    /// ������ ������ ��� ���Ӱ��� ����
    /// �÷��̾ ��Ÿ����� �ִٰ� �ǴܵǸ� ������ ���� ��
    /// �ٽ� �÷��̾���� �Ÿ��� �Ǵ��ϰ� ������ �÷��̾ ��Ÿ�
    /// ���� �ִٸ� �ļӰ����� �����Ѵ�.
    /// ���ݻ��� -> �Ÿ� üũ -> �ļӰ��� or �������� ��ȯ
    /// </summary>
    class MeleeAttack1 : State<FinalBoss>
    {
        public override void Enter(FinalBoss f)
        {
            //Debug.Log("����1 ����");
            // ���� ��� ����
            f.animator.SetTrigger("Attack1Trigger");
        }
        public override void Execute(FinalBoss f)
        {
            f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.AttackIdle));
        }

        public override void Exit(FinalBoss f)
        {
            //Debug.Log("����1 ����");
        }
    }
    class MeleeAttack2 : State<FinalBoss>
    {
        public override void Enter(FinalBoss f)
        {
            //Debug.Log("����2 ����");
            f.animator.SetTrigger("Attack2Trigger");
        }
        public override void Execute(FinalBoss f)
        {
            f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.AttackIdle));
        }
        public override void Exit(FinalBoss f)
        {
            //Debug.Log("����2 ����");
        }
    }
    class MeleeAttack3 : State<FinalBoss>
    {
        public override void Enter(FinalBoss f)
        {
            f.animator.SetTrigger("Attack3Trigger");
        }
        public override void Execute(FinalBoss f)
        {
            f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.AttackIdle));
        }
        public override void Exit(FinalBoss f)
        {

        }
    }

    class RangeAttack : State<FinalBoss>
    {
        float stateEnterTime, stateDuration = 2f;
        public override void Enter(FinalBoss f)
        {
            stateEnterTime = Time.time;
            f.animator.SetTrigger("RangeAttack");
            f.prevRangeAtkTime = Time.time;
        }

        public override void Execute(FinalBoss f)
        {
            if(Time.time - stateEnterTime >= stateDuration)
                f.stateMachine.ChangeState(f.stateMachine.GetState((int)FinalBoss.FinalBossStates.Trace));
        }

        public override void Exit(FinalBoss f)
        {

        }
    }

    class Die : State<FinalBoss>
    {
        public override void Enter(FinalBoss f)
        {
            f.animator.SetTrigger("Die");
            f.rbody.isKinematic = true;
            f.collider.enabled = false;
        }
        public override void Execute(FinalBoss f)
        {
        }
        public override void Exit(FinalBoss f)
        {

        }
    }
}
