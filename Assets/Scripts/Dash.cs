using UnityEngine;

public class Dash : StaminaAbility
{
    protected override void Activate(EntityCharacter caster)
    {
        CapsuleCollider2D collider = caster.GetComponent<CapsuleCollider2D>();
        collider.size = new Vector2(caster.OriginalColliderSize.x, caster.OriginalColliderSize.y / 2f);
        collider.offset = new Vector2(caster.OriginalColliderOffset.x, caster.OriginalColliderOffset.y - caster.OriginalColliderSize.y / 4f);
        SpriteRenderer sr = caster.GetComponent<SpriteRenderer>();
        float direction = sr.flipX ? -1f : 1f;
        Rigidbody2D rigidBody = caster.GetComponent<Rigidbody2D>();
        rigidBody.linearVelocity = new Vector2(direction * caster.DashSpeed, 0);
    }
}