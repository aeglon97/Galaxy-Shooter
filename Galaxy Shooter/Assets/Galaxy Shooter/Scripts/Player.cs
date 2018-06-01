using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool canSpeedBoost = false;

    [SerializeField]
    private float _speed = 6.0f;
    private float _fireRate = 0.25f;
    private float _canFire = 0;

    public GameObject laserPrefab;
    public GameObject tripleShotPrefab;

	// Use this for initialization
	private void Start ()
    { 
        transform.position = new Vector3(0, 0, 0);
	}

    // Update is called once per frame
    private void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        //moving the player
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (canSpeedBoost)
        {
            transform.Translate(Vector3.up * _speed * 3.0f * verticalInput * Time.deltaTime);
            transform.Translate(Vector3.right * _speed * 3.0f * horizontalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        }
        

        //setting player bounds
        if (transform.position.x < -7.9f)
        {
            transform.position = new Vector3(-7.9f, transform.position.y, 0);
        }
        else if (transform.position.x > 7.9f)
        {
            transform.position = new Vector3(7.9f, transform.position.y, 0);
        }

        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        else if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (canTripleShot)
            {
                Instantiate(laserPrefab, transform.position + new Vector3(-.7f, 0, 0), Quaternion.identity);
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                Instantiate(laserPrefab, transform.position + new Vector3(.7f, 0, 0), Quaternion.identity);
            }
            //cooldown
            if (Time.time > _canFire)
            {
                _canFire = Time.time + _fireRate;
                //spawn laser
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
        }
    }

    //triple shot logic
    public void TripleShotOn()
    {
        canTripleShot = true;
        StartCoroutine("TripleShotPowerDownRoutine");
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    //speed boost logic
    public void SpeedBoostOn()
    {
        canSpeedBoost = true;
        StartCoroutine("SpeedBoostPowerDownRoutine");
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }
}


