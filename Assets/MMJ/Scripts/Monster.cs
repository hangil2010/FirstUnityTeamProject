using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "���ݼ���") //todo
        {
            //���ݼ���(�±�����) ���ݼ����̸� = other.GetComponent<���ݼ���>();
            //curHealth -= ���ݼ��ܰ��ݷ�;
            Vector3 reactVec = transform.position - other.transform.position;

            //Destroy(other.gameObject); //(�Ѿ� ���� ����ü�ϰ�� �¾����� ����)

            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    { 
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 21;

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            Destroy(gameObject, 4);
        }
    }
}
