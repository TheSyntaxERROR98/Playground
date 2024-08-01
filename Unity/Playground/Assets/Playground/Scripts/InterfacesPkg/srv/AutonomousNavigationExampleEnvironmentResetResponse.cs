//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class AutonomousNavigationExampleEnvironmentResetResponse : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/AutonomousNavigationExampleEnvironmentReset";
        public override string RosMessageName => k_RosMessageName;

        public BuiltinInterfaces.TimeMsg timestamp;
        public AutonomousNavigationExampleAgentStateMsg state;

        public AutonomousNavigationExampleEnvironmentResetResponse()
        {
            this.timestamp = new BuiltinInterfaces.TimeMsg();
            this.state = new AutonomousNavigationExampleAgentStateMsg();
        }

        public AutonomousNavigationExampleEnvironmentResetResponse(BuiltinInterfaces.TimeMsg timestamp, AutonomousNavigationExampleAgentStateMsg state)
        {
            this.timestamp = timestamp;
            this.state = state;
        }

        public static AutonomousNavigationExampleEnvironmentResetResponse Deserialize(MessageDeserializer deserializer) => new AutonomousNavigationExampleEnvironmentResetResponse(deserializer);

        private AutonomousNavigationExampleEnvironmentResetResponse(MessageDeserializer deserializer)
        {
            this.timestamp = BuiltinInterfaces.TimeMsg.Deserialize(deserializer);
            this.state = AutonomousNavigationExampleAgentStateMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.timestamp);
            serializer.Write(this.state);
        }

        public override string ToString()
        {
            return "AutonomousNavigationExampleEnvironmentResetResponse: " +
            "\ntimestamp: " + timestamp.ToString() +
            "\nstate: " + state.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize, MessageSubtopic.Response);
        }
    }
}
