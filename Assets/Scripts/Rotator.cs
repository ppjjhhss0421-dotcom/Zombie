using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 60f; // 회전 속도

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); // y축을 기준으로 회전
    }
}
