using UnityEngine;

public class BlinkSkill : Skill {
    private BlinkSkillSO _skillData;
    float _trailtime;
    bool _emitting;
    GameObject _caster;
    TrailRenderer _trial;

    public BlinkSkill(SkillsComponent caster, SkillSO skilldata, float timer = 0) : base(caster, skilldata, timer)
    {
    }

    public override void OnTick(SkillsComponent caster) {
        if (!_emitting)
            return;

        if (_trailtime < .5f) {
            _trailtime += Time.deltaTime;
        }
        else {
            _emitting = false;
            _trailtime = 0f;
            // caster.GetComponent<TrailRenderer>().emitting = false;
        }
    }

    public override void OnCast(SkillsComponent caster, Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(caster.Behaviour.transform.position, direction, _skillData.Distance, _skillData.WallLayer);

        if (hit) {
            caster.Behaviour.transform.position = hit.point;
            return;
        }

        // _trial.emitting = true;
        caster.Behaviour.transform.position = (Vector2)caster.Behaviour.transform.position + direction * _skillData.Distance;
        // _trial.emitting = false;
    }

    public override void OnFinish(SkillsComponent caster)
    {
       
    }
}