using UnityEngine;
using System.Collections;
using planTopia;


public class Fireball : MonoBehaviour {
    public bool pushOnAwake = true;
    public Vector3 startDirection;
    public float startMagnitude;
    public ForceMode forceMode;

    public GameObject fieryEffect;
    public GameObject smokeEffect;
    public GameObject explodeEffect;

    protected Rigidbody rgbd;

    public void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        if (pushOnAwake)
            Push(startDirection, startMagnitude);
    }
    private void Update()
    {
        //Push(startDirection, startMagnitude);
    }
    public void Push(Vector3 direction, float magnitude)
    {
        Vector3 dir = direction.normalized;
        transform.Translate(Vector3.forward * Time.deltaTime*40);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Constants.Tag.PLAYER)
        {
            rgbd.Sleep();
            if (fieryEffect != null)
            {
                StopParticleSystem(fieryEffect);
            }
            if (smokeEffect != null)
            {
                StopParticleSystem(smokeEffect);
            }
            if (explodeEffect != null)
                explodeEffect.SetActive(true);
        }
    }

   

    public void StopParticleSystem(GameObject g)
    {
        ParticleSystem[] par;
        par = g.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem p in par)
        {
            p.Stop();
        }
    }

    public void OnEnable()
    {
        if (fieryEffect != null)
            fieryEffect.SetActive(true);
        if (smokeEffect != null)
            smokeEffect.SetActive(true);
        if (explodeEffect != null)
            explodeEffect.SetActive(false);
    }
}


