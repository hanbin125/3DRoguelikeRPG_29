using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager _instance;

    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<SkillManager>();
            }
            return _instance;
        }
    }

    public Player player;
    public UISkill uiSkill;
    public bool attacking;
    public float attackRate;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 스킬을 눌렀을 때, 스킬 애니메이션이 시전되며 시전 중임을 알리는 불리언을 활성화키는 메서드
    /// 스킬의 적중은 애니메이션 진행 도중 OnSkillHit 메서드를 발동시킴으로써 적용시킬 것
    /// </summary>
    public void OnSkillInput()
    {
        //if (!attacking && !condition.IsStaminaZero())
        //{
        //    attacking = true;
        //    if (!Skill)
        //    {
        //        animator.SetTrigger("Skill");
        //        Skill = true;
        //    }
        //    else
        //    {
        //        animator.SetTrigger("Skill_Alternative");
        //        Skill = false;
        //    }

        //    Invoke(nameof(OnCanUseSkill), attackRate);
        //}
    }

    /// <summary>
    /// 스킬이 시전되고 나서, 시전 중이라는 불리언을 비활성화시키는 메서드
    /// </summary>
    void OnCanUseSkill()
    {
        attacking = false;
    }

    /// <summary>
    /// 실행될 애니메이션 클립 안에서 호출될 공격 메서드
    /// </summary>
    public void onSkillHit()
    {
        //Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Debug.DrawRay(ray.origin, ray.direction, Color.white);
        //RaycastHit hit;

        //condition.ConsumeStamina(attackStamina);

        //if (Physics.Raycast(ray, out hit, attackDistance, hitLayer))
        //{
        //    Debug.Log(hit.collider.name);
        //    if (hit.collider.TryGetComponent(out IBreakableObject breakbleObject))
        //    {
        //        Debug.Log("실행");
        //        breakbleObject.TakeDamage(nowDamage);
        //    }
        //}
    }

}
