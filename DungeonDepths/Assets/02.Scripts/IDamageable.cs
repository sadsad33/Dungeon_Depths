using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // ���
    void TakeDamage(float _damage);
    // ��Ʈ��
    void TakeDamageOverTime(float _damagePerSecond, float _duration);
    // ����
    void OnDeath();
}
