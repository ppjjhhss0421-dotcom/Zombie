using UnityEngine;
using System;
public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100; //시작 체력
    public float health { get; protected set; } //현재 체력
    public bool dead { get; protected set; } //사망상태
    public event Action onDeath; //사망시 이벤트

    protected virtual void OnEnable() //생명체가 활성화될 때 상태를 리셋
    {
        dead = false; //사망하지않은 상태로 시작
        health = startingHealth; //체력을 시작 체력으로 초기화
    }

    public virtual void onDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) //데미지를 입었을 때 호출되는 메서드
    {
        health -= damage; //체력 감소
        if (health <= 0 && !dead) //체력이 0 이하이고 아직 사망하지 않은 경우
        {
            Die();
        }
    }

    public virtual void RestoreHealth(float newHealth) //체력을 회복하는 메서드
    {
        if (dead)
        {
            return; //사망한 상태에서는 체력을 회복할 수 없음
        }

            health += newHealth; //체력 추가

    }

    public virtual void Die() //onDeath 이벤트를 호출하고 사망 상태로 변경하는 메서드
    {
        if (onDeath != null)
        {
            onDeath();
        }
        dead = true; //사망 상태로 변경
    }

    internal void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        throw new NotImplementedException();
    }
}
