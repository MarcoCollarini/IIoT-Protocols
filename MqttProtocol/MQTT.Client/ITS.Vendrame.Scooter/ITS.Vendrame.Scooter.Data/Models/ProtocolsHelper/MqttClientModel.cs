using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ITS.Vendrame.Scooter.Data.Models.ProtocolsHelper
{
    public class MqttClientModel
    {
        private MqttClient client;
        private string brokerAddress = "192.168.104.60";

        public MqttClientModel()
        {
            client = new MqttClient(brokerAddress,1883,false,null,null,MqttSslProtocols.SSLv3);

            var code = client.Connect(Guid.NewGuid().ToString(),null,null,true,1,true,"lastWill","Mi sono disconnesso", true, 200);
        }

        public void SendMsgAsync(string topic, string message)
        {
            ushort msgId = client.Publish(topic,
                                          Encoding.UTF8.GetBytes(message),
                                          MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                                          false);
        }
    }
}
