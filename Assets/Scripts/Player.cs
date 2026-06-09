using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform locationLane1;
    public Transform locationLane2;
    public Transform locationLane3;
    public Transform gunpoint;

    public GameObject[] Bullet;
    public GameObject currentBullet;

    private int laneSelect = 2;

    void Start()
    {
        transform.position = new Vector2(locationLane2.position.x, locationLane2.position.y);
        
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
        // Стрільба на Лівий клік миші (або Пробіл)
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        // Зміна типу куль на клавіші 1 (чоловічі) та 2 (жіночі)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetBullet(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetBullet(1);
        }
    }

    public void SetBullet(int index)
    {
        if (index >= 0 && index < Bullet.Length)
        {
            currentBullet = Bullet[index];
        }
    }

    public void Fire()
    {
        // Створюємо кулю напряму, ігноруючи зламані анімації розробника гри
        if (currentBullet != null && gunpoint != null)
        {
            Instantiate(currentBullet, gunpoint.transform.position, Quaternion.identity);
        }
    }
}