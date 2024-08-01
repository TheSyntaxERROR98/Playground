using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosMessageTypes.InterfacesPkg;
using System;


using ActionMsg = RosMessageTypes.InterfacesPkg.AutonomousNavigationExampleAgentActionMsg;
using StateMsg = RosMessageTypes.InterfacesPkg.AutonomousNavigationExampleAgentStateMsg;
using ResetMsg = RosMessageTypes.InterfacesPkg.AutonomousNavigationExampleAgentResetMsg;


public class AutonomousNavigationExampleAgent : Agent<
    ActionMsg,
    StateMsg,
    ResetMsg> {

    [Header("Sensors")]
    [SerializeField]
    private PoseSensor _poseSensor;
    [SerializeField]
    private PoseSensor _targetPoseSensor;
    [SerializeField]
    private LidarSensor _lidarSensor;
    [SerializeField]
    private TriggerSensor _collisionTriggerSensor;
    [SerializeField]
    private TriggerSensor _targetTriggerSensor;

    [Header("Actuators")]
    [SerializeField]
    private TwistActuator _twistActuator;
    [SerializeField]
    private PoseActuator _poseActuator;
    [SerializeField]
    private PoseActuator _targetPoseActuator;


    void Start() {
        
        // Initialize sensors list
        _sensors = new List<Sensor> {
            _poseSensor,
            _targetPoseSensor,
            _lidarSensor,
            _collisionTriggerSensor,
            _targetTriggerSensor
        };
    }

    public override void Action(ActionMsg action) {

        // Set actuator data
        _twistActuator.SetData(action.twist);
        
    }

    public override StateMsg State() {

        // Get sensor data
        foreach (Sensor sensor in _sensors) {
            sensor.GetData();
        }

        // Fill the response
        StateMsg state = new StateMsg {
            pose = _poseSensor.pose,
            target_pose = _targetPoseSensor.pose,
            laser_scan = _lidarSensor.laserScan,
            collision_trigger_sensor = _collisionTriggerSensor.triggerSensorMsg,
            target_trigger_sensor = _targetTriggerSensor.triggerSensorMsg
        };

        return state;
    }

    public override StateMsg ResetAgent(ResetMsg resetAction) {
        
        // Reset sensors
        foreach (Sensor sensor in _sensors) {
            sensor.ResetSensor();
        }
        
        // Reset actuators
        _twistActuator.ResetActuator();
        _poseActuator.SetData(resetAction.agent_target_pose);
        _targetPoseActuator.SetData(resetAction.target_target_pose);

        // Return the state
        return State();
        
    }
}
