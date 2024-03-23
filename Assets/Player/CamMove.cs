using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CamMove : MonoBehaviour
{
    public float speed;
    public float dashSpeed; // �������� �� ��� "dash"
    public float dashDuration; // ��������� "dash"
    public static float hitPoints = 100;

    private Rigidbody2D rb;
    private bool isDashing = false; // ������

    public GameObject player;
    public GameObject mainCam;

    public Sprite spriteAbove; // ������, ���� ������ ���� ������
    public Sprite spriteBelow; // ������, ���� ������ ����� ������

    private SpriteRenderer spriteRenderer;

    public Slider HitPointBarSlider;
    public TextMeshProUGUI HitPointBarText;

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
            Dash();
        }
        else
        {
            // ��������� ���
            rb.velocity = move * speed;
        }

        mainCam.transform.position = new Vector3(
            player.transform.position.x, player.transform.position.y, mainCam.transform.position.z);

        UpdatePlayerSprite();
        UpdateHitPointBar();
    }

    void Dash()
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
    void UpdateHitPointBar()
    {
        HitPointBarSlider.value = Mathf.Clamp(hitPoints,0,100);
        HitPointBarText.text = $"HP: {HitPointBarSlider.value}";
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
