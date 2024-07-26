//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class LidarTestEnvironmentStepRequest : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/LidarTestEnvironmentStep";
        public override string RosMessageName => k_RosMessageName;

        public LidarTestAgentActionMsg agent_action;
        public bool reset;

        public LidarTestEnvironmentStepRequest()
        {
            this.agent_action = new LidarTestAgentActionMsg();
            this.reset = false;
        }

        public LidarTestEnvironmentStepRequest(LidarTestAgentActionMsg agent_action, bool reset)
        {
            this.agent_action = agent_action;
            this.reset = reset;
        }

        public static LidarTestEnvironmentStepRequest Deserialize(MessageDeserializer deserializer) => new LidarTestEnvironmentStepRequest(deserializer);

        private LidarTestEnvironmentStepRequest(MessageDeserializer deserializer)
        {
            this.agent_action = LidarTestAgentActionMsg.Deserialize(deserializer);
            deserializer.Read(out this.reset);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.agent_action);
            serializer.Write(this.reset);
        }

        public override string ToString()
        {
            return "LidarTestEnvironmentStepRequest: " +
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
