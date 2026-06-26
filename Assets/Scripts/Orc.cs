using UnityEngine;

public class Orc : Monster
{
    public override void Attack()
    {
        base.Attack(); // 부모 클래스의 Attack() 메서드 호출
    }
    public void Warcry()
    {
        Debug.Log("오크가 외칩니다: 와크리!");

        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None); ;//씬에 있는 모든 Monster 오브젝트를 찾아서 monsters 배열에 할당
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].damage += 10; // Monster 클래스의 Attack() 메서드 호출
            Debug.Log($"{monsters[i].name}의 공격력이 {monsters[i].damage}로 증가했습니다.");
        }
    }
}
