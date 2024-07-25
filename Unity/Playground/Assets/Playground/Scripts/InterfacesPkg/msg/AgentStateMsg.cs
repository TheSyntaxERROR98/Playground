//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.InterfacesPkg
{
    [Serializable]
    public class AgentStateMsg : Message
    {
        public const string k_RosMessageName = "interfaces_pkg/AgentState";
        public override string RosMessageName => k_RosMessageName;

        public Geometry.Vector3Msg position;
        public Geometry.Vector3Msg velocity;

        public AgentStateMsg()
        {
            this.position = new Geometry.Vector3Msg();
            this.velocity = new Geometry.Vector3Msg();
        }

        public AgentStateMsg(Geometry.Vector3Msg position, Geometry.Vector3Msg velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }

        public static AgentStateMsg Deserialize(MessageDeserializer deserializer) => new AgentStateMsg(deserializer);

        private AgentStateMsg(MessageDeserializer deserializer)
        {
            this.position = Geometry.Vector3Msg.Deserialize(deserializer);
            this.velocity = Geometry.Vector3Msg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.position);
            serializer.Write(this.velocity);
        }

        public override string ToString()
        {
            return "AgentStateMsg: " +
            "\nposition: " + position.ToString() +
            "\nvelocity: " + velocity.ToString();
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
