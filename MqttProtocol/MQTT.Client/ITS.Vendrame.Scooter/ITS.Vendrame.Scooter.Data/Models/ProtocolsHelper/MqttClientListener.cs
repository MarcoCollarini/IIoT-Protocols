using ITS.Vendrame.Scooter.Data.Models.MqttReceiveHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ITS.Vendrame.Scooter.Data.Models.ProtocolsHelper
{
    public class MqttClientListener
    {
        private MqttClient client;
        private string brokerAddress = "192.168.104.60";
        private bool status = false;
        
        public MqttClientListener(string topic)
        {
            client = new MqttClient(brokerAddress);

            byte code = client.Connect(Guid.NewGuid().ToString());

            client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
            client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            ushort subcribe = client.Subscribe(new string[] { topic },
            new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
            var bodyObj = JsonSerializer.Deserialize<DetectionModel>(Encoding.UTF8.GetString(e.Message));

            status = bodyObj.Status; 
        }
        private void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("Subscribed for id = " + e.MessageId);
        }

        public bool getStatus()
        {
            return status;
        }
    }
}