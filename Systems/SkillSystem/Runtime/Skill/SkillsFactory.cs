using System;
using System.Collections.Generic;

public static class SkillsFactory  {
    public static Dictionary<Type, Func<SkillSO, SkillsComponent, Skill>> Skills = new() { 
		  { typeof(BlinkSkillSO),           (data, user) => new BlinkSkill(user, data)},
    };
}