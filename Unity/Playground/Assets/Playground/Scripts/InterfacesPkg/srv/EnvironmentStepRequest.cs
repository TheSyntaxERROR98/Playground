//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class EnvironmentStepRequest : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/EnvironmentStep";
        public override string RosMessageName => k_RosMessageName;

        public AgentActionMsg agent_action;
        public bool reset;

        public EnvironmentStepRequest()
        {
            this.agent_action = new AgentActionMsg();
            this.reset = false;
        }

        public EnvironmentStepRequest(AgentActionMsg agent_action, bool reset)
        {
            this.agent_action = agent_action;
            this.reset = reset;
        }

        public static EnvironmentStepRequest Deserialize(MessageDeserializer deserializer) => new EnvironmentStepRequest(deserializer);

        private EnvironmentStepRequest(MessageDeserializer deserializer)
        {
            this.agent_action = AgentActionMsg.Deserialize(deserializer);
            deserializer.Read(out this.reset);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.agent_action);
            serializer.Write(this.reset);
        }

        public override string ToString()
        {
            return "EnvironmentStepRequest: " +
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
