using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : Monster
{

    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;

    Vector3 lookVec;
    Vector3 tauntVec;
    public bool isLook;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        anime = GetComponentInChildren<Animator>();

        StartCoroutine(Think());
    }


    void Update()
    {
        if (isDead)
        {
            StopAllCoroutines();
            return;
        }
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
          }
       }  

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
                //�̻��Ϲ߻�
                StartCoroutine(MissileShot());
                break;
            case 2:
            case 3:
                //����������~
                StartCoroutine(RockShot());
                break;
            case 4:
                //�����ϰ� �� �پ��?
                StartCoroutine(Taunt());
                break;

        }
    }

    IEnumerator MissileShot()
    {
        anime.SetTrigger("doShot");
        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMonsterMissile bossMonsterMissileA = instantMissileA.GetComponent<BossMonsterMissile>();
        bossMonsterMissileA.target = target;

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMonsterMissile bossMonsterMissileB = instantMissileB.GetComponent<BossMonsterMissile>();
        bossMonsterMissileB.target = target;

        yield return new WaitForSeconds(2f);
        StartCoroutine(Think());
    }

    IEnumerator RockShot()
    {
        isLook = false;
        anime.SetTrigger("doBigShot");
        Instantiate(bullet, transform.position, transform.rotation);

        yield return new WaitForSeconds(3f);

         isLook = true;
        StartCoroutine(Think());
    }

    IEnumerator Taunt()
    {
        isLook = false;
        anime.SetTrigger("doTaunt");

        tauntVec = target.position;

        Vector3 startPos = transform.position;
        Vector3 endPos = tauntVec;

        float jumpTime = 1.0f;
        float elapsed = 0f;
        boxCollider.enabled = false;
        while (elapsed < jumpTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / jumpTime;


            float height = 5f;
            float parabola = 4 * height * (t - t * t);

            transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * parabola;

            yield return null;
        }

        transform.position = endPos;

        meleeArea.enabled = true;
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);
        isLook = true;

        StartCoroutine(Think());
    }

    private void Trace()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(target.transform.position);
    }
}
