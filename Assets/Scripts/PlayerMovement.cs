using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    float rotationSpeed = 720f;

    GameObject carriedItem;

    public int money = 0;
    public LayerMask itemMask;
    public LayerMask gridMask;
    public GameManager gameManager;
    [SerializeField] Rigidbody rb;
    private int cameraPerspective = 0;
    public Transform mainCamera;
    public float distanceToItem;
    public Vector3 cameraTopDownPosition;
    public Vector3 cameraTopDownRotation;
    public Vector3 cameraPerspectivePosition;
    public Vector3 cameraPerspectiveRotation;

    public TMPro.TextMeshProUGUI moneyText;

    public Animator animator;
    

    // Update is called once per frame
    void Update()
    {
        SetMoney();

        Vector3 origin = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeCameraPerspective();
        }

        if (carriedItem != null)
        {
            animator.SetBool("carrying", true);

            carriedItem.transform.position = transform.position + transform.forward;
            carriedItem.transform.rotation = transform.rotation;

            Collider[] objects = Physics.OverlapSphere(origin, 1f, gridMask);

            if (objects.Length > 0)
            {
                float minDistance = 100f;
                int id = -1;
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i].transform.childCount == 0)
                    {
                        float distance = Vector3.Distance(origin, objects[i].transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            id = i;
                        }
                    }
                }
                if (id != -1)
                {
                    distanceToItem = Vector3.Distance(objects[id].gameObject.transform.position, transform.position);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PlaceItem(objects[id].gameObject);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropItem();
            }
        }
        else
        {
            animator.SetBool("carrying", false);

            Collider[] objects = Physics.OverlapSphere(origin, 1f, itemMask);

            if (objects.Length > 0)
            {
                float minDistance = 100f;
                int id = -1;
                for (int i = 0; i < objects.Length; i++) {
                    float distance = Vector3.Distance(origin, objects[i].transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        id = i;
                    }
                }
                if (id != -1)
                {
                    distanceToItem = Vector3.Distance(objects[id].gameObject.transform.position, transform.position);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        InteractItem(objects[id].gameObject);
                    }
                    else if (Input.GetKeyDown(KeyCode.Q))
                    {
                        SellItem(objects[id].gameObject);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        rb.MovePosition(transform.position + moveDirection * Time.deltaTime * speed);

        if (moveDirection != new Vector3(0, 0, 0))
        {
            animator.SetBool("moving", true);

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        } else
        {
            animator.SetBool("moving", false);
        }
    }

    void InteractItem(GameObject item)
    {
        if (item.CompareTag("Box"))
        {
            carriedItem = item;
        }
        else if (item.CompareTag("Station"))
        {
            money = item.GetComponent<Station>().Interact(money);
        }
        else
        {
            money = item.GetComponent<Item>().Upgrade(money);
        }
    }

    void SellItem(GameObject item)
    {
        money += item.GetComponent<Item>().value;
        if(item.transform.parent.CompareTag("Grid"))
            item.transform.parent.GetComponent<Renderer>().material.color = Color.red;
        Destroy(item);
    }

    void PlaceItem(GameObject grid)
    {
        Instantiate(gameManager.towers[carriedItem.GetComponent<Item>().type * 2 + 1], grid.transform);
        Destroy(carriedItem);
        carriedItem = null;
    }

    void DropItem()
    {
        carriedItem.transform.position = new Vector3(carriedItem.transform.position.x, 1f, carriedItem.transform.position.z);
        carriedItem = null;
    }

    void ChangeCameraPerspective()
    {
        if (cameraPerspective == 0)
        {
            cameraPerspective = 1;
            mainCamera.position = cameraTopDownPosition;
            mainCamera.eulerAngles = cameraTopDownRotation;

        } else
        {
            cameraPerspective = 0;
            mainCamera.position = cameraPerspectivePosition;
            mainCamera.eulerAngles = cameraPerspectiveRotation;
        }
    }

    void SetMoney()
    {
        moneyText.text = "Money: " + money;
    }
}
