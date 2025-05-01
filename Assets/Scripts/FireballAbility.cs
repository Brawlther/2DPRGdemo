using UnityEngine;

public class FireballAbility : MPAbility
{
    public FireballAbility(int consumption) {
        abilityName = "Fireball";
        this.consumption = consumption;
    }
    protected override void Activate(EntityCharacter caster){
        Transform spawnTransform = caster.FireballSpawnPoint;
        if (spawnTransform != null){
            Vector3 spawnPos = spawnTransform.position;
            GameObject fireball = Object.Instantiate(PrefabsManager.Instance.GetPrefabByName("fireball"), spawnPos, Quaternion.identity);
            fireball.tag = "PlayerStunningSpell";
            bool isFlipped = caster.IsHorizontallyFlipped;
            fireball.GetComponent<SpriteRenderer>().flipX = isFlipped;
            Fireball projectile = fireball.GetComponent<Fireball>();
            projectile.SetDirection(isFlipped ? Vector2.left : Vector2.right);
            projectile.Speed = 8f;
        }
    }
}