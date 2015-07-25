using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour {
    public int playerNum = 1;
    public float speedMultiplyer = 1;
    private GameStates gameStates;
    private Rigidbody rb;
    private PlayerStates playerStates;
    private GameController gameController;
    private ParticleSystem particleSys;

    
    private Slider powerSlider;
    private bool sliderMovingUp = true;

    void Start()
    {
        particleSys = gameObject.GetComponentInChildren<ParticleSystem>();
        playerStates = new PlayerStates();
        powerSlider = GameObject.FindGameObjectWithTag("PowerSlider").GetComponent<Slider>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameStates = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStates>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () {

        if (playerStates.isFirstFrameOfTurn)
        {
            PlayerSetup();
            playerStates.isFirstFrameOfTurn = false;
        }

        if (gameStates.playerTurn == playerNum && !playerStates.takingShot)
        {
            Debug.DrawLine(transform.position, transform.forward, Color.green);
            //UpdateTrajectory(transform.position, transform.forward * speedMultiplyer * powerSlider.maxValue, Vector3.down);
            InputManager();
        }

        if (playerStates.settingPower)
            SetPower();
                
	}

    void FixedUpdate()
    {
        if (playerStates.takingShot)
        {
            if (CheckIfStoped())
            {
                rb.isKinematic = true;
                playerStates.takingShot = false;
                playerStates.isStoped = true;
                particleSys.Stop();
                transform.rotation = Quaternion.identity;
                gameController.EndTurn();
            }

        }

    }

    void PlayerSetup()
    {
        powerSlider.value = powerSlider.minValue;
    }

    void InputManager()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * 100);
        }
        if (Input.GetButtonDown("Fire1"))
        {

            if (playerStates.settingPower)
            {
                transform.localScale = Vector3.one;
                particleSys.Play();
                rb.isKinematic = false;
                AddForce(powerSlider.value);
                playerStates.settingPower = false;
                playerStates.takingShot = true;
            }
            
            if (!playerStates.takingShot)
            {
                //rb.isKinematic = true;
                playerStates.settingPower = true;
            }
        }
    }

    void SetPower()
    {

        if (sliderMovingUp)
        {
            powerSlider.value += gameController.powerSlideSpeed;
            transform.localScale = new Vector3(transform.lossyScale.x - (powerSlider.maxValue * gameController.powerSlideSpeed), transform.lossyScale.y - (powerSlider.maxValue * gameController.powerSlideSpeed), transform.lossyScale.z - (powerSlider.maxValue * gameController.powerSlideSpeed));
        }
        if (powerSlider.value == powerSlider.maxValue)
            sliderMovingUp = false;

        if (!sliderMovingUp)
        { 
            powerSlider.value -= gameController.powerSlideSpeed;
            transform.localScale = new Vector3(transform.lossyScale.x + (powerSlider.maxValue * gameController.powerSlideSpeed), transform.lossyScale.y + (powerSlider.maxValue * gameController.powerSlideSpeed), transform.lossyScale.z + (powerSlider.maxValue * gameController.powerSlideSpeed));
        }
        if (powerSlider.value == powerSlider.minValue)
            sliderMovingUp = true;
        
    }

    void AddForce(float power)
    {
        rb.velocity = (transform.forward * speedMultiplyer * power);
        playerStates.isStoped = false;
    }

    bool CheckIfStoped()
    {
        return (rb.velocity.magnitude < .1f);
    }


    /*
    void UpdateTrajectory(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, float maxTravelDistance)
    {
        var positions = new List<Vector3>();
        var lastPos = startPos;

        positions.Add(startPos);

        var traveledDistance = 0.0f;
        while (traveledDistance < maxTravelDistance)
        {
            traveledDistance += speed * timePerSegmentInSeconds;
            var hasHitSomething = TravelTrajectorySegment(currentPos, direction, speed, timePerSegmentInSeconds, positions);
            if (hasHitSomething)
            {
                break;
            }
            var lastPos = currentPos;
            currentPos = positions[positions.Count - 1];
            direction = currentPos - lastPos;
            direction.Normalize();
        }

        BuildTrajectoryLine(positions);
    }

    bool TravelTrajectorySegment(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, List<Vector3> positions)
    {
        var newPos = startPos + direction * speed * timePerSegmentInSeconds + Physics.gravity * timePerSegmentInSeconds;

        RaycastHit hitInfo;
        var hasHitSomething = Physics.Linecast(startPos, newPos, out hitInfo);
        if (hasHitSomething)
        {
            newPos = hitInfo.position;
        }
        positions.Add(newPos);

        return hasHitSomething;
    }

    void BuildTrajectoryLine(List<Vector3> positions)
    {
        lineRenderer.SetVertexCount(positions.Count);
        for (var i = 0; i < positions.Count; ++i)
        {
            lineRenderer.SetPosition(i, positions[i]);
        }
    }
 
    */
}//End Class


