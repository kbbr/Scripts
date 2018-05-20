using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour {

    TimeManager timeManager;
    float t_cnt = 0f;
    SphereCollider spCol;

    void Start()
    {
        Destroy(this.gameObject, 1f);
        spCol = GetComponent<SphereCollider>();
        spCol.enabled = false;
    }
    
    void Update()
    {
        if(t_cnt < 2f) t_cnt += Time.deltaTime;
        if(t_cnt < 0.65)
        {
            spCol.enabled = true;
            transform.localScale = Vector3.zero;
            //spCol.radius = 0f;
        }
        else if(t_cnt > 0.65)
        {
            float tmp_scale = 170 / 7 * t_cnt - 207 / 14;
            //spCol.radius = tmp_scale / 2f;
            transform.localScale = new Vector3(tmp_scale, tmp_scale, tmp_scale);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("enemy");
            timeManager = Camera.main.gameObject.GetComponent<TimeManager>();
            timeManager.slowDown();
        }
    }
    
    private IEnumerable particleCollisionInit()
    {
        yield return new WaitForSeconds(0.65f);

    }
    /*
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("partile");

        timeManager = Camera.main.gameObject.GetComponent<TimeManager>();
        timeManager.slowDown();
    }
    */
}
