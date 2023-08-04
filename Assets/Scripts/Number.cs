using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    [SerializeField] private GameObject _cloudParticlePrefab;

    private int value;
    private bool isAdded = false; 
    private void Start() 
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        value = int.Parse(sprite.name);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Bird bird = collision.collider.GetComponent<Bird>();
        if (bird != null) 
        {
            if (!isAdded)
            {
                GameManager.instance.AddNumber(value);
                isAdded = true;  // 將布爾變量設置為true
                Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
                GameManager.instance.CheckNumbers();
            }
            Destroy(gameObject);
        }    

        Number number = collision.collider.GetComponent<Number>();
        if (number != null)
        {
            return;
        }

        if ( collision.contacts[0].normal.y < -0.5)
        {
            if (!isAdded)
            {
                GameManager.instance.AddNumber(value);
                isAdded = true;  // 將布爾變量設置為true
                Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
                GameManager.instance.CheckNumbers();
            }
            Destroy(gameObject);
        }
    }
}
