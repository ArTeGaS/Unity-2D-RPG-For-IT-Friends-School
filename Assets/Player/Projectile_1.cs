using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile_1 : MonoBehaviour
{
    public float projectileSpeed;  // �������� ��������

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.z = 0; // ��������� ��������� z, ������� �������� � 2D
        direction = direction.normalized;

        // ������� ������ � �������� ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        rb.velocity = direction * projectileSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������, �� �� �������� ��� "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ��������� ��, ��� ������� �������� ��� ���� ������
            Debug.Log("������ ������!");

            // �������, ������ ������ ��� ������� �����
            Destroy(collision.gameObject);

            // ������ ��������
            Destroy(gameObject);
        }
    }
}
