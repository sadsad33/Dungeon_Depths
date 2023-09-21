using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BossBaseFSM : MonoBehaviour {
    public enum BossStates { Idle, Trace, FastTrace, MeleeAttack, RangeAttack, Die };
    public StateMachine<BossBaseFSM> stateMachine;
    public Animator Animator { get; private set; }
    public Rigidbody Rbody { get; set; }
    #region ���� �������ͽ�
    public float BossMaxHp { get; set; }
    public float BossCurHp { get; set; }
    public float MoveSpeed { get; set; }
    public float RotSpeed { get; set; }
    public float AttackDamage { get; set; }
    #endregion
    #region ���� ���� ����
    public bool isSomethingAhead;
    public Transform TargetTransform { get; set; }
    public Transform BossTransform { get; set; }
    public float TraceRange { get; set; } // ���� �Ÿ��� �� ������ ��ŭ ������ ��
    #endregion  
    #region ���� ���� ����
    public float AttackDelay { get; set; }
    public float PrevAtkTime { get; set; }
    public float MeleeRange { get; set; }
    public float BeamRange { get; set; }
    #endregion
    public bool isDead;
    public float delayTime = 2.5f;
    public Vector3 targetPos;
    protected void FixedUpdate() {
        LayerMask layer = 1 << 8;
        isSomethingAhead = Physics.Raycast(BossTransform.position, BossTransform.forward, 3f, layer);
    }
    protected virtual void Awake() // �����߰� , �ʱ�ȭ
    {
        stateMachine = new StateMachine<BossBaseFSM>();
        Animator = GetComponent<Animator>();

        isDead = false;
        stateMachine.AddState((int)BossStates.Idle, new BossState.Idle());
        stateMachine.AddState((int)BossStates.Trace, new BossState.Trace());
        stateMachine.AddState((int)BossStates.FastTrace, new BossState.FastTrace());
        stateMachine.AddState((int)BossStates.MeleeAttack, new BossState.MeleeAttack());
        stateMachine.AddState((int)BossStates.RangeAttack, new BossState.RangeAttack());
        stateMachine.AddState((int)BossStates.Die, new BossState.Die());

        stateMachine.InitState(this, stateMachine.GetState((int)BossStates.Idle));
    }

    public abstract void GetHit(float _damage);

    // ���� ������� Ȯ��
    protected void CheckAlive() {
        if (BossCurHp <= 0) {
            isDead = true;
            stateMachine.ChangeState(stateMachine.GetState((int)BossStates.Die));
        }
    }

    /// <summary>
    /// �÷��̾�� ������ �Ÿ��� �����ؼ� �������¸� �����Ѵ�.
    /// CheckDistance �޼ҵ��� ��ȯ���� true��� ���� ����
    /// false��� �Ϲ� ����
    /// </summary>
    public void CheckTraceState() {
        if (Vector3.Distance(BossTransform.position, TargetTransform.position) > TraceRange / 2)
            stateMachine.ChangeState(stateMachine.GetState((int)BossStates.FastTrace));
        else
            stateMachine.ChangeState(stateMachine.GetState((int)BossStates.Trace));
    }

    /// <summary>
    /// ������ �÷��̾� ������ �Ÿ��� �����ؼ� CheckPlayerInSight�Լ��� ����
    /// </summary>
    public void CheckAttackState() {
        float dist = (TargetTransform.position - BossTransform.position).magnitude;
        CheckPlayerInSight(dist);
    }

    Vector3[] path;
    int targetIndex;
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {

        Vector3 currentWaypoints = path[0];
        while (true) {
            if (BossTransform.position == currentWaypoints) {
                targetIndex++;
                if (targetIndex >= path.Length) yield break;
                currentWaypoints = path[targetIndex];
            }
            if (stateMachine.CurrentState != stateMachine.GetState((int)BossStates.Trace) &&
                stateMachine.CurrentState != stateMachine.GetState((int)BossStates.FastTrace)) {
                Debug.Log("�̵� ����");
                yield return null;
            }
            BossTransform.position = Vector3.MoveTowards(BossTransform.position, currentWaypoints, MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void Rotate() {
        //Vector3 dir = TargetTransform.position - BossTransform.position;
        //Quaternion rot = Quaternion.LookRotation(dir);
        //BossTransform.rotation = Quaternion.Lerp(BossTransform.rotation, rot, RotSpeed * Time.deltaTime);
        Vector3 dir = TargetTransform.position - BossTransform.position;
        dir.y = 0; // y�� ������ ������ ������ �� ������ 0���� �����Ѵ�.
        if (dir != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(dir); // y�� ȸ������ �ִ� ���ʹϿ��� ���� �Ѵ�
            Quaternion finalRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // y�� ȸ������ �����ϵ��� �Ѵ�
            BossTransform.rotation = Quaternion.Lerp(BossTransform.rotation, finalRotation, RotSpeed * Time.deltaTime);
        }
    }

    // �÷��̾ ���� ��Ÿ����� ������ ���
    // �÷��̾ �þ߿� �����ƴ��� Ȯ��
    // �ƴ϶�� ������ ȸ���Ѵ�.
    public void CheckPlayerInSight(float _dist) {
        RaycastHit hit;
        Vector3 dir = TargetTransform.position - BossTransform.position;
        if (_dist <= MeleeRange) // �÷��̾ ���� ��Ÿ� ���� �ִٸ�
        {
            if (Physics.Raycast(BossTransform.position, BossTransform.forward, out hit, MeleeRange) && (hit.collider.CompareTag("Player") || hit.collider.CompareTag("BlockArea"))) {
                stateMachine.ChangeState(stateMachine.GetState((int)BossStates.MeleeAttack));
            }
            else {
                Rotate();
            }
        }
        else if (6 < _dist && _dist < 13) {
            stateMachine.ChangeState(stateMachine.GetState((int)BossStates.Trace));
        }
        else if (_dist <= BeamRange) {
            int random = Random.Range(0, 100);
            if (Physics.Raycast(BossTransform.position, BossTransform.forward, out hit, BeamRange) && (hit.collider.CompareTag("Player") || hit.collider.CompareTag("BlockArea"))) {
                targetPos = TargetTransform.position;
                if (random <= 4 && Time.time - PrevAtkTime >= AttackDelay) {
                    stateMachine.ChangeState(stateMachine.GetState((int)BossStates.RangeAttack));
                }
            }
            else {
                Rotate();
            }
        }

    }
}
