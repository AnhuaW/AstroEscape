using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool enable_gravity = false;
    public bool gravity_enabled = false;
    public float current_gravity_scale = 3f;
    public float force = 3f;
    public float time_interval = 0.5f;
    public float rotation = 10f;
    Rigidbody2D rb;
    Inventory inventory;
    private float orig_height = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inventory.get_energy_level() > 0)
        {
            enable_gravity = !enable_gravity;
            if (enable_gravity)
            {
                //inventory.decrease_energy(1);
                orig_height = transform.position.y;
            }
        }

        if(enable_gravity && !gravity_enabled)
        {
            StartCoroutine(EnableGravity());
        }

        if (enable_gravity)
        {
            rb.gravityScale = 10f;
        }

        if(!enable_gravity && gravity_enabled)
        {
            StartCoroutine(DisableGravity());
        }
    }

    public IEnumerator EnableGravity()
    {
        //immediately turns it off
        if(GetComponent<FloatingEffect>() != null)
        {
            GetComponent<FloatingEffect>().enabled = false;
        }
        rb.gravityScale = current_gravity_scale;
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 50f;
        rb.SetRotation(0);
        gravity_enabled = true;
    }

    public IEnumerator DisableGravity()
    {
        //TODO
        if (GetComponent<FloatingEffect>() != null)
        {
            GetComponent<FloatingEffect>().enabled = true;
        }
        while (transform.position.y < orig_height)
        {
            rb.gravityScale = -force;
            rb.SetRotation(rotation);
            yield return null;
        }
        rb.gravityScale = 0;
        rb.SetRotation(0);
        gravity_enabled = false;
    }

   void Rotate()
    {
        rb.SetRotation(force);
    }
}
