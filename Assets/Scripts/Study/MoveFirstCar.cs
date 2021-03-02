using UnityEngine;

public class MoveFirstCar : MonoBehaviour
{
    public GameObject firstCanvas, secondCar, secondCanvas;
    private bool isFirst;
    private CarController controller;

    private void Start()
    {
        controller = GetComponent<CarController>();
    }
    private void Update()
    {
        if (transform.position.x < 8f && !isFirst)
        {
            controller.speed = 0;
            firstCanvas.SetActive(true);
            isFirst = true;
        }
    }
    private void OnMouseDown()
    {
        if (!isFirst || transform.position.x > 9f)
            return;
        
            controller.speed = 12f;
            firstCanvas.SetActive(false);
            secondCanvas.SetActive(true);
            secondCar.GetComponent<CarController>().speed = 10f;
        
    }
}
