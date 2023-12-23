using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float speed;
    public float dashSpeed; // Швидкість під час "dash"
    public float dashDuration; // Тривалість "dash"
    public static int hitPoints = 100;

    private Rigidbody2D rb;
    private bool isDashing = false;

    public GameObject player;
    public GameObject mainCam;

    public Sprite spriteAbove; // Спрайт, коли курсор вище гравця
    public Sprite spriteBelow; // Спрайт, коли курсор нижче гравця

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
            // Викликаємо функцію Dash
            Dash(move);
        }
        else
        {
            // Звичайний рух
            rb.velocity = move * speed;
        }

        mainCam.transform.position = new Vector3(
            player.transform.position.x, player.transform.position.y, mainCam.transform.position.z);

        UpdatePlayerSprite();
    }

    void Dash(Vector2 direction)
    {
        // Позначаємо, що ми в "dash"
        isDashing = true;

        // Змінюємо швидкість під час "dash"
        speed = speed * 4;

        // Запускаємо таймер для завершення "dash"
        StartCoroutine(EndDash());
    }

    IEnumerator EndDash()
    {
        // Чекаємо заданий час, щоб завершити "dash"
        yield return new WaitForSecondsRealtime(dashDuration);

        speed = speed / 4;

        yield return new WaitForSecondsRealtime(3f);

        // Забираємо позначку про те, що "dash" завершено
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
