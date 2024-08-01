//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class AutonomousNavigationExampleEnvironmentStateRequest : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/AutonomousNavigationExampleEnvironmentState";
        public override string RosMessageName => k_RosMessageName;

        public BuiltinInterfaces.TimeMsg timestamp;

        public AutonomousNavigationExampleEnvironmentStateRequest()
        {
            this.timestamp = new BuiltinInterfaces.TimeMsg();
        }

        public AutonomousNavigationExampleEnvironmentStateRequest(BuiltinInterfaces.TimeMsg timestamp)
        {
            this.timestamp = timestamp;
        }

        public static AutonomousNavigationExampleEnvironmentStateRequest Deserialize(MessageDeserializer deserializer) => new AutonomousNavigationExampleEnvironmentStateRequest(deserializer);

        private AutonomousNavigationExampleEnvironmentStateRequest(MessageDeserializer deserializer)
        {
            this.timestamp = BuiltinInterfaces.TimeMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.timestamp);
        }

        public override string ToString()
        {
            return "AutonomousNavigationExampleEnvironmentStateRequest: " +
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
