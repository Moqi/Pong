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
	
	private bool isSinglePlayer = true;
	
	private bool isCollision = false;
	private bool isUpperWallCollision = false;
	private bool isLowerWallCollision = false;
	
	private AudioSource hitPaddle;
	private AudioSource hitWall;
	private AudioSource lose;
		
	private void Start() 
	{		
		LoadPlayerNumber();
		CreateSounds();
		CreateObjects();
		InitObjects();	
	}	
	
	private void LoadPlayerNumber()
	{
		if (ApplicationModel.noPlayers == 1)
			isSinglePlayer = true;
		else
			isSinglePlayer = false;	
	}
	
	private void CreateSounds()
	{
		hitPaddle = gameObject.AddComponent<AudioSource>();
		hitPaddle.clip = Resources.Load("Sounds/hitPaddle") as AudioClip;	
		
		hitWall = gameObject.AddComponent<AudioSource>();
		hitWall.clip = Resources.Load("Sounds/hitWall") as AudioClip;		
		
		lose = gameObject.AddComponent<AudioSource>();
		lose.clip = Resources.Load("Sounds/lose") as AudioClip;					
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
		leftPaddleRigidbody.rigidbody.mass = 10000;		
		leftPaddleRigidbody.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
		leftPaddle.AddComponent<LeftPaddle>().hitPaddle = hitPaddle;
	}
	
	private void CreateRightPaddle()
	{
		// create right paddle
		rightPaddle = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		rightPaddle.name = "RightPaddle";	
		//rightPaddle.gameObject.tag = "RightPaddle";
		Rigidbody rightPaddleRigidbody = rightPaddle.AddComponent<Rigidbody>();
		rightPaddleRigidbody.rigidbody.useGravity = false;			
		rightPaddleRigidbody.rigidbody.mass = 10000;
		rightPaddleRigidbody.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
		rightPaddle.AddComponent<RightPaddle>().hitPaddle = hitPaddle;
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
		UpperWall uw = upperWall.AddComponent<UpperWall>();//.parent = this;
		uw.parent = this;
		uw.hitWall = hitWall;
		//upperWall.AddComponent<UpperWall>().hitWall = hitWall;
	}
	
	private void CreateLowerWall()
	{
		lowerWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		lowerWall.name = "LowerWall";
		LowerWall lw = lowerWall.AddComponent<LowerWall>();//.parent = this;
		lw.parent = this;
		lw.hitWall = hitWall;
		//lowerWall.AddComponent<LowerWall>().hitWall = hitWall;
	}
	
	private void CreateLeftEdge()
	{
		leftEdge = GameObject.CreatePrimitive (PrimitiveType.Quad);
		leftEdge.name = "LeftEdge";
		LeftEdge le = leftEdge.AddComponent<LeftEdge>();//.parent = this;
		le.parent = this;
		le.lose = lose;
		//leftEdge.AddComponent<LeftEdge>().lose = lose;
	}	
	private void CreateRightEdge()
	{
		rightEdge = GameObject.CreatePrimitive (PrimitiveType.Quad);
		rightEdge.name = "RightEdge";	
		RightEdge re = rightEdge.AddComponent<RightEdge>();//.parent = this;
		re.parent = this;
		re.lose = lose;
		//rightEdge.AddComponent<RightEdge>().lose = lose;
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
		if(Input.GetKey(KeyCode.Q)) 		
		{
			Vector3 position = leftPaddle.transform.position;
			position.z += ScreenSettings.PADDLE_SPEED * Time.deltaTime;
			leftPaddle.transform.position = position;
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			Vector3 position = leftPaddle.transform.position;
			position.z -= ScreenSettings.PADDLE_SPEED * Time.deltaTime;
			leftPaddle.transform.position = position;			
		}
			
		
		if (!isSinglePlayer)
		{			
			if(Input.GetKey(KeyCode.O))
			{
				Vector3 position = rightPaddle.transform.position;
				position.z += ScreenSettings.PADDLE_SPEED * Time.deltaTime;
				rightPaddle.transform.position = position;	
			}
			if(Input.GetKey(KeyCode.L)) 
			{
				Vector3 position = rightPaddle.transform.position;
				position.z -= ScreenSettings.PADDLE_SPEED * Time.deltaTime;
				rightPaddle.transform.position = position;				
			}
		}
		else
		{				
			Vector3 position = rightPaddle.transform.position;
			Vector3 ballPos = ball.transform.position;
			
			if (ball.transform.position.z < 6 && ball.transform.position.z > -6 )
				position.z = ball.transform.position.z;	
	
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
		leftEdge.renderer.material.color = Color.gray;
		leftEdge.transform.rotation = Quaternion.Euler(0, 270, 270);
		leftEdge.transform.position = new Vector3(-10, 0.5f, 0);
		leftEdge.transform.localScale = new Vector3(1, 15, 1);			
		
	}
	
	private void InitRightEdge()
	{
		rightEdge.renderer.material.color = Color.gray;
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
	
	public void LowerWallCollision(bool collide)
	{
		isCollision = collide;
		isLowerWallCollision = collide;
	}
	
	public void UpperWallCollision(bool collide)
	{
		isCollision = collide;
		isUpperWallCollision = collide;
	}
}