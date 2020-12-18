using UnityEngine;

public class FliesScript : MonoBehaviour
{
    Vector3 primaryPosition;
    private void Start()
    {
        primaryPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
        if (transform.position.x >= primaryPosition.x + 2f) transform.position = new Vector3(primaryPosition.x + 2f, transform.position.y, 0f);
        if (transform.position.y >= primaryPosition.y + 2f) transform.position = new Vector3(transform.position.x, primaryPosition.y + 2f, 0f);
        if (transform.position.x <= primaryPosition.x - 2f) transform.position = new Vector3(primaryPosition.x - 2f, transform.position.y, 0f);
        if (transform.position.y <= primaryPosition.y - 2f) transform.position = new Vector3(transform.position.x, primaryPosition.y - 2f, 0f);
    }
}
