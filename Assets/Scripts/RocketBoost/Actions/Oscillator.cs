using Unity.Hierarchy;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector3; // 方向和距离
    [SerializeField] private float speed = 1f;      // 震荡的速度
    private float movenmentFactor; // 震荡的幅度，范围在0和1之间
    private Vector3 startPostion;  // 初始位置
    private Vector3 endPostion; // 结束位置
    void Start()
    {
        startPostion = transform.position;
        endPostion = startPostion + movementVector3;
    }

    void Update()
    {
        movenmentFactor = Mathf.PingPong(Time.time * speed, 1f); // 这个方法将会返回一个在0和1之间来回震荡的值，震荡的速度由speed参数控制。
        transform.position = Vector3.Lerp(startPostion,endPostion,movenmentFactor);
    }
}
