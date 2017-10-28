using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
	public float speed = 6f;
	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigidbody;
	private int floorMask;
	private float camRayLength = 100f;

	private void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
        


	}

    private void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))//如果有打到地板
        {
            //打到地板的點座標
            Vector3 playerToMuse = floorHit.point - transform.position;
            playerToMuse.y = 0f;
            //(x,y,z,w)
            Quaternion newRotation = Quaternion.LookRotation(playerToMuse);
            playerRigidbody.MoveRotation(newRotation);
        }

    }

    private void Animating(float h, float v)
    {

        bool walking = h != 0f || v != 0;
        anim.SetBool("IsWalking", walking);
    }

}
