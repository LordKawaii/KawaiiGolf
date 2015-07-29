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

        if (playerStates.thisPlayersTurn && !playerStates.isTakingShot)
        {

            if (playerStates.isFirstFrameOfTurn)
            {
                PlayerSetup();
                playerStates.isFirstFrameOfTurn = false;
            }

            Debug.DrawLine(transform.position, transform.forward, Color.green);
            //UpdateTrajectory(transform.position, transform.forward * speedMultiplyer * powerSlider.maxValue, Vector3.down);
            InputManager();
        }

        if (playerStates.isSettingPower)
            SetPower();
	}


    void FixedUpdate()
    {
        if (playerStates.thisPlayersTurn && playerStates.isTakingShot)
        {
            if (CheckIfStoped())
            {
                rb.isKinematic = true;
                playerStates.isTakingShot = false;
                playerStates.isStoped = true;
                particleSys.Stop();
                transform.rotation = Quaternion.identity;
                playerStates.isFirstFrameOfTurn = true;
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

            if (playerStates.isSettingPower)
            {
                transform.localScale = Vector3.one;
                particleSys.Play();
                rb.isKinematic = false;
                AddForce(powerSlider.value);
                playerStates.isSettingPower = false;
                playerStates.isTakingShot = true;
            }

            if (!playerStates.isTakingShot)
            {
                //rb.isKinematic = true;
                playerStates.isSettingPower = true;
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


    public bool GetPlayerStates()
    {
         return playerStates.thisPlayersTurn;
    }

    /*
    void UpdateTrajectory(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, float maxTravelDistance)
    {
        List<Vector3> positions = new List<Vector3>();
        Vector3 lastPos = startPos;

        positions.Add(startPos);

        float traveledDistance = 0.0f;
        while (traveledDistance < maxTravelDistance)
        {
            traveledDistance += speed * timePerSegmentInSeconds;
            var hasHitSomething = TravelTrajectorySegment(currentPos, direction, speed, timePerSegmentInSeconds, positions);
            if (hasHitSomething)
            {
                break;
            }
            Vector3 lastPos = currentPos;
            currentPos = positions[positions.Count - 1];
            direction = currentPos - lastPos;
            direction.Normalize();
        }

        BuildTrajectoryLine(positions);
    }

    bool TravelTrajectorySegment(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, List<Vector3> positions)
    {
        Vector3 newPos = startPos + direction * speed * timePerSegmentInSeconds + Physics.gravity * timePerSegmentInSeconds;

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
        for (int i = 0; i < positions.Count; ++i)
        {
            lineRenderer.SetPosition(i, positions[i]);
        }
    }
 
    */
}//End Class


