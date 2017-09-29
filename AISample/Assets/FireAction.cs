using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FireAction : Action
{
    public SharedGameObject targetManager;
    public SharedGameObject myTransform;
    public float inaccuracy = .2f;
    private FPSRigidBodyWalker FPSWalker;
    public SharedGameObject playerObj;
    Vector3 targetPos;
    float eyeHeight;
    private Vector3 rayOrigin;
    private Vector3 targetDir;
    public float damage = 10.0f;
    public bool isMeleeAttack;
    private float damageAmt;
    private bool canFire = true;

    public AudioClip fireSound;
    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("targetManager") != null)
        {
            targetManager = GameObject.Find("targetManager");
        }
        FPSWalker = playerObj.Value.GetComponent<FPSRigidBodyWalker>();

    }

    public override TaskStatus OnUpdate()
    {
        
        for (int i = 0; i < 3; i++)
        {
            if (canFire)
            {
                FireOneShot();
                GetComponent<AudioSource>().clip = fireSound;
                GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip, 0.8f);
            }
        }
        
        
        transform.LookAt(targetPos);

        return TaskStatus.Success;
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(.1f);
        canFire = true;
    }

    void FireOneShot()
    {

        eyeHeight = playerObj.Value.transform.position.y * -0.25f;
        Vector3 shooterOrigin = new Vector3(myTransform.Value.transform.position.x, myTransform.Value.transform.position.y + 1f, myTransform.Value.transform.position.z);
        if (Vector3.Distance(myTransform.Value.transform.position, targetManager.Value.transform.position) > 2.5f)
        {
            targetPos = new Vector3(targetManager.Value.transform.position.x + Random.Range(-inaccuracy, inaccuracy),
                                    targetManager.Value.transform.position.y + Random.Range(-inaccuracy, inaccuracy),
                                    targetManager.Value.transform.position.z + Random.Range(-inaccuracy, inaccuracy));
        }
        else
        {
            targetPos = new Vector3(targetManager.Value.transform.position.x,
                                    targetManager.Value.transform.position.y,
                                    targetManager.Value.transform.position.z);
        }
        if (Vector3.Distance(myTransform.Value.transform.position, targetManager.Value.transform.position) > 2.5f)
        {
            targetPos = new Vector3(targetManager.Value.transform.position.x + Random.Range(-inaccuracy, inaccuracy),
                targetManager.Value.transform.position.y + Random.Range(-inaccuracy, inaccuracy),
                targetManager.Value.transform.position.z + Random.Range(-inaccuracy, inaccuracy));
            //+ (playerObj.Value.transform.up * eyeHeight);
            
        }
        else
        {
            targetPos = new Vector3(targetManager.Value.transform.position.x,
                targetManager.Value.transform.position.y,
                targetManager.Value.transform.position.z);

        }

        rayOrigin = new Vector3(myTransform.Value.transform.position.x, eyeHeight, myTransform.Value.transform.position.z);
        //targetDir = (targetPos - rayOrigin).magnitude;

        RaycastHit hit;
        hit = new RaycastHit();
        //calculate damage amount
        damageAmt = Random.Range(damage, damage + damage);
        Vector3 shootSpot = (targetPos - shooterOrigin).normalized;

       
        //case 11://hit object is player
        if (Physics.Raycast(shooterOrigin, shootSpot, out hit))
        {
            if (hit.transform.gameObject.GetComponent<FPSPlayer>())
            {
                hit.transform.gameObject.GetComponent<FPSPlayer>().ApplyDamage(damageAmt, myTransform.Value.transform, isMeleeAttack);
            }
            if (hit.transform.gameObject.GetComponent<LeanColliderDamage>())
            {
                hit.transform.gameObject.GetComponent<LeanColliderDamage>().ApplyDamage(damageAmt);
            }
            Debug.Log(hit.transform.gameObject.name);
            Debug.DrawRay(shooterOrigin, shootSpot, Color.red, 1f);
        }
        canFire = false;
        StartCoroutine(FireDelay());
    }
}

