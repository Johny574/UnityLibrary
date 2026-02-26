using System;
using UnityEngine;

[Serializable]
public abstract class Skill : ISkill {
    public bool Cooldown { get; private set; }
    public SkillSO Data;
    public float Timer;
    public float Fill;
    private SkillsComponent _caster;

    public Skill(SkillsComponent caster, SkillSO skilldata, float timer=0f) {
        _caster = caster;
        Data = skilldata;
        Timer = timer;
        Cooldown = false;
    }

    public void Cast(SkillsComponent caster, Vector2 direction) {
        if (Data == null) 
            return;

        Cooldown = true;
        OnCast(_caster, direction);
    }

    public void Finish(SkillsComponent caster)
    {
        Timer = 0f;
        Fill = Timer / Data.CooldownDuration;
        Cooldown = false;
        OnFinish(_caster);
    }

    public void Tick(SkillsComponent gameObject) {
        if (!Cooldown) 
            return;

        if (Timer < Data.CooldownDuration) {
            Timer += Time.deltaTime;
            Fill = 1 - Timer / Data.CooldownDuration ;
            OnTick(_caster);
        }
        else
        {
            Finish(_caster);
        }
    }

    public abstract void OnFinish(SkillsComponent caster);
    public abstract void OnTick(SkillsComponent caster);
    public abstract void OnCast(SkillsComponent caster, Vector2 direction);
}