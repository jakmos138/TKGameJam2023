using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 6f;
    GameObject carriedItem;
    public int money = 0;
    public LayerMask itemMask;
    public LayerMask gridMask;
    public GameManager gameManager;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime + transform.position.x, transform.position.y, Input.GetAxis("Vertical") * speed * Time.deltaTime + transform.position.z);

        Vector3 origin = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        if (carriedItem != null)
        {
            carriedItem.transform.position = transform.position + transform.forward;
            carriedItem.transform.rotation = transform.rotation;

            Collider[] objects = Physics.OverlapSphere(origin, 3f, gridMask);

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

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlaceItem(objects[id].gameObject);
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropItem();
            }
        }
        else
        {
            Collider[] objects = Physics.OverlapSphere(origin, 3f, itemMask);

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

                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractItem(objects[id].gameObject);
                } 
                else if(Input.GetKeyDown(KeyCode.Q))
                {
                    SellItem(objects[id].gameObject);
                }
            }
        }
    }

    void InteractItem(GameObject item)
    {
        if (item.CompareTag("Box"))
        {
            carriedItem = item;
        } else {
            money = item.GetComponent<Item>().Upgrade(money);
        }
    }

    void SellItem(GameObject item)
    {
        money += item.GetComponent<Item>().value;
        Destroy(item);
    }

    void PlaceItem(GameObject grid)
    {
        Instantiate(gameManager.towers[carriedItem.GetComponent<Item>().type], grid.transform);
        Destroy(carriedItem);
        carriedItem = null;
    }

    void DropItem()
    {
        carriedItem.transform.position = new Vector3(carriedItem.transform.position.x, 1f, carriedItem.transform.position.z);
        carriedItem = null;
    }
}
