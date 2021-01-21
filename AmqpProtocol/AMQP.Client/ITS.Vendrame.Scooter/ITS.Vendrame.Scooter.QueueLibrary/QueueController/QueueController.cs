using ITS.Vendrame.Scooter.Data.Models.ProtocolsHelper;
using ITS.Vendrame.Scooter.Data.Models.SensorsModel;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ITS.Vendrame.Scooter.QueueLibrary.QueueController
{
    public class QueueController
    {
        AmqpClientModel amqpClientModel;
        readonly ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("127.0.0.1 , abortConnect=false");
        IDatabase conn;
        readonly RedisKey redisKey = new RedisKey("messages");

        public QueueController()
        {
            conn = muxer.GetDatabase();
        }


        public void CheckIfDataIsPresent()
        {
           var len = conn.ListLength(redisKey);
           // Console.WriteLine(len.Result);

            if (len > 0)
            {
                SendData();
            }
        }

        public void InsertIntoList(SensorSampleModel jsonSensorModel)
        {
            // queue.Push(jsonSensorModel);
            RedisValue redisValue = new RedisValue(JsonSerializer.Serialize(jsonSensorModel));
            conn.ListRightPush(redisKey, redisValue);
        }

        public void SendData()
        {
            if (amqpClientModel == null)
            {
                amqpClientModel = new AmqpClientModel();
            }
            
            //ar message = queue.Pop();
            while(conn.ListLength(redisKey) > 0)
            {
                var data = conn.ListRightPop(redisKey).ToString();

                amqpClientModel.PublishMessage(data);

                Console.WriteLine("Sent: " + data);
            }
            
        }
    }
}
