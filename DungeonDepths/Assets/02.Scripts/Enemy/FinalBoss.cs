using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FinalBoss : MonoBehaviour
{
    public enum FinalBossStates { Idle, AttackIdle, Trace, MeleeAttack1, MeleeAttack2, MeleeAttack3, RangeAttack, Die };
    public StateMachine<FinalBoss> stateMachine;
    public Animator animator;
    public float comboDuration = 2.5f;
    [SerializeField] float moveSpeed = 9f;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] Transform finalBossTransform;
    [SerializeField] Transform targetTransform;
    public bool isDead;
    public Rigidbody rbody;
    public CapsuleCollider collider;

    public bool isSecondPhase;
    public float prevRangeAtkTime;
    float bossRangeAtkDelay = 15f;

    float meleeAttackRange = 3f;
    float meleeAttackAngle = 60f;

    public float hpMax = 100, hpCur;
    readonly int hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    void Awake()
    {
        hpCur = hpMax;
        finalBossTransform = GetComponent<Transform>();
        targetTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        stateMachine = new StateMachine<FinalBoss>();
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();

        
        isSecondPhase = false;

        stateMachine.AddState((int)FinalBossStates.Idle, new FinalBossState.Idle());
        stateMachine.AddState((int)FinalBossStates.AttackIdle, new FinalBossState.AttackIdle());
        stateMachine.AddState((int)FinalBossStates.Trace, new FinalBossState.Trace());
        stateMachine.AddState((int)FinalBossStates.MeleeAttack1, new FinalBossState.MeleeAttack1());
        stateMachine.AddState((int)FinalBossStates.MeleeAttack2, new FinalBossState.MeleeAttack2());
        stateMachine.AddState((int)FinalBossStates.MeleeAttack3, new FinalBossState.MeleeAttack3());
        stateMachine.AddState((int)FinalBossStates.RangeAttack, new FinalBossState.RangeAttack());
        stateMachine.AddState((int)FinalBossStates.Die, new FinalBossState.Die());

        stateMachine.InitState(this, stateMachine.GetState((int)FinalBossStates.Idle));
    }

	private void OnEnable()
	{
        animator.SetTrigger("BossEnter");
        stateMachine.InitState(this, stateMachine.GetState((int)FinalBossStates.Idle));
    }
    private void Update()
    {
        //Debug.Log("�������� ���� ���� : " + stateMachine.CurrentState);
        stateMachine.Execute();
    }

    public void GetHit(float _damage)
    {
        hpCur -= _damage;
        Debug.Log("���� ���� ���� ü�� : " + hpCur);
        if(hpCur <= hpMax * 0.6 && !isSecondPhase)
        {
            animator.SetTrigger("Stun");
            isSecondPhase = true;
        }
        else if(hpCur <= 0)
        {
            hpCur = 0f;
            isDead = true;
            stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.Die));
        }
    }

    //������ �÷��̾� ������ �Ÿ��� ���ؼ� ��ȯ
    public float CheckDistance()
    {
        //Debug.Log("�÷��̾���� �Ÿ� ���ϱ�");
        return (finalBossTransform.position - targetTransform.position).magnitude;
    }

    // ������ ���¸� Trace ���·� ��ȯ�Ѵ�.
    public void CheckTraceState()
    {
        //Debug.Log("���� Ȯ��");
        stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.Trace));
    }
    // ������ ��ǥ�� �÷��̾� ��ǥ������ �̵���Ų��.
    public void Trace()
    {
        //Debug.Log("���� ����");
        finalBossTransform.position = Vector3.MoveTowards(finalBossTransform.position, targetTransform.position, moveSpeed * Time.deltaTime);
    }

    // ������ �÷��̾����� �ٶ󺸰Բ� ȸ����Ų��.
    public void Rotation()
    {
        //Debug.Log("ȸ��");
        Vector3 dir = targetTransform.position - finalBossTransform.position;
        if(dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir); // y�� ȸ������ �ִ� ���ʹϿ��� �����մϴ�.
            Quaternion finalRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // y�� ȸ������ �����ϵ��� �մϴ�.
            finalBossTransform.rotation = Quaternion.Lerp(finalBossTransform.rotation, finalRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // �÷��̾���� �Ÿ��� Ȯ������ �̸� ���ݹݰ� �˻� �Լ��� �����Ѵ�.
    public void CheckAttackState()
    {
        Debug.Log("���ݻ��� Ȯ��");
        //if(Time.time - prevAttackTime < bossAttackDelay) return;
        float dist = CheckDistance();
        int attackIndex;
        //Debug.Log("�÷��̾���� �Ÿ� : " + dist);
        if(IsPlayerInAttackSight(dist, out attackIndex))
        {
            //prevAttackTime = Time.time;
            if(attackIndex == 1)
                stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.MeleeAttack1));
            else if(attackIndex == 2)
                stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.MeleeAttack2));
            else if(attackIndex == 3)
                stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.MeleeAttack3));
            else if(attackIndex == 4)
                stateMachine.ChangeState(stateMachine.GetState((int)FinalBossStates.RangeAttack));
        }
        else Rotation();
    }

    //������ ���ݹݰ��� �˻��Ѵ�.
    public bool IsPlayerInAttackSight(float _dist, out int _attackIndex)
    {
        //Debug.Log("���� ���� �þ� Ȯ��");
        //RaycastHit hit;
        //Ray ray = new Ray(finalBossTransform.position, finalBossTransform.forward);
        Vector3 dir = (targetTransform.position - finalBossTransform.position).normalized;
        // �������� ��Ÿ� ���� �����ִٸ�
        if(_dist <= meleeAttackRange)
        {
            float dot = Vector3.Dot(finalBossTransform.forward, dir);
            float theta = Mathf.Acos(dot);
            float angle = Mathf.Rad2Deg * theta;

            //if(Physics.Raycast(ray, out hit, meleeAttackRange) && hit.collider.CompareTag("Player"))
            if(angle <= meleeAttackAngle / 5)
            {
                _attackIndex = 1;
                return true;
            }
            else if(angle <= meleeAttackAngle && isSecondPhase)
            {
                _attackIndex = 3;
                return true;
            }
            else if(angle <= meleeAttackAngle * 2)
            {
                _attackIndex = 2;
                return true;
            }
            else Rotation();
        }
        else if(_dist < 10f)
        {
            _attackIndex = 0;
            return false;
        }
        else if(_dist <= meleeAttackRange * 5 && isSecondPhase)
        {
            Debug.Log("���Ÿ� ���� Ȯ��");
            if(Time.time - prevRangeAtkTime >= bossRangeAtkDelay)
            {
                _attackIndex = 4;
                return true;
            }
        }
        _attackIndex = 0;
        return false;
    }

    public bool ShouldCombo(out int comboIndex)
    {
        Debug.Log("�޺����� �˻�");
        float dist = CheckDistance();
        if(IsPlayerInAttackSight(dist, out comboIndex))
        {
            if(comboIndex == 0) return false;
            return true;
        }
        return false;
    }
}
