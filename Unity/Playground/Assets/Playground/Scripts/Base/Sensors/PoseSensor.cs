using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosMessageTypes.Geometry;

public class PoseSensor : Sensor {

    [HideInInspector] public PoseMsg pose;

    [Header("Pose Sensor Settings")]
    public GameObject target;

    void Start() {
        pose = new PoseMsg();
    }
    
    public override void GetData() {

        // Convert Unity data to ROS message
        pose.position.x = target.transform.position.x;
        pose.position.y = target.transform.position.y;
        pose.position.z = target.transform.position.z;

        pose.orientation.x = target.transform.rotation.x;
        pose.orientation.y = target.transform.rotation.y;
        pose.orientation.z = target.transform.rotation.z;
        pose.orientation.w = target.transform.rotation.w;

    }

    public override void ResetSensor() {
    }
}
