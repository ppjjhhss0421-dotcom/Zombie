
using System;
using UnityEngine;

public class Polytest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Orc orc = FindFirstObjectByType<Orc>(); //씬에 있는 Orc 오브젝트를 찾아서 orc 변수에 할당
        Monster monster = orc; // Orc는 Monster를 상속받았기 때문에, orc를 monster로 형변환 가능
        monster.Attack(); // Monster 클래스의 Attack() 메서드 호출

        orc.Warcry();
}

  
}
