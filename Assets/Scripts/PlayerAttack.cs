using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {


    private PlayerStats stats;
    private GameObject weapon;
    private Rigidbody weapon_rb;
    private Collider weapon_coll;

    public float weaponPower;
    bool weaponOUT;

    bool debug;

    private float startTime;
    private float journeyLength;
    private float overTime;
    private Vector3 newPosition;
    private float t;
    private float distance;
    private Vector3 startPosition;

    private Vector3 startPos,endPos;
    private float timePos;


    void Start() {
        stats = GetComponent<PlayerStats>();
        weapon = GameObject.Find("Weapon");
        weapon_rb = weapon.GetComponent<Rigidbody>();
        weapon_coll = weapon.GetComponent<Collider>();
        weaponPower = 1.5f;
        weaponOUT = false;

        debug = true;
        overTime = 2.0f;
        newPosition = Vector3.back * weaponPower;
        t = 0;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetButtonDown("Fire1")){
            startPosition = transform.position;
            startPos = transform.position;
            timePos = 1.0f;
            endPos = new Vector3(0, 0, transform.position.z) * weaponPower;
            //StartCoroutine("ShootWeapon");
            StartCoroutine("MoveWeapon");
            //weapon.transform.position = Vector3.Lerp(weapon.transform.localPosition, newPosition, t);
            //t = Vector3.Distance(weapon.transform.localPosition, newPosition) / 120.0f;
        }
    }


    IEnumerator MoveWeapon()
    {
        //weapon_rb.AddForce(weaponPower,ForceMode.Force);
        if (weaponOUT == false)
        {
            weaponOUT = true;

            float i = 0;
            float rate = 1.0f / timePos;
            float rate_rt = 1.0f / 0.85f;

            if (debug) Debug.Log("firing");
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                weapon.transform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
            

            if (debug) Debug.Log("returning");
            i = 0;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate_rt;
                weapon.transform.position = Vector3.Lerp(endPos, transform.position, i);
                yield return null;
            }
            weaponOUT = false;
        }
    }
        
    IEnumerator ShootWeapon()
    {
        //weapon_rb.AddForce(weaponPower,ForceMode.Force);
        if (weaponOUT == false) {
            weaponOUT = true;
            Vector3 shotDistance = Vector3.back * weaponPower;
            Vector3 endPoint = transform.position + shotDistance;

            float distCovered = (Time.time - startTime) * weaponPower;
            float journeyLength = Vector3.Distance(weapon.transform.localPosition, shotDistance);
            float fracJourney = 0;
            //float fracJourney = ((Time.time - startTime) / overTime);

            //float fracJourney = ((shotDistance - transform.position) /120.0f);

            if (debug) Debug.Log("firing");
            //weapon_coll.enabled = true;
            //weapon.transform.position = new Vector3(1.0f, 0, -1.0f) * weaponPower;
            startTime = Time.time;
            //while (Time.time < startTime + overTime)
            if (debug) Debug.Log("weapon.transform.position.z  = " + weapon.transform.position.z + " < endPoint.z " + endPoint.z);
            while (weapon.transform.position.z != endPoint.z)
            {
                weapon.transform.position = Vector3.Lerp(weapon.transform.localPosition, shotDistance, fracJourney);
                fracJourney += ((Time.time - startTime) / overTime);
                if (debug) Debug.Log("os called lerping - fracJourny=" + fracJourney);
                yield return null;
            }
            //transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            //weapon_coll.enabled = false;
            //weapon.transform.position = GetComponentInParent<Transform>().position;
            weapon.transform.localPosition = Vector3.zero;
            if (debug) Debug.Log("returning");
            weaponOUT = false;
        }
    }
}
