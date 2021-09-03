using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private int numberOfProjectiles = 5;
    [SerializeField] private float timeBetweenProjectileSpawn = .2f;
    [SerializeField] private GameObject playerHolder;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject projectilePrefab;
    
    public GameObject hitObject;
    private bool playerControlling = false;
    private Vector2 currentDirection;
    private int borderLayerMask = 0;


    private bool firingActive = false;
    private float fireTimer = 0;
    private int currentLaunchedProjectile = 0;


    private void Start() 
    {
       borderLayerMask = 1 << LayerMask.NameToLayer("Borders"); //TODO we can change it to player interactable or add another layers to layer bitmask.

       //Set the start position of renderer.
       lineRenderer.transform.position = playerHolder.transform.position;
       lineRenderer.SetPosition(0,Vector3.zero);    //Make sure first position is zero.
       lineRenderer.gameObject.SetActive(false);

       hitObject.SetActive(false);
    }

    private void Update() 
    {
        if(playerControlling && !firingActive)
        {
            //Calculate the current mouse position and find the direction between position and player.
            Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseposition.z = 0;

            Vector3 distance = mouseposition - playerHolder.transform.position;
            currentDirection = distance.normalized;
            //Send ray that direction take the position of hit as last position.
            RaycastHit2D hitinfo = Physics2D.Raycast(playerHolder.transform.position,currentDirection,Mathf.Infinity,borderLayerMask);
            
            if(hitinfo.collider != null)
            {
                hitObject.transform.position = hitinfo.point;
                Vector3 distancetoHit = hitinfo.point - new Vector2(playerHolder.transform.position.x,playerHolder.transform.position.y);
                lineRenderer.SetPosition(1,distancetoHit);
            }
            //And make renderer between player position and that position dynamicaly.
        }


        if(firingActive)
        {
            fireTimer += Time.deltaTime;
            if(fireTimer >= timeBetweenProjectileSpawn)
            {
                GameObject projectile = Instantiate(projectilePrefab,playerHolder.transform.position,Quaternion.identity);
                projectile.GetComponent<Projectiles>().SetDirection(currentDirection);
                fireTimer = 0;
                currentLaunchedProjectile ++;
                if(currentLaunchedProjectile >= numberOfProjectiles)
                {
                    firingActive = false;
                }
            }
            
        }
    }

    private void OnMouseDown() 
    {
        //Player clicked 
        if(firingActive) return;
        playerControlling = true;
        hitObject.SetActive(true);
        lineRenderer.gameObject.SetActive(true);
    }


    private void OnMouseUp() 
    {
        if(playerControlling)
        {
            playerControlling = false;
            hitObject.SetActive(false);
            lineRenderer.gameObject.SetActive(false);
            MakeFire();
        }
    }

    public void MakeFire()
    {
        //Send projectile to current direction.
        firingActive = true;
        currentLaunchedProjectile = 0;
        fireTimer = timeBetweenProjectileSpawn; //We need to instatiate projectile at first.
    }

}
