//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class TriggerSensorTestEnvironmentStateRequest : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/TriggerSensorTestEnvironmentState";
        public override string RosMessageName => k_RosMessageName;

        public BuiltinInterfaces.TimeMsg timestamp;

        public TriggerSensorTestEnvironmentStateRequest()
        {
            this.timestamp = new BuiltinInterfaces.TimeMsg();
        }

        public TriggerSensorTestEnvironmentStateRequest(BuiltinInterfaces.TimeMsg timestamp)
        {
            this.timestamp = timestamp;
        }

        public static TriggerSensorTestEnvironmentStateRequest Deserialize(MessageDeserializer deserializer) => new TriggerSensorTestEnvironmentStateRequest(deserializer);

        private TriggerSensorTestEnvironmentStateRequest(MessageDeserializer deserializer)
        {
            this.timestamp = BuiltinInterfaces.TimeMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.timestamp);
        }

        public override string ToString()
        {
            return "TriggerSensorTestEnvironmentStateRequest: " +
            "\ntimestamp: " + timestamp.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
