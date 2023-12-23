using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float speed;
    public float dashSpeed; // �������� �� ��� "dash"
    public float dashDuration; // ��������� "dash"
    public static int hitPoints = 100;

    private Rigidbody2D rb;
    private bool isDashing = false;

    public GameObject player;
    public GameObject mainCam;

    public Sprite spriteAbove; // ������, ���� ������ ���� ������
    public Sprite spriteBelow; // ������, ���� ������ ����� ������

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (Input.GetKey(KeyCode.Space) && !isDashing)
        {
            // ��������� ������� Dash
            Dash(move);
        }
        else
        {
            // ��������� ���
            rb.velocity = move * speed;
        }

        mainCam.transform.position = new Vector3(
            player.transform.position.x, player.transform.position.y, mainCam.transform.position.z);

        UpdatePlayerSprite();
    }

    void Dash(Vector2 direction)
    {
        // ���������, �� �� � "dash"
        isDashing = true;

        // ������� �������� �� ��� "dash"
        speed = speed * 4;

        // ��������� ������ ��� ���������� "dash"
        StartCoroutine(EndDash());
    }

    IEnumerator EndDash()
    {
        // ������ ������� ���, ��� ��������� "dash"
        yield return new WaitForSecondsRealtime(dashDuration);

        speed = speed / 4;

        yield return new WaitForSecondsRealtime(3f);

        // �������� �������� ��� ��, �� "dash" ���������
        isDashing = false;
    }

    void UpdatePlayerSprite()
    {
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (cursorWorldPos.y > player.transform.position.y)
        {
            spriteRenderer.sprite = spriteAbove;
        }
        else
        {
            spriteRenderer.sprite = spriteBelow;
        }
    }
}
