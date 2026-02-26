using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsBehaviour : MonoBehaviour
{
    public SkillsComponent Skills { get; set; }
    [SerializeField] SkillSO[] _skills = new SkillSO[4];

    void Awake() {
        Skills = new(this, _skills);
    }

    void Start() {
    }
    
    void Update() {
        Skills.Update();
    }

}