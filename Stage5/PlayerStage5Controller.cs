using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStage5Controller : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;                  // Reference to the nav mesh agent component.
    public float turnSmoothing = 15f;           // The amount of smoothing applied to the player's turning using spherical interpolation.
    public float speedDampTime = 0.1f;          // The approximate amount of time it takes for the speed parameter to reach its value upon being set.
    public float slowingSpeed = 0.175f;         // The speed the player moves as it reaches close to it's destination.
    public float turnSpeedThreshold = 0.5f;     // The speed beyond which the player can move and turn normally.
    public float inputHoldDelay = 0.5f;         // How long after reaching an interactable before input is allowed again.
    

    private Vector3 destinationPosition;        // The position that is currently being headed towards, this is the interactionLocation of the currentInteractable if it is not null.
    private bool handleInput = true;            // Whether input is currently being handled.
    private WaitForSeconds inputHoldWait;       // The WaitForSeconds used to make the user wait before input is handled again.

    private const float stopDistanceProportion = 0.1f;
                                                // The proportion of the nav mesh agent's stopping distance within which the player stops completely.
    private const float navMeshSampleDistance = 4f;
                                                // The maximum distance from the nav mesh a click can be to be accepted.


    private void Start()
    {
        // The player will be rotated by this script so the nav mesh agent should not rotate it.
        agent.updateRotation = false;

        // Create the wait based on the delay.
        inputHoldWait = new WaitForSeconds (inputHoldDelay);
		
        // Set the initial destination as the player's current position.
        destinationPosition = transform.position;
    }
		
    private void Update()
    {
        // If the nav mesh agent is currently waiting for a path, do nothing.
        if (agent.pathPending)
            return;

        // Cache the speed that nav mesh agent wants to move at.
        float speed = agent.desiredVelocity.magnitude;
        
        // If the nav mesh agent is very close to it's destination, call the Stopping function.
        if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
            Stopping (out speed);
        // Otherwise, if the nav mesh agent is close to it's destination, call the Slowing function.
        else if (agent.remainingDistance <= agent.stoppingDistance)
            Slowing(out speed, agent.remainingDistance);
        // Otherwise, if the nav mesh agent wants to move fast enough, call the Moving function.
        else if (speed > turnSpeedThreshold)
            Moving ();
    }


    // This is called when the nav mesh agent is very close to it's destination.
    private void Stopping (out float speed)
    {
        // Stop the nav mesh agent from moving the player.
        agent.isStopped = true;

        // Set the player's position to the destination.
        transform.position = destinationPosition;

        // Set the speed (which is what the animator will use) to zero.
        speed = 0f;

        // If the player is stopping at an interactable...
/*        if (currentInteractable)
        {
            // ... set the player's rotation to match the interactionLocation's.
            transform.rotation = currentInteractable.interactionLocation.rotation;

            // Interact with the interactable and then null it out so this interaction only happens once.
            currentInteractable.Interact();
            currentInteractable = null;

            // Start the WaitForInteraction coroutine so that input is ignored briefly.
            StartCoroutine (WaitForInteraction ());
        }*/
    }


    // This is called when the nav mesh agent is close to its destination but not so close it's position should snap to it's destination.
    private void Slowing (out float speed, float distanceToDestination)
    {
        // Although the player will continue to move, it will be controlled manually so stop the nav mesh agent.
        agent.isStopped = true;

        // Find the distance to the destination as a percentage of the stopping distance.
        float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;

        // The target rotation is the rotation of the interactionLocation if the player is headed to an interactable, or the player's own rotation if not.
/*        Quaternion targetRotation = currentInteractable ? currentInteractable.interactionLocation.rotation : transform.rotation;

        // Interpolate the player's rotation between itself and the target rotation based on how close to the destination the player is.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);

        // Move the player towards the destination by an amount based on the slowing speed.
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);

        // Set the speed (for use by the animator) to a value between slowing speed and zero based on the proportional distance.
        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);*/
        speed = 0f;
    }


    // This is called when the player is moving normally.  In such cases the player is moved by the nav mesh agent, but rotated by this function.
    private void Moving ()
    {
        // Create a rotation looking down the nav mesh agent's desired velocity.
        Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);

        // Interpolate the player's rotation towards the target rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    }


    // This function is called by the EventTrigger on the scene's ground when it is clicked on.
    public void OnGroundClick(int data)
    {
        // If the handle input flag is set to false then do nothing.
        if(!handleInput)
            return;
        
        // The player is no longer headed for an interactable so set it to null.
//        currentInteractable = null;

        // This function needs information about a click so cast the BaseEventData to a PointerEventData.
        //PointerEventData pData = (PointerEventData)data;

        // Try and find a point on the nav mesh nearest to the world position of the click and set the destination to that.
        //UnityEngine.AI.NavMeshHit hit;
        //if (UnityEngine.AI.NavMesh.SamplePosition (pData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, UnityEngine.AI.NavMesh.AllAreas))
        //    destinationPosition = hit.position;
        //else
            // In the event that the nearest position cannot be found, set the position as the world position of the click.
        //    destinationPosition = pData.pointerCurrentRaycast.worldPosition;

        // Set the destination of the nav mesh agent to the found destination position and start the nav mesh agent going.
        //agent.SetDestination(destinationPosition);
        //agent.isStopped = false;
    }
}
