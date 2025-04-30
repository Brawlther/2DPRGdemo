using UnityEngine;
using System.Collections;

public class DummyController : MonoBehaviour
{
    private Dummy dummy;
    private KnightState currentState = KnightState.Idle;
    private Vector2 movement;
    private Vector2 velocity;
    private float dizzleTime;
    private Coroutine dizzledCoroutine;
    private SpriteRenderer sr;
    private Rigidbody2D rb2d;
    private Animator ani;
    private CapsuleCollider2D colldier;
    private bool isDizzled;
    void Awake()
    {
        dummy = GetComponent<Dummy>();
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        colldier = GetComponent<CapsuleCollider2D>();
        velocity = Vector2.zero;
        dizzleTime = 2f;
        rb2d.gravityScale = 2f;
        isDizzled = false;
    }
    void Update()
    {
        switch (currentState)
        {
            case KnightState.Idle:
                HandleIdle();
                break;
            case KnightState.Dizzled:
                HandleDizzled();
                break;
            case KnightState.Hurt:
                HandleHurt();
                break;
            case KnightState.Die:
                HandleDie();
                break;
        }
    }
    void HandleIdle() { }
    void HandleDizzled() { }
    void HandleHurt() { }
    void HandleDie() { }
    
    void CheckMovement(){
        movement.x = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(movement.x) > 0.1f)
        {
            bool isMovingLeft = movement.x < 0;
            sr.flipX = isMovingLeft;

            if (currentState == KnightState.Idle)
                SwitchState(KnightState.Walk);
        }
        else
        {
            if (currentState == KnightState.Walk)
                SwitchState(KnightState.Idle);
        }
    }
    void FixedUpdate(){
        // Only allow control when NOT Dizzled
        if (currentState == KnightState.Dizzled)
        {
            // During dizzled, check if grounded
            if (IsGrounded())
            {
                // Stop horizontal movement when touched ground
                velocity.Set(0f, rb2d.linearVelocity.y);
                rb2d.linearVelocity = velocity;
            }

            // Otherwise, allow natural flying
            return;
        }

        if (currentState == KnightState.Dash)
            return;
            
        velocity.Set(movement.x * dummy.GetSpeed(), rb2d.linearVelocity.y);
        rb2d.linearVelocity = velocity;
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
            case KnightState.Dizzled:
                ani.Play("state_dizzle");
                break;
            case KnightState.Hurt:
                ani.Play("state_hurt");
                break;
            case KnightState.Die:
                ani.Play("state_die");
                break;
        }
    }

    void TryHurt(){
        if(currentState != KnightState.Die){
            StartHurt();
        }
    }
    void StartHurt(){
        SwitchState(KnightState.Hurt);
    }

    public void StartDie(){
        SwitchState(KnightState.Die);
    }

    void TryDazzle(){
        if(currentState != KnightState.Die){
            StartDazzle();
        }
    }
    void StartDazzle(){
        isDizzled = true;
        SwitchState(KnightState.Dizzled);
        if (dizzledCoroutine != null)
            StopCoroutine(dizzledCoroutine);
        dizzledCoroutine = StartCoroutine(OnDazzleEnd());
    }
    IEnumerator OnDazzleEnd(){
        yield return new WaitForSeconds(dizzleTime);
        if (currentState == KnightState.Dizzled)
        {
            SwitchState(KnightState.Idle);
            isDizzled = false;
        }
    }

    public void OnHurtEnd(){
        if(isDizzled){
            SwitchState(KnightState.Dizzled);
        }
        else{
            SwitchState(KnightState.Idle);
        }
    }

    //This needs to be updated into a normal method and called after damage calculation
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerStunningSpell")){
            TryDazzle();
        }
        else if(other.CompareTag("PlayerWeapon")){
            TryHurt();
        }
    }

    public void OnDieAnimationEnd(){
        Destroy(gameObject);
    }

    bool IsGrounded()
    {
        // You can replace this by a proper FeetCollider check later!
        return rb2d.linearVelocity.y == 0;
    }
}
