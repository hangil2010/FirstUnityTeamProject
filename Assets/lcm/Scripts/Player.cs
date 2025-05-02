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
    [SerializeField] private int maxhealth;
    private int culhealth;
    public int CulHealth { get { return culhealth; } set { culhealth = value; } }
    [SerializeField] private float damage = 10f;
    public float Damage { get { return damage; } set { damage = value; } }

    

    [SerializeField] Attack attack;
    [SerializeField] private float attackRate = 0.5f; //���ݼӵ�
    private float nextAttackTime = 0f;

    private bool wDown;
    private bool jDown;

    private bool isSide;
    private bool isDodge;
    private bool isDamage = false;

    

    private GameObject nearObject;
    private Invincible invincibleScript;


    Rigidbody rigid;
    MeshRenderer[] meshs; 

    Vector3 moveVec;
    Vector3 sideVec;
    Vector3 dodgeVec;

    private static Player instance = null;

    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Player>();

                if(instance != null)
                {
                    Debug.LogError("Player ������Ʈ�� ���� �����ϴ�.");
                }
            }

            return instance;
        }
    }

    Animator anim;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;



        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        invincibleScript = GetComponent<Invincible>();
    }
    // Start is called before the first frame update
    void Start()
    {
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
        else if (Input.GetKey(KeyCode.Q))
        {
            if (Time.time >= nextAttackTime)
            {
                attack.Fire(damage);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // ���� ���°� �ƴ� ���� �������� ����
        if (invincibleScript != null && !invincibleScript.isInvincible)
        {
            CulHealth -= damageAmount;
            Debug.Log($"�÷��̾� �ǰ�! ���� ������: {damageAmount}, ���� ü��: {CulHealth}");

            // �ǰ� �� ���� ���� ���� (���� ����)
            invincibleScript.StartInvincible();

            if (CulHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("�÷��̾� ���� ���·� �������� ���� ����!");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            MonsterBullet enemyBullet = other.GetComponent<MonsterBullet>();
            CulHealth -= enemyBullet.damage;
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color= Color.red;
        }
        yield return new WaitForSeconds(1f);

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
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
                    activeItems.RemoveAt(index);
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
