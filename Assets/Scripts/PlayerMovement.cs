using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float rotateSpeed = 180f; // 회전 속도

    private PlayerInput playerInput; // 플레이어 입력 컴포넌트 참조
    private Rigidbody playerRigidbody; //플레이어 캐릭터의 리지드바디
    private Animator playerAnimtor; // 플레이어 캐릭터의 애니메이터

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
     playerInput = GetComponent<PlayerInput>(); // 플레이어 입력 컴포넌트 참조
     playerRigidbody = GetComponent<Rigidbody>(); // 플레이어 리지드바디 참조
     playerAnimtor = GetComponent<Animator>(); // 플레이어 애니메이터 참조
    }

    //물리갱신주기에 맞춰 실행
    private void FixedUpdate()
    {
        Rotate();
        Move();

        playerAnimtor.SetFloat("Move", playerInput.move);
        //애니메이터의 Move 파라미터에 움직임 입력값 전달
    }

    // Update is called once per frame
    private void Move()
    {
        Vector3 moveDistance =
        playerInput.move * transform.forward * moveSpeed * Time.fixedDeltaTime; // 이동 거리 계산
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance); // 리지드바디 이동
    }

    private void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.fixedDeltaTime; // 회전 각도 계산
        playerRigidbody.rotation =
            playerRigidbody.rotation * Quaternion.Euler(0, turn, 0); // 리지드바디 회전
    }
}
