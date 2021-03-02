using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public AudioClip crash;
    public AudioClip[] accelerates;
    public GameObject leftSignal, rightSignal, explosion, exhaust;
    public float speed = 15f, force = 800f;
    private float rotatePosY, rotateMultRight = 6f, rotateMultLeft = 4.5f;
    private Rigidbody rb;
    public bool rightTurn, turnLeft, moveFromUp;
    [NonSerialized] public bool carPassed;
    [NonSerialized] public static bool isLose;
    private bool isMovingFast, carCrashed;
    public LayerMask carsLayer;
    [NonSerialized] public static int countCars;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotatePosY = transform.eulerAngles.y;
       if (rightTurn)
         StartCoroutine(turnSignals(rightSignal));
       else if (turnLeft)
           StartCoroutine(turnSignals(leftSignal));
    }
    IEnumerator turnSignals(GameObject turnSignal)
    {
        while (!carPassed)
        {
            turnSignal.SetActive(!turnSignal.activeSelf);
            yield return new WaitForSeconds(0.4f);
        }
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }
    private void Update()
    {
#if UNITY_EDITOR
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
#else
        if (Input.touchCount > 0)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f, carsLayer))
        {
            string carName = hit.transform.gameObject.name;
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !isMovingFast && gameObject.name == carName) {

#else       
                if (Input.GetTouch(0).phase == TouchPhase.Began && !isMovingFast && gameObject.name == carName) {
#endif
                GameObject vfx = Instantiate(exhaust, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z)
                    ,Quaternion.Euler(90,0,0));
                Destroy(vfx, 2f);
                speed *= 2;
                isMovingFast = true;
                if (PlayerPrefs.GetString("music") != "No")
                {
                    GetComponent<AudioSource>().clip = accelerates[Random.Range(0, 3)];
                    GetComponent<AudioSource>().Play();
                }
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Car") && !carCrashed)
        {
            isLose = true;
            speed = 0;
            collision.gameObject.GetComponent<CarController>().speed = 0;
            GameObject vfx = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(vfx, 5f);
            rb.AddRelativeForce(Vector3.back * force);
            if (isMovingFast)
                force *= 1.5f;
            carCrashed = true;

            if (PlayerPrefs.GetString("music") != "No")
            {
                GetComponent<AudioSource>().clip = crash;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (carCrashed)
            return;
        if (other.transform.CompareTag("TurnRight") && rightTurn)
            RotateCar(rotateMultRight);

        else if (other.transform.CompareTag("TurnLeft") && turnLeft)
            RotateCar(rotateMultLeft, -1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Car") && other.GetComponent<CarController>().carPassed)
        {
            other.gameObject.GetComponent<CarController>().speed = speed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (carCrashed)
            return;
        if (other.transform.CompareTag("Trigger Pass"))
        {
            if (carPassed)
                return;
            Collider[] boxes = GetComponents<BoxCollider>();
            carPassed = true;
            foreach (Collider col in boxes)
                col.enabled = true;

            countCars++;
        }
        if (other.transform.CompareTag("TurnRight") && rightTurn)
            rb.rotation = Quaternion.Euler(0, rotatePosY + 90, 0);
        else if (other.transform.CompareTag("TurnLeft") && turnLeft)
            rb.rotation = Quaternion.Euler(0, rotatePosY - 90, 0);
        else if (other.transform.CompareTag("Delete Car"))
            Destroy(gameObject);
    }
    void RotateCar(float speedRotate, int dir = 1)
    {
        if (carCrashed)
            return;
        if (dir == -1 && transform.localRotation.eulerAngles.y < rotatePosY - 90f)
            return;
        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250 && transform.localRotation.eulerAngles.y < 270)
            return;

        float rotateSpeed = speed * speedRotate * dir;
        Quaternion rot = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * rot);
    }

    



}
