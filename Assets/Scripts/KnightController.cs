using System;
using System.Collections;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    private CameraScript cameraScript;
    private Knight knight;
    private Rigidbody2D rb2d;
    private CapsuleCollider2D cc2d;
    private Vector2 colliderSize;
    private Vector2 colliderOffset;
    private Animator ani;
    private SpriteRenderer sr;
    private Sword sword;
    [SerializeField]private GameObject swordObj;
    private Vector2 movement;
    private Vector2 velocity;
    private KnightState currentState = KnightState.Idle;
    private bool blockRequested;
    private float dizzleTime;
    private Coroutine dizzledCoroutine;
    private Coroutine dashCoroutine;
    public KnightState GetCurrentState(){
        return currentState;
    }

    void Awake()
    {
        knight = GetComponent<Knight>();
        velocity = Vector2.zero;
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CapsuleCollider2D>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sword = swordObj.GetComponent<Sword>();
        rb2d.gravityScale = 2f;
        colliderSize = cc2d.size;
        colliderOffset = cc2d.offset;
        blockRequested = false;
        dizzleTime = 2f;
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    void Update()
    {
        switch (currentState)
        {
            case KnightState.Idle:
                HandleIdle();
                break;
            case KnightState.Walk:
                HandleWalking();
                break;
            case KnightState.Jump:
                HandleJumping();
                break;
            case KnightState.Attack:
                HandleAttacking();
                break;
            case KnightState.Block:
                HandleBlocking();
                break;
            case KnightState.Dash:
                HandleDashing();
                break;
            case KnightState.Cast:
                HandleCasting();
                break;
            case KnightState.Dizzled:
                HandleDizzled();
                break;
        }
    }

    void FixedUpdate()
    {
        if (currentState == KnightState.Dash) return;
        
        velocity.Set(movement.x * knight.Speed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = velocity;
    }

    // ---------------------------
    // State Handling
    // ---------------------------

    void HandleIdle()
    {
        if (!IsGrounded())
        {
            SwitchState(KnightState.Jump);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.W)) TryJump();
        else if (Input.GetKeyDown(KeyCode.J)) StartAttack();
        else if (Input.GetKeyDown(KeyCode.Space)) StartBlock();
        else if (Input.GetKeyDown(KeyCode.K)) StartDash();
        else if (Input.GetKeyDown(KeyCode.L)) StartCast();
        else CheckMovement();
    }

    void HandleWalking()
    {
        if (!IsGrounded())
        {
            SwitchState(KnightState.Jump);
            return;
        }

        CheckMovement();
        if (Input.GetKeyDown(KeyCode.W)) TryJump();
        else if (Input.GetKeyDown(KeyCode.J)) StartAttack();
        else if (Input.GetKeyDown(KeyCode.Space)) StartBlock();
        else if (Input.GetKeyDown(KeyCode.K)) StartDash();
    }

    void HandleJumping()
    {
        CheckMovement();
        if (Input.GetKeyDown(KeyCode.Space)){
            if (!IsGrounded()){
                blockRequested = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            blockRequested = false;
        }
        if (IsGrounded())
        {
            if (blockRequested)
            {
                blockRequested = false;
                StartBlock();
            }
            else
            {
                SwitchState(KnightState.Idle);
            }
        }
    }

    void HandleAttacking() {
        movement.x = Input.GetAxisRaw("Horizontal");
    }
    void HandleBlocking()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SwitchState(KnightState.Idle);
        }
    }
    void HandleDashing() { }
    void HandleCasting() { }
    void HandleDizzled() { }

    // ---------------------------
    // Actions
    // ---------------------------

    void CheckMovement(){
        movement.x = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(movement.x) > 0.1f)
        {
            bool isMovingLeft = movement.x < 0;
            sr.flipX = isMovingLeft;

            // Flip the spawn points correctly
            Vector3 spawnLocalPos = knight.FireballSpawnPoint.localPosition;
            Vector3 dashLocalPos = knight.DashEffectSpawnPoint.localPosition;
            Vector3 swordLocalPos = swordObj.transform.localPosition;

            float sign = isMovingLeft ? -1f : 1f;

            spawnLocalPos.x = Mathf.Abs(spawnLocalPos.x) * sign;
            dashLocalPos.x = Mathf.Abs(dashLocalPos.x) * -sign;
            swordLocalPos.x = Mathf.Abs(swordLocalPos.x) * sign;
            

            knight.FireballSpawnPoint.localPosition = spawnLocalPos;
            knight.DashEffectSpawnPoint.localPosition = dashLocalPos;
            swordObj.transform.localPosition = swordLocalPos;

            if (currentState == KnightState.Idle)
                SwitchState(KnightState.Walk);
        }
        else
        {
            if (currentState == KnightState.Walk)
                SwitchState(KnightState.Idle);
        }
    }

    void TryJump(){
        if (IsGrounded())
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, knight.JumpForce);
            SwitchState(KnightState.Jump);
        }
    }

    void StartAttack(){
        SwitchState(KnightState.Attack);
    }

    void StartBlock(){
        movement = Vector2.zero;
        velocity = Vector2.zero;
        rb2d.linearVelocity = velocity;
        SwitchState(KnightState.Block);
    }

    void StartDash(){
        Dash ability = new(10);
        if(ability.CanCast(knight)){
            if (dashCoroutine != null)
                StopCoroutine(dashCoroutine);
            dashCoroutine = StartCoroutine(OnDashingEnd());
            SwitchState(KnightState.Dash);
            knight.Cast(ability);
        }
    }

    void StartCast(){
        SwitchState(KnightState.Cast);
    }

    void StartDizzled(){
        if(sword.isActiveAndEnabled){
            sword.DisableSwordCollider();
        }
        SwitchState(KnightState.Dizzled);
        if (dizzledCoroutine != null)
            StopCoroutine(dizzledCoroutine);
        dizzledCoroutine = StartCoroutine(OnDizzleEnd());
    }

    // ---------------------------
    // Helper
    // ---------------------------

    public void DetectDamageArea(){
        sword.EnableSwordCollider();
    }
    
    public void OnAttackAnimationEnd()
    {
        if (currentState == KnightState.Attack)
        {
            SwitchState(KnightState.Idle);
        }
        if (sword.isActiveAndEnabled){
            sword.DisableSwordCollider();
        }
    }

    public void OnDashAnimationStart(){
        PlayDashEffect();
    }

    public void PlayDashEffect(){
        if (knight.DashEffectSpawnPoint != null){
            Vector3 spawnPos = knight.DashEffectSpawnPoint.position;
            GameObject dashEffect = Instantiate(PrefabsManager.Instance.GetPrefabByName("dasheffect"), spawnPos, Quaternion.identity);
            dashEffect.GetComponent<SpriteRenderer>().flipX = sr.flipX;
        }
    }

    public void OnDashAnimationEnd()
    {
        if (currentState == KnightState.Dash){
            // Restore collider
            cc2d.size = colliderSize;
            cc2d.offset = colliderOffset;

            SwitchState(KnightState.Idle);
        }
    }

    public void OnCastAnimationEnd()
    {
        ShootFireball();
        if (currentState == KnightState.Cast)
        {
            SwitchState(KnightState.Idle);
        }
    }

    IEnumerator OnDashingEnd(){
        cameraScript.LockY();
        yield return new WaitForSeconds(0.4f);
        cameraScript.UnlockY();
    }

    IEnumerator OnDizzleEnd(){
        yield return new WaitForSeconds(dizzleTime);
        if (currentState == KnightState.Dizzled)
        {
            SwitchState(KnightState.Idle);
        }
    }

    public void SwitchState(KnightState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        PlayAnimationForState(newState);
    }

    void PlayAnimationForState(KnightState state)
    {
        switch (state)
        {
            case KnightState.Idle:
                ani.Play("state_idle");
                break;
            case KnightState.Walk:
                ani.Play("state_walk");
                break;
            case KnightState.Jump:
                ani.Play("state_jump");
                break;
            case KnightState.Attack:
                ani.Play("state_attack");
                break;
            case KnightState.Block:
                ani.Play("state_block");
                break;
            case KnightState.Dash:
                ani.Play("state_dash");
                break;
            case KnightState.Cast:
                ani.Play("state_cast");
                break;
            case KnightState.Dizzled:
                ani.Play("state_dizzle");
                break;
        }
    }

    bool IsGrounded()
    {
        // You can replace this by a proper FeetCollider check later!
        return rb2d.linearVelocity.y == 0;
    }

    void ShootFireball()
    {
        FireballAbility ability = new(50);
        if(ability.CanCast(knight)){
            knight.Cast(ability);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FoeStunningSpell"))
        {
            StartDizzled();
        }
    }
}