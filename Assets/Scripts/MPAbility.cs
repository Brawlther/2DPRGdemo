public abstract class MPAbility : Ability
{
    protected override bool HasSufficientResource(EntityCharacter caster) {
        return caster.Mp > Consumption;
    }

    protected override void ConsumeResource(EntityCharacter caster) {
         caster.UpdateMp(Consumption);
    }
}