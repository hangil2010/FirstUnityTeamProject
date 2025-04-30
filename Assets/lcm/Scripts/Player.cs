using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private float hAxis;
    private float vAxis;

    [SerializeField] private float speed;
    [SerializeField] private List<Item> passiveItems = new List<Item>();
    [SerializeField] private List<Item> activeItems = new List<Item>();
    [SerializeField] private int health;
    public int Health { get { return health; } set { health = value; } }
    [SerializeField] private int damage = 10;
    public int Damage { get { return damage; } set { damage = value; } }
    [SerializeField] Attack attack;

    private bool wDown;
    private bool jDown;

    private bool isSide;
    private bool isDodge;

    private bool isInvincible = false;
    [SerializeField] private float invincibleDuration = 0.5f;

    private GameObject nearObject;


    Rigidbody rigid;

    Vector3 moveVec;
    Vector3 sideVec;
    Vector3 dodgeVec;

    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AcquireItem(new SadOnion());
        AcquireItem(new Pentagram());
        ApplyPassiveEffects();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        Move();
        Turn();
        Dodge();
        Attack();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    private void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

        if (isSide && moveVec == sideVec)
            moveVec = Vector3.zero;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    private void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    

    private void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.4f);
        }
    }

    private void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            attack.Fire(damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // ���� ���°� �ƴ� ���� �������� ����
        if (!isInvincible)
        {
            health -= damageAmount;
            Debug.Log($"�÷��̾� �ǰ�! ���� ������: {damageAmount}, ���� ü��: {health}");

            // �ǰ� �ִϸ��̼� ��� (���� ����)
            //if (anim != null)
            //{
            //    anim.SetTrigger("doHit");
            //}

            // �ǰ� �� ���� ���� ���� (���� ����)
            StartInvincible();

            // ü���� 0 ���Ϸ� �������� �� ��� ó��
            if (health <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("�÷��̾� ���� ���·� �������� ���� ����!");
        }
    }

    private void StartInvincible()
    {
        isInvincible = true;
        // ���� �ð� �� ���� ���� ����
        Invoke("EndInvincible", invincibleDuration);
        // �ʿ��ϴٸ� ���� ���� �� �ð��� ȿ���� �� ���� �ֽ��ϴ�.
    }

    private void EndInvincible()
    {
        isInvincible = false;
        // ���� ���� ���� �� �ð��� ȿ���� ������ �� �ֽ��ϴ�.
    }

    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("isJump", false);

         // ������ ���� ó�� (�浹 ����)
         if (collision.gameObject.GetComponent<ItemPickup>() != null)
         {
            ItemPickup pickup = collision.gameObject.GetComponent<ItemPickup>();
            AcquireItem(pickup.item);
            Destroy(collision.gameObject); // ������ ������ ������Ʈ �ı�
            ApplyPassiveEffects(); // ���� �� �нú� ȿ�� �ٽ� ����
            Debug.Log("������ ȹ��: " + pickup.item.itemName);
         }



    }

    public void AcquireItem(Item newItem)
    {
        if(newItem.itemType == itemType.Passive)
        {
            passiveItems.Add(newItem);
            ApplyPassiveEffects();
        }
        else if(newItem.itemType == itemType.Active)
        {
            activeItems.Add(newItem);

        }
        Debug.Log("������ ȹ�� :" + newItem.itemName + " (" + newItem.itemType + ")");
    }

    private void ApplyPassiveEffects()
    {
        // ����� �����ϰ� �α׸� ���, ���� ȿ�� ���� ���� ���� �ʿ�
        foreach (Item item in passiveItems)
        {
            if (item.itemType == itemType.Passive)
            {
                Debug.Log("�нú� ������ ȿ�� ����: " + item.itemName);
                item.UseItem(); // �� �������� UseItem() ȣ�� (���� ȿ�� ����)
            }
        }
    }


    // ������ ��� �Լ� (�ε��� ���)
    public void UseItem(int index, itemType type)
    {
        List<Item> targetList = (type == itemType.Active) ? activeItems : passiveItems;

        if (index >= 0 && index < targetList.Count)
        {
            if (targetList[index].itemType == type)
            {
                Debug.Log("��Ƽ�� ������ ���: " + targetList[index].itemName);
                targetList[index].UseItem(); // ��Ƽ�� �������� UseItem() ȣ�� (���� ȿ�� ����)
                // ��� �� ������ ���� �Ǵ� ��Ÿ�� ó�� �� �߰� ���� �ʿ�
                if (type == itemType.Active)
                {
                    // ����: ��� �� ù ��° ��Ƽ�� ������ ����
                    // activeItems.RemoveAt(index);
                    // UpdateActiveItemUI();
                }
            }
            else
            {
                Debug.Log("�ش� ������ " + type + " �������� �ƴմϴ�.");
            }
        }
        else
        {
            Debug.Log("�ش� �ε����� �������� �����ϴ�.");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDodge)
        {
            health -= damageAmount;
            Debug.Log("�÷��̾� �ǰ�! ���� ü��: " + health);

            if(health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("�÷��̾� ���!");
    }

    // UI ������Ʈ (�ӽ�)
    private void UpdatePassiveItemsUI()
    {
        // ���� UI �ý��ۿ� �°� �����ؾ� ��
        Debug.Log("���� ���� ������:");
        for (int i = 0; i < passiveItems.Count; i++)
        {
            Debug.Log($"{i + 1}: {passiveItems[i].itemName} ({passiveItems[i].itemType})");
        }
    }

}
