using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float xPadding;
    [SerializeField] private float yPadding;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 15f;
    [SerializeField] private float projectileFiringPeriod = 0.2f;

    private Coroutine firingCoroutine;
    
    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();
    }


    private void CalculatePlayerSpritePadding()
    {
        Vector2 spriteSize = GetComponent<SpriteRenderer>().bounds.size;
        xPadding = spriteSize.x / 2;
        yPadding = spriteSize.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var myPosition = transform.position;

        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(myPosition.x + deltaX, _xMin, _xMax);
        var newYPos = Mathf.Clamp(myPosition.y + deltaY, _yMin, _yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundries()
    {
        CalculatePlayerSpritePadding();

        Camera gameCamera = Camera.main;
        if (gameCamera != null)
        {
            _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
            _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
            _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
            _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
        }
    }
}