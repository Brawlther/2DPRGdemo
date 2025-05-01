public abstract class MPAbility : Ability
{
    public override bool CanCast(EntityCharacter caster) {
        return caster.Mp >= Consumption;
    }

    protected override void ConsumeResource(EntityCharacter caster) {
         caster.UpdateMp(-Consumption);
    }
}