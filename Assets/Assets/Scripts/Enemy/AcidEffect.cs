using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    //destroy this after 5 seconds
    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    //Move right at 3 meters per second
    private void Update()
    {
        transform.Translate(Vector3.right * 3 * Time.deltaTime);
    }

    //detect player and deal damage (IDamageable Interface)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
