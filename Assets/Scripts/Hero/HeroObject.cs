using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ���� ���ӿ�����Ʈ : �������� ������ / ���� ������ �̴� / ���� / �̵� / HP���� / ����
public class HeroObject : SerializedMonoBehaviour
{
    [Title("������ ��ü", bold: false)]
    // ���� ������Ʈ�� �� ������
    [SerializeField]
    private readonly HeroData _baseHeroData;

    // ������Ʈ�� �Ҵ�� ������
    private HeroData _objHeroData;


    [Title("�ʿ��� ������Ʈ��", bold: false)]
    // ������ٵ�
    private Rigidbody2D _rigid;

    // ��������Ʈ ������
    private SpriteRenderer _spriteRenderer;

    // �̵��� ����� vector2 dir
    private Vector2 _moveDir;

    // HP��
    private HPBar _hpBar;


    private void FixedUpdate()
    {
        if (PlaySceneManager.Instance.IsGameStart)
        {
            Move();
            Attack();
        }            
    }

    // ������ ����
    public void DataSetting()
    {
        // ������ �־��ֱ�
        _objHeroData = _baseHeroData;

        // �ʿ� ������Ʈ�� �����ͼ� �Ҵ�
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;

        // HP�ٵ� �Ҵ�
        _hpBar = Instantiate(PlaySceneManager.Instance.HPBar, transform.position, Quaternion.identity);
        _hpBar.SetParent(this.transform);
        _hpBar.Update_HPSlider(_objHeroData.Hp, _baseHeroData.Hp);
    }


    // �̵�
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _moveDir.x = _rigid.position.x + (horizontal * _objHeroData.Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * _objHeroData.Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);        
    }


    // ����
    private void Attack()
    {
        // ��ų����Ʈ�� �ƹ��͵� ������ �׳� ����
        if (HeroEquipedSkill.EquipedSkillList.Count == 0)
        {
            return;
        }

        // ��ų����Ʈ�� �ִ� ��� ��ų�� ��ȯ, Ȯ��, ����
        for (int i = 0; i < HeroEquipedSkill.EquipedSkillList.Count; i++)
        {
            SkillBase skill = HeroEquipedSkill.EquipedSkillList[i];

            if (skill.SkillUpdate())
            {
                SkillAttackArgs skillAttackArgs = new SkillAttackArgs() { StartSkillPos = transform.position };
                skill.AttackFunc(skillAttackArgs);
            }
        }
    }

    // HP ����
    public void HPMinus(int atk)
    {
        _objHeroData.Hp -= atk;

        // ��������Ʈ �����̱�
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        Debug.Log($"_objHeroData.Hp : {_objHeroData.Hp}");
        Debug.Log($"_baseHeroData.Hp : {_baseHeroData.Hp}");

        _hpBar.Update_HPSlider(_objHeroData.Hp, _baseHeroData.Hp);

        if (_objHeroData.Hp < 0)
            Death();
    }

    // ����
    private void Death()
    {
        //Debug.Log("���ӿ���!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
