using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpSpeed;
    [SerializeField] float _friction;
    [SerializeField] bool _grounded;
    [SerializeField] float _maxSpeed;
    [SerializeField] Transform colliderTransform;
 
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) || _grounded == false)
            colliderTransform.localScale = Vector3.Lerp(colliderTransform.localScale, new Vector3(1f, 0.5f, 1f), 5 * Time.deltaTime);
        else
            colliderTransform.localScale = Vector3.Lerp(colliderTransform.localScale, new Vector3(1f, 1f, 1f), 5 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_grounded)
                rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.VelocityChange);
        }
      
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(0f,0f,0f, ForceMode.VelocityChange);
      
        float speedMultiplier = 1f;
        if (_grounded == false)
        {
            speedMultiplier = 0.2f;

            if (rigidbody.velocity.x > _maxSpeed && Input.GetAxis("Horizontal") > 0)
                speedMultiplier = 0;
            if (rigidbody.velocity.x < -_maxSpeed && Input.GetAxis("Horizontal") < 0)
                speedMultiplier = 0;
        }

        rigidbody.AddForce(Input.GetAxis("Horizontal") * _moveSpeed * speedMultiplier, 0, 0, ForceMode.VelocityChange);

        if (_grounded)
            rigidbody.AddForce(-rigidbody.velocity.x * _friction, 0, 0, ForceMode.VelocityChange); ;
    }


    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            if (angle < 45f)
                _grounded = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }
}
