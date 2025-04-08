using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkill : MonoBehaviour
{
    public SkillCondition Skill_A;
    public SkillCondition Skill_B;
    public SkillCondition Skill_C;

    private void Start()
    {
        //스킬매니저 안에 이 UI를 집어넣기
        //SkillManager.Instance.uiSkill=this;
    }
}
