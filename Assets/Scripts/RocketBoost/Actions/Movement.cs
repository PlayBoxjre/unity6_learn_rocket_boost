using System;
using UnityEngine;
using UnityEngine.InputSystem;

// 这个脚本将会监听玩家的输入，并根据输入来控制角色的移动。
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    // 这个字段将会在Unity编辑器中显示，允许我们将一个InputAction分配给它。
    // 名词 (n.)：推力；驱动力
    //搭配：engine thrust（发动机推力）
    //例句：The rocket has enough thrust to escape gravity.（这枚火箭有足够的推力摆脱引力。）
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotaion;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;

    Rigidbody rb;

  

    private void OnEnable()
    {
        thrust.Enable();
        rotaion.Enable();
    }

    private void OnDisable()
    {
        thrust.Disable();
        rotaion.Disable();
    }

    private void Start()
    {
        Physics.gravity = new Vector3(0, -4f, 0);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {   // 这个方法将会在每个物理更新周期被调用，在这里我们将会处理玩家的输入并根据输入来控制角色的移动。
        ProcessThrust();

        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float routationInput = rotaion.ReadValue<float>();
        if(routationInput != 0f)
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * -routationInput * Time.fixedDeltaTime * rotationStrength);
            rb.freezeRotation = false;

        }
    }

    // 这个方法将会检查玩家是否按下了推力按钮，如果按下了，就会给角色施加一个向上的力。
    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }
}
