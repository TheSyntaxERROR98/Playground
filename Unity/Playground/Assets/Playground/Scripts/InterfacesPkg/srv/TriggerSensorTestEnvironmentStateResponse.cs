//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class TriggerSensorTestEnvironmentStateResponse : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/TriggerSensorTestEnvironmentState";
        public override string RosMessageName => k_RosMessageName;

        public BuiltinInterfaces.TimeMsg timestamp;
        public TriggerSensorTestAgentStateMsg state;

        public TriggerSensorTestEnvironmentStateResponse()
        {
            this.timestamp = new BuiltinInterfaces.TimeMsg();
            this.state = new TriggerSensorTestAgentStateMsg();
        }

        public TriggerSensorTestEnvironmentStateResponse(BuiltinInterfaces.TimeMsg timestamp, TriggerSensorTestAgentStateMsg state)
        {
            this.timestamp = timestamp;
            this.state = state;
        }

        public static TriggerSensorTestEnvironmentStateResponse Deserialize(MessageDeserializer deserializer) => new TriggerSensorTestEnvironmentStateResponse(deserializer);

        private TriggerSensorTestEnvironmentStateResponse(MessageDeserializer deserializer)
        {
            this.timestamp = BuiltinInterfaces.TimeMsg.Deserialize(deserializer);
            this.state = TriggerSensorTestAgentStateMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.timestamp);
            serializer.Write(this.state);
        }

        public override string ToString()
        {
            return "TriggerSensorTestEnvironmentStateResponse: " +
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
