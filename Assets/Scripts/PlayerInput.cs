using UnityEngine;

//플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
//감지된 입력값을 다른 컴포넌트가 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour
{
   public string moveAxisName = "Vertical"; //앞뒤 움직일을 위한 입력축 이름
   public string rotateAxisName = "Horizontal"; //좌우 회전을 위한 입력축 이름
   public string fireButtonName = "Fire1"; //발사 버튼 이름
   public string reloadButtonName = "Reload"; //재장전 버튼 이름

    //값 할당은 내부에서만 가능
    public float move { get; private set; } // 감지된 움직임 입력값
    public float rotate { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값

    //매프레임 사용자 입력을 감지
    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            return;
        }
        move = Input.GetAxis(moveAxisName); //움직임 입력값 감지
        rotate = Input.GetAxis(rotateAxisName); //회전 입력값 감지
        fire = Input.GetButton(fireButtonName); //발사 입력값 감지
        reload = Input.GetButtonDown(reloadButtonName); //재장전 입력값 감지
    }
}
