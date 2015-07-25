using UnityEngine;
using System.Collections;

public class ScoreMarkerController : MonoBehaviour {
    public Material collectedColor;

    private Renderer renderer;
    private GameController gameController;
    private bool hasBeenTagged = false;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        renderer = gameObject.GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            renderer.material = collectedColor;
            if (!hasBeenTagged)
            {
                hasBeenTagged = true;
                gameController.SetMarkerToCollected();
            }
        }

    }

    public bool IsCollected()
    {
        return hasBeenTagged;
    }
	
}
