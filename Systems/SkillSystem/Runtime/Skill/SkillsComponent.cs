using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SkillsComponent 
{
    public Skill[] Skills = new Skill[4];
    public Action<Skill[]> Updated;
    public MonoBehaviour Behaviour { get; private set; }
    public SkillsComponent(SkillsBehaviour behaviour, SkillSO[] skills){
        Skills = new Skill[skills.Count()];
        int index = 0;
        foreach (var skill in skills)
        {
            if (skill != null)
                Skills[index] = SkillsFactory.Skills[skill.GetType()].Invoke(skill, this);
            index++;
        }
        
        Behaviour = behaviour; 
    }

    public void Load(SkillData[] save) {
        Skills = new Skill[4];
        for (int i = 0; i < save.Length; i++) {
            if (save[i].GUID != null) {
                SkillData skill = save[i]; // keep this reference in memory 
                int index = i;  // keep this reference in memory

                UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SkillSO> SkillSO = Addressables.LoadAssetAsync<SkillSO>(new AssetReference(skill.GUID));
                SkillSO.Completed += (skillso) => {
                    if (skillso.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed) {
                        throw new System.Exception($"Failed to load asset {skill.GUID}");
                    }

                    Skills[index] = SkillsFactory.Skills[skill.GetType()].Invoke(skillso.Result, this);
                    Updated.Invoke(Skills);
                };
            }
        }
    }

    public SkillData[] Save() => Skills.Select(x => x == null ? new SkillData(null, 0f) : new SkillData(x.Data.GUID, x.Timer)).ToArray();

    public void Add(Skill skill, int slot) {
        // Skills[slot] = SkillsFactory.Skills[skill.GetType()].Invoke(skill, this);
        Skills[slot] = skill;
        Updated?.Invoke(Skills);
    }

    public void Remove(int index)
    {
        Skills[index] = null;
        Updated?.Invoke(Skills);
    }

    public void Update()
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            if (Skills[i] == null)
                return;

            Skills[i].Tick(this);

            if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), $"Alpha{i + 1}"))) //&& _mana.Data.Amount >= Skills[i].Data.ManaCost)     // plug mana here
            {
                if (Skills[i].Cooldown)
                    return;

                Skills[i].Cast(this, Vector2.up);                               // plug aim here 
                // _mana.Update(0 - Skills[i].Data.ManaCost);       // plug mana here
            }
        }
    }
}