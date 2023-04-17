using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Stack<Command> undoStack;
    private Stack<Command> redoStack;

    private void Awake()
    {
        undoStack = new Stack<Command>();
        redoStack = new Stack<Command>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveInDirection(Vector3.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveInDirection(Vector3.right);
        if (Input.GetKeyDown(KeyCode.UpArrow)) MoveInDirection(Vector3.up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) MoveInDirection(Vector3.down);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (undoStack.Count > 0)
            {
                Command command = undoStack.Peek();
                if (command != null)
                {
                    command.Undo();
                    redoStack.Push(command);
                    undoStack.Pop();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (redoStack.Count > 0)
            {
                Command command = redoStack.Peek();
                if (command != null)
                {
                    command.Execute();
                    undoStack.Push(command);
                    redoStack.Pop();
                }
            }
        }
    }

    private void MoveInDirection(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction;

        Command moveCommand = new MoveTransformCommand(transform, targetPosition);

        moveCommand.Execute();

        undoStack.Push(moveCommand);
        
        redoStack.Clear();
    }
}
