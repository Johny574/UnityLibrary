using NUnit.Framework;
using UnityEngine;

public class SkillTests  {

    SkillsComponent _skills;
    Skill _mockSkill;
    SkillSO _mockSkillSO;
    [SetUp]
    public void SetUp() {
        _skills = new(null, new SkillSO[1]);
        _mockSkillSO = ScriptableObject.CreateInstance<MockSkillSO>();
        _mockSkill = new MockSkill(null, _mockSkillSO);
    }
    
    [Test]
    public void SkillAdded() {
        Assert.Null(_skills.Skills[0]);
        _skills.Add(_mockSkill, 0);
        Assert.NotNull(_skills.Skills[0]);
    }

    [Test]
    public void SkillRemoved() {
        Assert.Null(_skills.Skills[0]);
        _skills.Add(_mockSkill, 0);
        Assert.NotNull(_skills.Skills[0]);
        _skills.Remove(0);
        Assert.Null(_skills.Skills[0]);
    }

    [Test]
    public void SkillCooldownOnCast() {
        Assert.Null(_skills.Skills[0]);
        _skills.Add(_mockSkill, 0);
        Assert.NotNull(_skills.Skills[0]);
        _skills.Skills[0].Cast(null, Vector2.zero);
        Assert.IsTrue(_skills.Skills[0].Cooldown);
    }

    [Test]
    public void SkillsSavedAndLoaded()
    {
       Assert.Null(_skills.Skills[0]);
        _skills.Add(_mockSkill, 0);
        Assert.NotNull(_skills.Skills[0]);
        Serializer.SaveFile(_skills.Save(), "Skills", Serializer.SaveSlot.AutoSave);
        _skills = new(null, new SkillSO[1]);
        SkillData[] skills = Serializer.LoadFile<SkillData[]>("Skills", Serializer.SaveSlot.AutoSave);
        Assert.NotNull(skills[0]);
    }
}

public class MockSkill : Skill
{
    public MockSkill(SkillsComponent caster, SkillSO skilldata, float timer = 0) : base(caster, skilldata, timer)
    {
    }

    public override void OnCast(SkillsComponent caster, Vector2 direction)
    {
    }

    public override void OnFinish(SkillsComponent caster)
    {
    }

    public override void OnTick(SkillsComponent caster)
    {
    }
}

public class MockSkillSO : SkillSO {
    
}