using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;
    public GameObject shellSpawn;
    public float speed;
    public float rotateSpeed = 20;
    float yAngle = 0;
    Vector3 hitPosition = Vector3.zero;

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObj = hitInfo.collider.gameObject;
            Transform temp = hitObj.GetComponent<Collider>().transform.root;
            if (temp.gameObject.name == "LevelArt")
            {
                hitPosition = hitInfo.point;
            }
        }

        if (hitPosition != Vector3.zero)
        {
            float? upAngle = calculateAngle(hitPosition);
            if (upAngle != null)
            {
                yAngle = (float)upAngle;
            }
        }
        Vector3 direction = (hitPosition - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, yAngle, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
    float? calculateAngle(Vector3 targetPos)
    {
        Vector3 targetDirection = targetPos - shellSpawn.transform.position;
        float y = targetDirection.y;
        float x = targetDirection.magnitude;
        float gravity = 9.8f;
        float speedSqr = speed * speed;
        float squareRoot = (speedSqr * speedSqr) - gravity * (gravity * x * x + 2 * y * speedSqr);
        if (squareRoot >= 0)
        {
            float root = Mathf.Sqrt(squareRoot);
            //float highAngle = speedSqr + root;
            float lowAngle = speedSqr - root;
            return (Mathf.Atan2(lowAngle, gravity * x));
        }
        return null;
        
    }
}
