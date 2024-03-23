using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    private GameObject playerObj;
    private Transform player; // ��������� �� ��'��� ������
    public float moveSpeed = 5f; // �������� ���� ������

    private Rigidbody2D rb;
    private bool isMoveing = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // ��������, �� ���� ��������� �� ������
        if (player != null && isMoveing)
        {
            // ���������� ������� �������� �� ������
            Vector2 direction = player.position - transform.position;
            direction.Normalize(); // ����������� ��� ��������� ���������� �������

            // ������� ������ � �������� ������
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            // ��� ������ � �������� ������
            //transform.Translate(direction * moveSpeed * Time.fixedDeltaTime, Space.World);
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == player)
        {
            // ���� ����� �������� � �������
            CamMove.hitPoints -= 10;
            StartCoroutine(StopMoving());
            Debug.Log(CamMove.hitPoints);
            // ��� ����� ������ ����� ��������� ����� �������
        }
    }

    IEnumerator StopMoving()
    {
        isMoveing = false;
        yield return new WaitForSeconds(3f);
        isMoveing = true;
    }
}
