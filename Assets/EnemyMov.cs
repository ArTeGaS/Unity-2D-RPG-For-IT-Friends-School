using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    public Transform player; // ��������� �� ��'��� ������
    public float moveSpeed = 5f; // �������� ���� ������

    private bool isMoveing = true;

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
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
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
