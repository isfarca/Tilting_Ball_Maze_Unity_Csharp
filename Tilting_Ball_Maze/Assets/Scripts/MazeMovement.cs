using UnityEngine;

public class MazeMovement : MonoBehaviour
{
    // Consts for minimum / maximum rotation of the maze.
    private const int MINIMUM = -10;
    private const int MAXIMUM = 10;

    // Rotation values over the minimum / maximum.
    private bool isAbove = false;

    // Game object to move.
    [SerializeField] private Transform maze;
    [SerializeField] private Rigidbody sphereRigidbody;

    /// <summary>
    /// Active maze movement and passive sphere movement. 
    /// </summary>
    private void FixedUpdate()
    {
        // Check, if the rotation values in minimum / maximum.

        // X.
        if (maze.eulerAngles.x <= MINIMUM) // Minimum.
        {
            maze.eulerAngles = new Vector3(MINIMUM, maze.eulerAngles.y, maze.eulerAngles.z);
        }
        else if (maze.eulerAngles.x >= MAXIMUM) // Maximum.
        {
            maze.eulerAngles = new Vector3(MAXIMUM, maze.eulerAngles.y, maze.eulerAngles.z);
        }

        // Z.
        if (maze.eulerAngles.z <= MINIMUM) // Minimum.
        {
            maze.eulerAngles = new Vector3(maze.eulerAngles.x, maze.eulerAngles.y, MINIMUM);
        }
        else if (maze.eulerAngles.z >= MAXIMUM) // Maximum.
        {
            maze.eulerAngles = new Vector3(maze.eulerAngles.x, maze.eulerAngles.y, MAXIMUM);
        }

        // Set the above value true, when a condition true.
        isAbove = maze.eulerAngles.x <= MINIMUM || maze.eulerAngles.x >= MAXIMUM || maze.eulerAngles.z <= MINIMUM || maze.eulerAngles.z >= MAXIMUM;

        // False, than increment / decrement rotation value.
        if (isAbove)
        {
            maze.eulerAngles = new Vector3(Input.GetAxis("Vertical") * MAXIMUM, maze.eulerAngles.y, Input.GetAxis("Horizontal") * MAXIMUM);
        }

        // Roll the sphere by rotation values.
        sphereRigidbody.AddForce(Vector3.forward * maze.eulerAngles.z);
        sphereRigidbody.AddForce(Vector3.right * maze.eulerAngles.x);
    }
}