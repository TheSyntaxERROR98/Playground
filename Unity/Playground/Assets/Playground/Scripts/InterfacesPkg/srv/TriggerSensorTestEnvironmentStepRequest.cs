//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class TriggerSensorTestEnvironmentStepRequest : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/TriggerSensorTestEnvironmentStep";
        public override string RosMessageName => k_RosMessageName;

        public TriggerSensorTestAgentActionMsg agent_action;
        public bool reset;

        public TriggerSensorTestEnvironmentStepRequest()
        {
            this.agent_action = new TriggerSensorTestAgentActionMsg();
            this.reset = false;
        }

        public TriggerSensorTestEnvironmentStepRequest(TriggerSensorTestAgentActionMsg agent_action, bool reset)
        {
            this.agent_action = agent_action;
            this.reset = reset;
        }

        public static TriggerSensorTestEnvironmentStepRequest Deserialize(MessageDeserializer deserializer) => new TriggerSensorTestEnvironmentStepRequest(deserializer);

        private TriggerSensorTestEnvironmentStepRequest(MessageDeserializer deserializer)
        {
            this.agent_action = TriggerSensorTestAgentActionMsg.Deserialize(deserializer);
            deserializer.Read(out this.reset);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.agent_action);
            serializer.Write(this.reset);
        }

        public override string ToString()
        {
            return "TriggerSensorTestEnvironmentStepRequest: " +
            "\nagent_action: " + agent_action.ToString() +
            "\nreset: " + reset.ToString();
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
