using UnityEngine;
public abstract class EntityCharacter : Character
{
    private int _hp;
    public int Hp {
        get => _hp;
    }
    public virtual void UpdateHp(int change){
        _hp += change;
    }

    private int _mp;
    public int Mp {
        get => _mp;
    }
    public virtual void UpdateMp(int change){
        _mp += change;
    }

    private int _stamina;
    public int Stamina {
        get => _stamina;
    }
    public virtual void UpdateStamina(int change){
        _stamina += change;
    }

    private int _speed;
    public int Speed {
        get => _speed;
    }
    public void UpdateSpeed(int change){
        _speed += change;
    }

    private int _maxHp;
    public int MaxHp {
        get => _maxHp;
    }
    public void UpdateMaxHp(int change){
        _maxHp += change;
    }

    private int _maxMp;
    public int MaxMp {
        get => _maxMp;
    }
    public void UpdateMaxMp(int change){
        _maxMp += change;
    }

    private int _maxStamina;
    public int MaxStamina {
        get => _maxStamina;
    }
    public void UpdateMaxStamina(int change){
        _maxStamina += change;
    }

    private float _jumpForce;
    public float JumpForce {
        get => _jumpForce;
    }
    public void UpdateJumpForce(float change){
        _jumpForce += change;
    }
    private float _dashSpeed;
    public float DashSpeed {
        get => _dashSpeed;
    }
    public void UpdateDashSpeed(float change){
        _dashSpeed += change;
    }

    private Vector2 _originalColliderSize;
    public Vector2 OriginalColliderSize {
        get => _originalColliderSize;
        set => _originalColliderSize = value;
    }

    private Vector2 _originalColliderOffset;
    public Vector2 OriginalColliderOffset {
        get => _originalColliderOffset;
        set => _originalColliderOffset = value;
    }

    public virtual void Cast(Ability ability){
        ability.Launch(this);
    }


}