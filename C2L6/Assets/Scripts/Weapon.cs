using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject Shooter;
    public GameObject Parent;

    private Transform _firePoint;

    // Start is called before the first frame update
    void Start()
    {
        _firePoint = transform.Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;

        Bullet bulletComponent = myBullet.GetComponent<Bullet>();
        
        if(Parent == null)
            if (Shooter.transform.localScale.x <0f)
            {
                bulletComponent.direction = Vector2.left; //new Vector2(-1,0)
            }
            else
            {
                bulletComponent.direction = Vector2.right; //new Vector2(0,-1)
            }
        else
        {
            if (Parent.transform.localScale.x < 0f)
            {
                bulletComponent.direction = Vector2.left; //new Vector2(-1,0)
            }
            else
            {
                bulletComponent.direction = Vector2.right; //new Vector2(0,-1)
            }
        }
    }

}
