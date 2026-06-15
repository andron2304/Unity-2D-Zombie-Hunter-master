using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public Transform locationLane1;
    public Transform locationLane2;
    public Transform locationLane3;
    public Transform gunpoint;

    public GameObject[] Bullet;
    public GameObject currentBullet;

    private int laneSelect = 2;

    public enum FireMode
    {
        Single,
        Burst,
        Automatic
    }

    public FireMode fireMode;

    void Start()
    {
        transform.position = new Vector2(locationLane2.position.x, locationLane2.position.y);

        fireMode = FireMode.Single;

        Debug.Log("Current Bullet: " + currentBullet.name);
        Debug.Log("Fire Mode: " + fireMode.ToString());
        
        if (Bullet.Length > 0)
        {
            currentBullet = Bullet[0]; 
        }
        laneSelect = 2; 
    }

    void Update()
    {
        MovementInput();
        CombatInput();
    }

    void MovementInput()
    {
        // Рух вгору (W)
        if (Input.GetKeyDown(KeyCode.W))
        {
            laneSelect--;
            if (laneSelect < 1) laneSelect = 1; 
            UpdateLanePosition();
        }
        // Рух вниз (S)
        else if (Input.GetKeyDown(KeyCode.S))
        {
            laneSelect++;
            if (laneSelect > 3) laneSelect = 3; 
            UpdateLanePosition();
        }
    }

    void UpdateLanePosition()
    {
        if (laneSelect == 1)
        {
            transform.position = new Vector2(locationLane1.position.x, locationLane1.position.y);
        }
        else if (laneSelect == 2)
        {
            transform.position = new Vector2(locationLane2.position.x, locationLane2.position.y);
        }
        else if (laneSelect == 3)
        {
            transform.position = new Vector2(locationLane3.position.x, locationLane3.position.y);
        }
    }

    void CombatInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            fireMode = FireMode.Single;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            fireMode = FireMode.Automatic;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            fireMode = FireMode.Burst;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Fire(fireMode);
        }
    }

    public void SetBullet(int index)
    {
        if (index >= 0 && index < Bullet.Length)
        {
            currentBullet = Bullet[index];
        }
    }

    public void Fire(enum FireMode fireMode = FireMode.Single)
    {
        Debug.Log("Fire!");

        switch (fireMode)
        {
            case FireMode.Single:
                Debug.Log("Single Fire");
                Instantiate(currentBullet, gunpoint.position, gunpoint.rotation);
                break;
            case FireMode.Burst:
                Debug.Log("Burst Fire");
                StartCoroutine(BurstFire());
                break;
            case FireMode.Automatic:
                Debug.Log("Automatic Fire");
                StartCoroutine(AutomaticFire());
                break;
        }
    }

    private IEnumerator BurstFire()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(currentBullet, gunpoint.position, gunpoint.rotation);
            yield return new WaitForSeconds(0.1f); // Затримка між пострілами
        }
    }

    private IEnumerator AutomaticFire()
    {
        while (Input.GetMouseButton(1) || Input.GetKey(KeyCode.E))
        {
            Instantiate(currentBullet, gunpoint.position, gunpoint.rotation);
            yield return new WaitForSeconds(0.1f); // Затримка між пострілами
        }
    }
}