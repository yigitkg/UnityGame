using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Playerr : MonoBehaviour
{
	public int speed = 5;
	float vertical;
	float horizontal;
	public Joystick joystick;
	public Vector2 joystickInputAxis;

	public float moveSpeed = 5;

	Camera viewCamera;
	PlayerController controller;
	GunController gunController;

	void Start()
	{
		controller = GetComponent<PlayerController>();
		gunController = GetComponent<GunController>();
		viewCamera = Camera.main;
	}

    void FixedUpdate()
	{
		vertical = joystick.Horizontal;
		horizontal = joystick.Horizontal;
		if (vertical != 0f || horizontal != 0f)
		{
			transform.up = new Vector3(vertical * speed, 0, 0);
			transform.right = new Vector3(0, 0, horizontal * speed);
			//transform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime, Space.World);

		}
		// Movement input
		//Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		//Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		//controller.Move(moveVelocity);


		Vector3 moveInput = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		controller.Move(moveVelocity);
		/*if (joystick.Horizontal >= .2f || joystick.Horizontal <= -.2f || joystick.Vertical >= .2f || joystick.Vertical <= -.2f)
		{
			
		}
	*/

		// Look input
		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayDistance;

		if (groundPlane.Raycast(ray, out rayDistance))
		{
			Vector3 point = ray.GetPoint(rayDistance);
			//Debug.DrawLine(ray.origin,point,Color.red);
			controller.LookAt(point);
		}

		// Weapon input
		if (Input.GetMouseButton(0))
		{
			gunController.Shoot();
		}
	}
}