using UnityEngine;

public class Monster : MonoBehaviour
{
    public float damage = 10f; // 몬스터가 플레이어에게 입히는 피해량

    public void Attack()
    {
        Debug.Log("공격");
    }
    
}
