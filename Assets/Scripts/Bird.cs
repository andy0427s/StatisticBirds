using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround;
    bool _reset;
    [SerializeField] private float _launchPower = 500;

    // Reference to the BirdManager
    private BirdManager birdManager;

    private void Awake() {
        _initialPosition = transform.position;

        // Find the BirdManager in the scene
        birdManager = FindObjectOfType<BirdManager>();
    }

    private void Update() {
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (_birdWasLaunched && 
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f)
            {
                _timeSittingAround += Time.deltaTime;
            }

        if (transform.position.y > 20 ||
            transform.position.y < -20 ||
            transform.position.x > 20 ||
            transform.position.x < -20 ||
            _timeSittingAround > 3)
            {
                _reset = true;
            // string currentName = SceneManager.GetActiveScene().name;
            // SceneManager.LoadScene(currentName);
            }

        if (_reset)
            {
                // Instead of resetting the bird, we now spawn a new bird and destroy the current one
                birdManager.SpawnBird(_initialPosition, transform);
                Destroy(gameObject);
            }
    }
    private void OnMouseDown() 
    {
        GetComponent<SpriteRenderer>().color = Color.red;   
        GetComponent<LineRenderer>().enabled = true; 
    }

    private void OnMouseUp() 
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag() {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);

    }
}
