using UnityEngine;

public class MoveTransformCommand : Command
{
    private Transform targetTransform;
    private Vector3 targetPosition;
    private Vector3 previousPosition;

    public MoveTransformCommand(Transform targetTransform, Vector3 targetPosition)
    {
        this.targetTransform = targetTransform;
        this.targetPosition = targetPosition;
        previousPosition = targetTransform.position;
    }
    
    public override void Execute()
    {
        targetTransform.position = targetPosition;
    }

    public override void Undo()
    {
        targetTransform.position = previousPosition;
    }
}
