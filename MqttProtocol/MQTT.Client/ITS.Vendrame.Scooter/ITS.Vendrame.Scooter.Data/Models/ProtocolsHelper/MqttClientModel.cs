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
        public MqttClientModel(string brokerAddress)
        {
            client = new MqttClient(brokerAddress,10234,false,null,null,MqttSslProtocols.SSLv3);

            var code = client.Connect(Guid.NewGuid().ToString());
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
