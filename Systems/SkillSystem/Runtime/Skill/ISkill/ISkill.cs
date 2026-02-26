using UnityEngine;

public interface ISkill {
    public abstract void Finish(SkillsComponent caster);
    public abstract void Cast(SkillsComponent caster, Vector2 direction);
    public abstract void Tick(SkillsComponent caster);
}