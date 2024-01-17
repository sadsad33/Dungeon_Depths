using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;


//public abstract class AbilityEffect
//{
//    public abstract void ApplyEffect(PlayerBase _player, CardData _cardData);
//}
public class AbilityEffect// : AbilityEffect
{
    float moveSpeedCoefficient = 2f;
    // ���ݼӵ� 15���� ����
    float attackSpeedCoefficient = 1.15f;
    float attackRangeCoefficient = 1f;
    float attackPowerCoefficient = 10f;
    int jumpNum = 1;
    float defenseCoefficient = 5f;
    float coolDownCoefficient = 0.3f;

    public bool Explode { get; set; }
    public bool IsExecute { get; set; }
    public bool Sting { get; set; }
    public bool Flash { get; set; }
    public bool Meteor { get; set; }
    public bool FrozenField { get; set; }
    public bool Roll { get; set; }
    public bool FlashBang { get; set; }
    public bool Genocide { get; set; }
    public bool HasPet { get; set; }
    public void StatBoostEffect(PlayerBase _player, CardData _cardData)
    {
        // StatType�� ���� �̵��ӵ�, ���� ���� ������ ������ŵ�ϴ�.
        switch (_cardData.CardID)
        {
            case CardID.CARD_SPRINT:
                _player.MoveSpeed += moveSpeedCoefficient;
                break;
            case CardID.CARD_FRENZY:
                _player.animator.SetFloat("AttackSpeed", attackSpeedCoefficient);
                _player.attackStateDuration -= _player.attackStateDuration * 0.15f;
                break;
            case CardID.CARD_SNIPER:
                //_player.AttackRange += attackRangeCoefficient;
                _player.HasSniper = true;
                break;
            case CardID.CARD_POWER:
                _player.AttackPower += attackPowerCoefficient;
                break;
            case CardID.CARD_JUMP:
                _player.possibleJumpNum += jumpNum;
                break;
            case CardID.CARD_DEFENSE:
                _player.Defense += defenseCoefficient;
                break;
            case CardID.CARD_COOLDOWN:
                _player.firstSkillCoolDown -= 3f;
                _player.secondSkillCoolDown -= 3f;
                _player.dodgeSkillCoolDown -= 3f;
                Mathf.Clamp(_player.dodgeSkillCoolDown, 0f, 10f);
                break;
            case CardID.CARD_REBIRTH:
                _player.HasRebirth = true;
                break;
            case CardID.CARD_BARRIER:
                _player.HasBarrier = 100f;
                break;
            case CardID.CARD_LIFESTEAL:
                _player.HasLifeSteal = true;
                break;
            case CardID.CARD_REGEN:
                _player.HasRegen = true;
                _player.lastHitTimer = Time.time;
                break;
            case CardID.CARD_BERSERK:
                _player.HasBerserk = true;
                break;
            case CardID.CARD_EXPLODE:
                break;
            case CardID.CARD_EXECUTE:
                break;
            case CardID.CARD_AMPLIFY:
                _player.HasAmplify = true;
                break;
            case CardID.CARD_BOSS:
                _player.HasBossBonus = true;
                break;
            case CardID.CARD_POISON:
                _player.HasPoison = true;
                break;
            case CardID.CARD_SHIELD:
                _player.HasCounter = true;
                break;
            case CardID.CARD_EARTHQUAKE:
                _player.EarthQuake = true;
                break;
            case CardID.CARD_STING:
                break;
            case CardID.CARD_FLASH:
                break;
            case CardID.CARD_METEOR:
                break;
            case CardID.CARD_FROZENFIELD:
                break;
            case CardID.CARD_ROLL:
                break;
            case CardID.CARD_FLASHBANG:
                break;
            case CardID.CARD_GENOCIDE:
                break;
            case CardID.CARD_PET:
                break;
            default:
                break;
        }
    }

    
}

/*===============value ������===============
 * �̼� ����
 * ���� ����
 * ���� ����(��Ÿ�) ���� 
 * ���ݷ� ����
 * ���� Ƚ�� ����
 * 
 * �ǰ� ������ ����
*/

/*============================== value ������==============================
 * ��ų ��Ÿ�� ����
 */

//==============================Ư�� �ɷ·�==============================

//===============Ȱ��ȭ��===============
// ��Ȱ
// ��ȣ��
// �⺻ ���� ���� 
// �����ð� ���ǰݽ� ü�� ���
// ����Ŀ
// �� óġ�� �����ϸ� ���� ����
// ������ ������ hp ��� ó��
//ũ��Ƽ�� �� ������ ����(Ȯ�� �����س��� ������ ������ ��Ű��)
// �⺻ ���ݽ� ��, ����, �� �Ӽ� �߰�
// ���� �ߵ�
// TODO ���ȯ : ���

/*===============Ȱ��ȭ��(��ų��ȭ)===============
 * �˻�| ȸ�Ǳ�(���渷��) ������, ���� ȿ��
 * �˻�| ����� ������ ����
 * => ���� �Լ� �ϳ��� ������ ��
 * �˻�| ��� ��Ʈ�ڽ� ����
 */

//TODO ������ ������ RARE �ɷ� ����


