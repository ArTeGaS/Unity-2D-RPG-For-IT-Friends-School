using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile_1 : MonoBehaviour
{
    public float projectileSpeed;  // Швидкість проєктиля

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.z = 0; // Видаляємо компонент z, оскільки працюємо в 2D
        direction = direction.normalized;

        // Поворот ворога у напрямку гравця
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        rb.velocity = direction * projectileSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Перевірте, чи має колайдер тег "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Виконайте дію, яка повинна відбутися при ударі ворога
            Debug.Log("Влучив ворога!");

            // Можливо, знищте ворога або нанесіть шкоду
            Destroy(collision.gameObject);

            // Знищте проєктиль
            Destroy(gameObject);
        }
    }
}
