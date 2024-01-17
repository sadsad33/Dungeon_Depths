using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossGolem : BossBaseFSM
{
    [HideInInspector] public GameObject meleeHitBox1;
    [HideInInspector] public GameObject meleeHitBox2;

    public Transform firePos;
    GameObject missilePrefab;
    public GameObject laserHitBox;

    protected override void Awake()
    {
        base.Awake();
        firePos = transform.GetChild(5).GetComponent<Transform>();
        missilePrefab = Resources.Load<GameObject>("Prefabs/GolemMissile");
        Rbody = GetComponent<Rigidbody>();
        meleeHitBox1 = transform.GetChild(2).gameObject;
        meleeHitBox2 = transform.GetChild(3).gameObject;
        laserHitBox = transform.GetChild(4).gameObject;
        
        BossMaxHp = 500;
        BossCurHp = BossMaxHp;
        MoveSpeed = 3.5f;
        RotSpeed = 1.5f;
        AttackDamage = 30f;
        
        TargetTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        BossTransform = GetComponent<Transform>();
        TraceRange = 100f; // ������ ũ��� ����
        
        AttackDelay = 5f;
        MeleeRange = 3.5f;
        BeamRange = 20f;

    }
    public void OnMeleeAttackOneCollision()
    {
        meleeHitBox1.SetActive(true);
    }

    public void OnMeleeAttackTwoCollision()
    {
        meleeHitBox2.SetActive(true);
    }

    public void OnLaserAttackCollision()
    {
        laserHitBox.SetActive(true);   
    }

    public void OnMissileFire() 
    {
        GameObject[] missiles = new GameObject[5];
        for(int i = 0; i < 5; i++)
        {
            missiles[i] = Instantiate(missilePrefab, firePos.position, Quaternion.identity);
        }
        
    }
    public override void GetHit(float _damage)
    {
        BossCurHp -= _damage;
        BossCurHp = Mathf.Clamp(BossCurHp, 0, BossMaxHp);
        Debug.Log("���� ���� ü�� : " + BossCurHp);
        CheckAlive();
    }
    void Update()
    {
        Debug.Log("���� ���� : " + stateMachine.CurrentState);
        stateMachine.Execute();
        //rbody.velocity = Vector3.zero;
        //rbody.angularVelocity = Vector3.zero;
    }
}
