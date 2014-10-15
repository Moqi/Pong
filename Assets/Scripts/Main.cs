using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	private GameObject leftPaddle;
	private GameObject rightPaddle;
	private GameObject ball;	
	private GameObject background;
	private GameObject light;
	private GameObject upperWall;
	private GameObject lowerWall;
	private GameObject net;
	private GameObject leftScore;
	private GameObject rightScore;
	private GameObject leftEdge;
	private GameObject rightEdge;
	
	private int leftScoreVal = 0;
	private int rightScoreVal = 0;
	
	public delegate void OnCollisionEnter(Collider collider);
	public event OnCollisionEnter OnCollider;	
	
	private void Start() 
	{		
		CreateObjects();
		InitObjects();
	}
	
	private void CreateObjects()
	{
		CreateBackground();
		CreateLeftPaddle();
		CreateRightPaddle();
		CreateBall();
		CreateGameLight();	
		CreateUpperWall();
		CreateLowerWall();
		CreateLeftEdge();
		CreateRightEdge();
		CreateNet();
		CreateHUD();
	}
	
	private void InitObjects()
	{
		InitLeftPaddle();
		InitRightPaddle();
		InitBall();	
		InitBackground();		
		InitMainCamera();
		InitLight();
		InitUpperWall();
		InitLowerWall();
		InitNet();
		InitHUD();
		InitLeftEdge();
		InitRightEdge();
    }	
	
	private void CreateBackground()
	{
		// create a plane in the middle of the scene( the background)
		background = GameObject.CreatePrimitive(PrimitiveType.Plane);
		background.name = "Background";		
	}
	
	private void CreateLeftPaddle()
	{
		// create left paddle 
		leftPaddle = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		leftPaddle.name = "LeftPaddle";	
		Rigidbody leftPaddleRigidbody = leftPaddle.AddComponent<Rigidbody>();
		leftPaddleRigidbody.rigidbody.useGravity = false;	
		leftPaddleRigidbody.rigidbody.mass = 1000;		
		leftPaddleRigidbody.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
	}
	
	private void CreateRightPaddle()
	{
		// create right paddle
		rightPaddle = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		rightPaddle.name = "RightPaddle";	
		Rigidbody rightPaddleRigidbody = rightPaddle.AddComponent<Rigidbody>();
		rightPaddleRigidbody.rigidbody.useGravity = false;			
		rightPaddleRigidbody.rigidbody.mass = 1000;
		rightPaddleRigidbody.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
	}
	
	private void CreateBall()
	{
		// create ball
		ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		ball.name = "Ball";	
		Rigidbody ballRigidbody = ball.AddComponent<Rigidbody>();
		ball.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		ball.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		ball.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
		
		PhysicMaterial ballPhysic = new PhysicMaterial();
		ballPhysic.name = "BallPhysic";
		ballPhysic.dynamicFriction = 0;
		ballPhysic.staticFriction = 0;
		ballPhysic.bounciness = 1;
		ballPhysic.frictionCombine = PhysicMaterialCombine.Minimum;
		ballPhysic.bounceCombine = PhysicMaterialCombine.Maximum;
		ball.collider.material = ballPhysic;			
	}
	
	private void CreateGameLight()
	{
		// create a light in the middle of the scene
		light = new GameObject("Light");
		light.AddComponent<Light>();
	}
	
	private void CreateUpperWall()
	{
		upperWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		upperWall.name = "UpperWall";
	}
	
	private void CreateLowerWall()
	{
		lowerWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		lowerWall.name = "LowerWall";
	}
	
	private void CreateLeftEdge()
	{
		leftEdge = GameObject.CreatePrimitive (PrimitiveType.Quad);
		leftEdge.name = "LeftEdge";
		leftEdge.AddComponent<LeftEdge>().parent = this;
	}	
	private void CreateRightEdge()
	{
		rightEdge = GameObject.CreatePrimitive (PrimitiveType.Quad);
		rightEdge.name = "RightEdge";	
		rightEdge.AddComponent<RightEdge>().parent = this;
	}
		
	private void CreateNet()
	{
		net = GameObject.CreatePrimitive(PrimitiveType.Quad);
		net.name = "Net";
	}
	
	private void CreateHUD()
	{
		leftScore = new GameObject("LeftScore");
		leftScore.AddComponent<GUIText>();	
		
		rightScore = new GameObject("RightScore");
		rightScore.AddComponent<GUIText>();			
	}
		
	private void FixedUpdate()
	{
		MovePaddles();
	}
	
	private void MovePaddles()
	{	
		if(Input.GetKey(KeyCode.W)) 		
		{
			Vector3 position = leftPaddle.transform.position;
			position.z += ScreenSettings.PADDLE_SPEED * Time.deltaTime;
			leftPaddle.transform.position = position;
		}
		if (Input.GetKey(KeyCode.S))
		{
			Vector3 position = leftPaddle.transform.position;
			position.z -= ScreenSettings.PADDLE_SPEED * Time.deltaTime;
			leftPaddle.transform.position = position;			
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			Vector3 position = rightPaddle.transform.position;
			position.z += ScreenSettings.PADDLE_SPEED * Time.deltaTime;
			rightPaddle.transform.position = position;	
		}
		if(Input.GetKey(KeyCode.DownArrow)) 
		{
			Vector3 position = rightPaddle.transform.position;
			position.z -= ScreenSettings.PADDLE_SPEED * Time.deltaTime;
			rightPaddle.transform.position = position;				
		}
	}  	
	
	private void InitLight()
	{
		light.light.type = LightType.Directional;
		light.light.intensity = 0.3f;
		light.light.range = 500;
		light.light.shadows = LightShadows.Soft;
	    light.transform.position = new Vector3(0, 12, 0);
		light.transform.rotation = Quaternion.Euler(90, 0, 0);	
	}  
	
	private void InitLeftPaddle()
	{
		Vector3 leftVector3 = new Vector3(ScreenSettings.LEFT_PADDLE_X, ScreenSettings.LEFT_PADDLE_Y, ScreenSettings.LEFT_PADDLE_Z);
		leftPaddle.transform.position = leftVector3;
		leftPaddle.transform.rotation = Quaternion.Euler(90, 0, 0);	
		
		leftPaddle.transform.localScale = new Vector3(1, ScreenSettings.PADDLE_HEIGHT, 1);	
	}
	
	private void InitRightPaddle()
	{
		rightPaddle.transform.position = new Vector3(ScreenSettings.RIGHT_PADDLE_X, ScreenSettings.RIGHT_PADDLE_Y, ScreenSettings.RIGHT_PADDLE_Z);;
		rightPaddle.transform.rotation = Quaternion.Euler(90, 0, 0);

		rightPaddle.transform.localScale = new Vector3(1, ScreenSettings.PADDLE_HEIGHT, 1);
	}
	
	private int Direction()
	{
		return Random.Range (0, 2) == 0 ? -1 : 1;
	}
	
	private void InitBall()
	{
		ball.transform.position =  new Vector3(ScreenSettings.BALL_X, ScreenSettings.BALL_Y, ScreenSettings.BALL_Z);
				
		Vector3 initialImpuls;
		
		initialImpuls = new Vector3 (Direction()* ScreenSettings.BALL_SPEED, 0, Direction() * ScreenSettings.BALL_SPEED);

		ball.rigidbody.velocity = initialImpuls;	
	}
	
	private void InitBackground()
	{
		background.transform.localScale = new Vector3(ScreenSettings.BG_WIGHT, 1, ScreenSettings.BG_HEIGHT);
	}
	
	private void InitMainCamera()
	{
		this.camera.transform.rotation = Quaternion.Euler(90, 0, 0);
		this.camera.transform.position = new Vector3(ScreenSettings.CAMERA_X, ScreenSettings.CAMERA_Y, ScreenSettings.CAMERA_Z);
		
		this.gameObject.AddComponent<MeshCollider>();
	}
	
	private void InitUpperWall()
	{
		upperWall.transform.position = new Vector3(0, 0.5f, 8);
		upperWall.transform.localScale = new Vector3(20, 1, 1);
	}
	
	private void InitLowerWall()
	{
		lowerWall.transform.position = new Vector3(0, 0.5f, -8);
		lowerWall.transform.localScale = new Vector3(20, 1, 1);
	}
	
	private void InitNet()
	{
		net.renderer.material.color = Color.grey;
		net.transform.rotation = Quaternion.Euler(90, 0, 0);
		net.transform.position = new Vector3(0, 0.01f, 0);
		net.transform.localScale = new Vector3(1, 15, 1);
	}
	
	private void InitHUD()
	{
		leftScore.guiText.text = leftScoreVal.ToString();
		leftScore.transform.position = new Vector3(0.37f, 0.9f, 0);
		leftScore.guiText.fontSize = 25;
		leftScore.guiText.text = leftScoreVal.ToString();
		
		rightScore.guiText.text = rightScoreVal.ToString();
		rightScore.transform.position = new Vector3(0.6f, 0.9f, 0);
		rightScore.guiText.fontSize = 25;
		rightScore.guiText.text = rightScoreVal.ToString();
	}

	private void InitLeftEdge()
	{
	
		leftEdge.renderer.material.color = Color.red;
		leftEdge.transform.rotation = Quaternion.Euler(0, 270, 270);
		leftEdge.transform.position = new Vector3(-10, 0.5f, 0);
		leftEdge.transform.localScale = new Vector3(1, 15, 1);			
		
	}
	
	private void InitRightEdge()
	{
		rightEdge.renderer.material.color = Color.red;
		rightEdge.transform.rotation = Quaternion.Euler(0, 90, 90);
		rightEdge.transform.position = new Vector3(10, 0.5f, 0);
		rightEdge.transform.localScale = new Vector3(1, 15, 1);		
	}

	public void UpdateLeftScore()
	{
		Debug.Log("Update Left Score");
		
		leftScoreVal++;
		leftScore.guiText.text = leftScoreVal.ToString();
		
		Restart();
	}
	
	public void UpdateRightScore()
	{
		Debug.Log("Update Right Score");
		
		rightScoreVal++;
		rightScore.guiText.text = rightScoreVal.ToString();	
		
		Restart();	
	}	
	
	private void Restart()
	{
		InitBall();
		InitLeftPaddle();
		InitRightPaddle();
	}
}