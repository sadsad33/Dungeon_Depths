using System;
using System.Collections;
using UnityEngine;

public class GolemMissile : MonoBehaviour
{
    private Vector3 startPos, endPos, targetPos;
    //���� �ݱ���� �ɸ��� �ð�
    protected float timer;
    protected float timeToFloor;
    float xPosOffset, zPosOffset;
    PlayerBase player;
    GameObject explosionPrefab;
    private void Start()
    {
        explosionPrefab = Resources.Load<GameObject>("Prefabs/BossMisslieImpact");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        targetPos = player.transform.position;
        startPos = transform.position;
        xPosOffset = UnityEngine.Random.Range(-5, 5);
        zPosOffset = UnityEngine.Random.Range(-5, 5);
        endPos = new Vector3(targetPos.x + xPosOffset, targetPos.y, targetPos.z + zPosOffset);

        StartCoroutine("BulletMove");
    }
    private void Update()
    {
        CheckImpact();
    }
    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> func = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    protected IEnumerator BulletMove()
    {
        timer = 0;
        // ����ü�� ���߿� �� �ִ� ����
        while(transform.position.y >= 0)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }
    }

    void CheckImpact()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.2f);
        if(colliders.Length >= 1)
        {
            Destroy(gameObject, 1.0f);
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].CompareTag("Player"))
                    player.SetTakedDamage(50);
            }

            Explosion();
        }
    }

    void Explosion()
    {
        GameObject impact = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
    //TODO
    /*
     * ��ź�� �����ϴ� ����Ʈ�� �Բ� ��Ʈ�ڽ� ����
     * ��Ʈ�ڽ� ���� �÷��̾� ������ ������
     */
}
