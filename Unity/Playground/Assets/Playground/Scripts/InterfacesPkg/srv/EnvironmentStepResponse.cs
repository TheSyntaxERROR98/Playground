//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class EnvironmentStepResponse : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/EnvironmentStep";
        public override string RosMessageName => k_RosMessageName;

        public AgentStateMsg agent_state;

        public EnvironmentStepResponse()
        {
            this.agent_state = new AgentStateMsg();
        }

        public EnvironmentStepResponse(AgentStateMsg agent_state)
        {
            this.agent_state = agent_state;
        }

        public static EnvironmentStepResponse Deserialize(MessageDeserializer deserializer) => new EnvironmentStepResponse(deserializer);

        private EnvironmentStepResponse(MessageDeserializer deserializer)
        {
            this.agent_state = AgentStateMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.agent_state);
        }

        public override string ToString()
        {
            return "EnvironmentStepResponse: " +
            "\nagent_state: " + agent_state.ToString();
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
