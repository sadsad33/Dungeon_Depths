using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitBox : MonoBehaviour
{
    PlayerBase player;
    MonsterBase monster;
    private void Start()
    {
        monster = transform.parent.GetComponent<MonsterBase>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("BlockArea"))
        {
            if (player.HasCounter)  // ���� + ī���� = ����
            {
                //����
                Debug.Log("����");
            }
        }    
        if (_other.CompareTag("Player") && !player.IsDodge)
        {
            //_other.SendMessage("SetTakedDamage", this.gameObject.transform.parent.GetComponent<MonsterBase>().Damage);
            player.SetTakedDamage(monster.Damage);
            //Debug.Log(this.gameObject.transform.parent.name + " Damage " + this.gameObject.transform.parent.GetComponent<MonsterBase>().Damage);
        }

    }
}
