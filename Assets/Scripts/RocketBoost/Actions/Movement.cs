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
    [SerializeField] float volumnScale = 1.0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainTrustParticle;
    [SerializeField] ParticleSystem rightTrustParticle;
    [SerializeField] ParticleSystem leftTrustParticle;



    Rigidbody rb;
    AudioSource trustAudioSource;


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
        trustAudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {   // 这个方法将会在每个物理更新周期被调用，在这里我们将会处理玩家的输入并根据输入来控制角色的移动。
        ProcessThrust();

        ProcessRotation();
    }
    // 这个方法将会检查玩家是否按下了推力按钮，如果按下了，就会给角色施加一个向上的力。
    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    private void ProcessRotation()
    {
        float routationInput = rotaion.ReadValue<float>();
        if (routationInput != 0f)
        {
            StartRotaion(routationInput);
        }
        else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        rightTrustParticle.Stop();
        leftTrustParticle.Stop();
    }

    private void StartRotaion(float routationInput)
    {
        ApplyRotation(routationInput);
        // 左转  Left turn button
        if (routationInput < 0)
        {
            LeftTurn();
        }
        // 右转 Right turn button
        else if (routationInput > 0)
        {
            RightTrun();
        }
    }

    private void RightTrun()
    {
        if (!leftTrustParticle.isPlaying)
        {
            rightTrustParticle.Stop();
            leftTrustParticle.Play();
        }
    }

    private void LeftTurn()
    {
        if (!rightTrustParticle.isPlaying)
        {
            leftTrustParticle.Stop();
            rightTrustParticle.Play();
        }
    }

    private void ApplyRotation(float routationInput)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * -routationInput * Time.fixedDeltaTime * rotationStrength);
        rb.freezeRotation = false;
    }

 

    private void StopThrusting()
    {
        trustAudioSource.Stop();
        mainTrustParticle.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        if (!trustAudioSource.isPlaying)
        {
            trustAudioSource.PlayOneShot(mainEngine, volumnScale);

        }
        if (!mainTrustParticle.isPlaying)
        {
            mainTrustParticle.Play();
        }
    }


}
