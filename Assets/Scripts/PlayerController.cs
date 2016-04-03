using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour {

	private ConfigurableJoint joint;

	public float speed = 5f;

	public float lookSensitivity = 3f;

	[SerializeField]
	private float thrusterForce = 1000f;

	private PlayerMotor motor;

	void Start ()
	{

		motor = GetComponent<PlayerMotor>();
		joint = GetComponent<ConfigurableJoint> ();

	}

	void Update ()
	{
		float _zMov = Input.GetAxisRaw ("Vertical");
		float _xMov = Input.GetAxisRaw ("Horizontal"); 

		Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;
        //final calculation
		Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        
       //apply calculations for motion
       motor.Move(_velocity);

		//Sideways camera rotation
		float _yRot = Input.GetAxisRaw("Mouse X");
        
		Vector3 _rotation = new Vector3 (0f, _yRot, 0f) * lookSensitivity;

		motor.Rotate (_rotation);


		//UP-DOWN camera
		float _xRot = Input.GetAxisRaw("Mouse Y");

		Vector3 _cameraRotation = new Vector3 (_xRot, 0f , 0f) * lookSensitivity;

		motor.RotateCamera (_cameraRotation);

		//Apply thruster force:

		//Calculate force:
		Vector3 _thrusterForce = Vector3.zero;

		if (Input.GetButton ("Jump")) {
			_thrusterForce = Vector3.up * thrusterForce;
		}

		motor.ApplyThruster (_thrusterForce);
	}
}