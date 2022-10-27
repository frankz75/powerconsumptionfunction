using System;
using System.Threading;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using MQTTnet.Client;
using MQTTnet;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using CaseOnline.Azure.WebJobs.Extensions.Mqtt.Messaging;
using CaseOnline.Azure.WebJobs.Extensions.Mqtt;
using ExampleFunction.AdvancedConfig;

namespace FunctionApp2
{
    public class Function1 : IFunction
    {
        public Function1 ()
        {
          
        }

        [FunctionName("SimpleFunction")]
        public static void AdvancedFunction(
            [MqttTrigger(typeof(ExampleMqttConfigProvider), "tele/tasmota/SENSOR")] IMqttMessage message,
            ILogger log)
        {
            var body = Encoding.UTF8.GetString(message.GetMessage());

            ConsumptionData consumptionData = (ConsumptionData)JsonConvert.DeserializeObject<ConsumptionData>(body);

            consumptionData.Time = new DateTime(consumptionData.Time.Ticks, DateTimeKind.Utc);

            using (var db = new PowerConsumptionContext())
            {
                //db.Database.EnsureCreated();

                var pc = new PowerConsumption { Time = consumptionData.Time, Power = consumptionData.Power.Power_curr, Consumption= consumptionData.Power.Total_in };
                db.Add(pc);
                db.SaveChanges();
            }


            log.LogInformation($"Advanced: message from topic {message.Topic} body: {body}");
        }

        //[FunctionName("Function1")]
        //public async Task Run([TimerTrigger("*/1 * * * * *")] TimerInfo myTimer, ILogger log)
        //{
        //    var mqttFactory = new MqttFactory();

        //    using (var mqttClient = mqttFactory.CreateMqttClient())
        //    {
        //        var mqttClientOptions = new MqttClientOptionsBuilder()
        //            .WithTcpServer("66fe1de71aa84c53be11e7a12d1ab845.s1.eu.hivemq.cloud", 8883)
        //            .WithCredentials("MySmlMQTTBroker", "mQHFpwi$6!")
        //            .WithTls()
        //            .WithCleanSession()
        //            .Build();


        //        // Setup message handling before connecting so that queued messages
        //        // are also handled properly. When there is no event handler attached all
        //        // received messages get lost.
        //        mqttClient.ApplicationMessageReceivedAsync +=MqttClient_ApplicationMessageReceivedAsync;
        //        //Handlers

        //        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        //        //using (var timeoutToken = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
        //        //{
        //        //    await mqttClient.ConnectAsync(mqttClientOptions, timeoutToken.Token);
        //        //}

        //        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
        //            .WithTopicFilter(
        //                f =>
        //                {
        //                    f.WithTopic("tele/tasmota/SENSOR");
        //                })
        //            .Build();

        //        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

        //        Console.WriteLine("MQTT client subscribed to topic.");

        //        Console.WriteLine("Press enter to exit.");
        //        Console.ReadLine();
        //    }
        //}

        

        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("Received application message.");

            string payload = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);

            ConsumptionData consumptionData = (ConsumptionData)JsonConvert.DeserializeObject<ConsumptionData>(payload);

            using (var db = new PowerConsumptionContext())
            {
                db.Database.EnsureCreated();

                var pc = new PowerConsumption { Time = consumptionData.Time.ToUniversalTime(), Power = consumptionData.Power.Power_curr,  Consumption= consumptionData.Power.Total_in};
                db.Add(pc);
                db.SaveChanges();
            }

            return Task.CompletedTask;
        }


       }

    public interface IFunction
    {

    }
}
