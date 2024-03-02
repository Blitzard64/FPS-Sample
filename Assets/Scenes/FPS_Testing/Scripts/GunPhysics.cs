using UnityEngine;
using System.Collections;
public class GunPhysics : MonoBehaviour
{
    public Camera camera;
    public Camera scope;
    public float timeBetween1;
    public float timeBetween2;
    public float timeBetween3;
    public float fireRate = 10f;
    public float gunAudioRate;
    public float damage = 10f;
    public AudioSource source;
    public AudioSource shootingSource;
    public AudioClip rifleSFX;
    public AudioClip reloadSFX;
    public int maxBullet = 10;
    public int currBullet;
    public float reloadTime = 3f;
    public int reloadPass=0;
    public float scopeRange;
    public Vector3 scopeVector;
    public Vector3 constrainVector;
    void Start()
    {
        currBullet = maxBullet;
        camera.enabled = true;
        scope.enabled = false;
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && (Time.time>=timeBetween1) && (currBullet>0))
        {
            currBullet--;
            timeBetween1 = Time.time + 1f/fireRate;
            castRay();
        }
        if (Input.GetButton("Fire1") && (Time.time>=timeBetween2) && (currBullet>0))
        {
            timeBetween2 = Time.time + 1f/gunAudioRate;
            if (!source.isPlaying)
            {
                shootingSource.PlayOneShot(rifleSFX);
            }
        }
        scopeFunc();
        if ((currBullet<=0)||(Input.GetKey("r")))
        {
            StartCoroutine(Reload());
        }
    }
    void scopeFunc()
    {
        scopeVector = camera.transform.position+camera.transform.forward*scopeRange;
        scopeVector = new Vector3(scopeVector.x,scopeVector.y,scopeVector.z);
        
        if (Input.GetButton("Fire2"))
        {
            Debug.Log("Scope:");
            Debug.Log(scopeVector);
            Debug.Log("Constrain:");
            Debug.Log(constrainVector);
            if (scopeVector.x>constrainVector.x)
            {
                scopeVector.x = constrainVector.x;
            }
            if (scopeVector.y>constrainVector.y)
            {
                scopeVector.y = constrainVector.y;
            }
            if (scopeVector.z>constrainVector.z)
            {
                scopeVector.z = constrainVector.z;
            }
            scope.transform.position = scopeVector;
            Debug.Log("Final:");
            Debug.Log(scopeVector);
            scopeRay();
            camera.enabled = false;
            scope.enabled = true;
        }
        else
        {
            scope.transform.position = scopeVector;
            camera.enabled = true;
            scope.enabled = false;
        }
    }
    IEnumerator Reload()
    {
        currBullet = maxBullet;
        Debug.Log("Reloading...Hello");
        if (!source.isPlaying)
        {
            source.PlayOneShot(reloadSFX);
        }
        yield return new WaitForSeconds(reloadTime);
    }
    
    void castRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position,camera.transform.forward,out hit))
        {
            if (hit.transform.tag=="Enemy")
            {
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                enemy.Damage(damage);
            }
        }
    }
    void scopeRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position,camera.transform.forward,out hit))
        {
            if (hit.transform.name != "Plane")
            {
                if (hit.transform.tag=="jumpResetter")
                {
                    constrainVector = hit.transform.position;
                    Debug.Log(constrainVector);
                }
            }
        }
    }
}