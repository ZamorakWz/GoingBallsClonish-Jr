using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : MonoBehaviour
{
    private HealthManager _healthManager;
    // Start is called before the first frame update
    void Start()
    {
        _healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_healthManager.health <= 4 && _healthManager.health >= 0)
            {
                _healthManager.health++;
                //_healthManager.SaveHealth();
            }

            Destroy(this.gameObject);
        }
    }


}
