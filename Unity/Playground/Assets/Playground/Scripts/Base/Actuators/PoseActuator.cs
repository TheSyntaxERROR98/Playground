using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.Geometry;
using UnityEngine;

public class PoseActuator : Actuator<PoseMsg> {

    [Header("Pose Actuator Settings")]
    public GameObject target;
    public bool teleport;
    public float positionSpeed;
    public float rotationSpeed;
    private Quaternion _targetRotation;
    private Vector3 _targetPosition;


    public override void SetData(PoseMsg msg) {

        // Convert ROS pose message to Unity data

        _targetPosition = new Vector3(
            (float)msg.position.x,
            (float)msg.position.y,
            (float)msg.position.z
        );

        _targetRotation = new Quaternion(
            (float)msg.orientation.x,
            (float)msg.orientation.y,
            (float)msg.orientation.z,
            (float)msg.orientation.w
        );

        if (teleport) {

            // Teleport to target position
            target.transform.SetPositionAndRotation(_targetPosition, _targetRotation);
            return;
        }
    }

    public override void ResetActuator() {
    }

    void Update() {

        if (teleport) {
            return;
        }

        // Move towards target position
        target.transform.position = Vector3.MoveTowards(target.transform.position, _targetPosition, positionSpeed * Time.deltaTime);

        // Rotate towards target rotation
        target.transform.rotation = Quaternion.RotateTowards(target.transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);

    }
}
