using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private LineRenderer lineRenderer;
    public GameObject playerHolder;
    
    public GameObject hitObject;
    private bool playerControlling = false;
    private Vector2 currentDirection;
    private int aimTargetLayerMask = 0;

    [HideInInspector]
    public bool playerAbleToControl = true;


    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {

        int enemylayermask = 1 << LayerMask.NameToLayer("Enemies");
        int borderlayermask = 1 <<  LayerMask.NameToLayer("Borders");
       aimTargetLayerMask = enemylayermask | borderlayermask;   //We'r combining layer mask with enemies and borders together with bit opeparations.

       //Set the start position of renderer.
       lineRenderer.transform.position = playerHolder.transform.position;
       lineRenderer.SetPosition(0,Vector3.zero);    //Make sure first position is zero.
       lineRenderer.gameObject.SetActive(false);
       hitObject.SetActive(false);

       GameplayManager.instance.TurnSettledAction += () => {playerAbleToControl = true;};
    }

    private void Update() 
    {
        if(playerControlling && playerAbleToControl)
        {
            //Calculate the current mouse position and find the direction between position and player.
            Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseposition.z = 0;

            Vector3 distance = mouseposition - playerHolder.transform.position;
            currentDirection = distance.normalized;
            //Send ray that direction take the position of hit as last position.
            RaycastHit2D hitinfo = Physics2D.Raycast(playerHolder.transform.position,currentDirection,Mathf.Infinity,aimTargetLayerMask);
            
            if(hitinfo.collider != null)
            {
                hitObject.transform.position = hitinfo.point;
                Vector3 distancetoHit = hitinfo.point - new Vector2(playerHolder.transform.position.x,playerHolder.transform.position.y);
                lineRenderer.SetPosition(1,distancetoHit);
            }
            //And make renderer between player position and that position dynamicaly.
        }
   
    }

    private void OnMouseDown() 
    {
        //Player clicked 
        if(!playerAbleToControl) return;
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
            playerAbleToControl = false;    //Turn played for player.
            ProjectileSpawner.instance.MakeFire(currentDirection);  //Projectiles Fired.
        }
    }

}
