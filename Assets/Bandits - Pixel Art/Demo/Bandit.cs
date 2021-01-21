using System;
using FMODUnity;
using UnityEngine;

#pragma warning disable CS0649

public class Bandit : MonoBehaviour {
    public event Action AttackedEvent = delegate { };
    public event Action DiedEvent = delegate { };
    public event Action JumpedEvent = delegate { };
    public event Action LandedEvent = delegate { };
    public event Action FootstepEvent = delegate { };
    public event Action AttackHitEvent = delegate { };


    [SerializeField] float m_speed = 1.0f;
    [SerializeField] float m_jumpForce = 2.0f;
    [SerializeField] float blockDuration = 1f;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] Animator m_animator;
    [SerializeField] Rigidbody2D m_body2d;
    [SerializeField] Sensor_Bandit m_groundSensor;
    [SerializeField] HealthDisplay healthDisplay;
    
    bool m_grounded;
    bool m_combatIdle;
    bool m_isDead;

    float postAttackCooldown;
    float postBlockCooldown;
    float blockTimer;
    public float currentHealth { get; private set; }

    ICharacterInput input;

    public bool IsBlocking { get; private set; }
    public bool IsDead => m_isDead;

    
    public void Setup(ICharacterInput newInput) {
        input = newInput;
        currentHealth = maxHealth;
        healthDisplay.SetPercentage(1);
    }

    // Update is called once per frame
    void Update() {
        if (input == null || m_isDead)
            return;
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            LandedEvent();
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        var inputX = CanMove() ? input.GetHorizontalMove() : 0;

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        if (postAttackCooldown > 0)
            postAttackCooldown -= Time.deltaTime;

        if (blockTimer <= 0 && postBlockCooldown > 0) {
            postBlockCooldown -= Time.deltaTime;
            IsBlocking = false;
            m_combatIdle = false;
        }

        if (blockTimer > 0) {
            blockTimer -= Time.deltaTime;
        }
        // -- Handle Animations --
        //Attack
        else if (input.Attack() && postAttackCooldown <= 0) {
            m_animator.SetTrigger("Attack");
            postAttackCooldown = 0.9f;
            AttackedEvent();
        }
        //Jump
        else if (input.Jump() && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
            JumpedEvent();
        } else if (input.Block() && postBlockCooldown <= 0) {
            IsBlocking = true;
            m_combatIdle = true;
            postBlockCooldown = 1f;
            blockTimer = blockDuration;
        }
        //Run
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);
        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);
        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    public void TakeDamage(float damage) {
        if (m_isDead)
            return;
        if (IsBlocking)
            return;
        currentHealth -= damage;
        var healthPercentage = currentHealth / maxHealth;
        healthDisplay.SetPercentage(healthPercentage);
        if (currentHealth <= 0) {
            m_isDead = true;
            m_animator.SetTrigger("Death");
            DiedEvent();
        } else {
            m_animator.SetTrigger("Hurt");
        }
    }

    bool CanMove() {
        return postAttackCooldown <= 0 && !IsBlocking;
    }

    void FireAnimationEventOnFootstep() {
        FootstepEvent();
    }

    public void AnimatorCallback_CheckForTarget() {
        AttackHitEvent();
    }

    public Vector2 GetFacingDirection() {
        return Vector2.left * transform.localScale.x;
    }
}

public interface ICharacterInput {
    float GetHorizontalMove();
    bool Attack();
    bool Jump();
    bool Block();
}