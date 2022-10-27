using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MQTTnet.Extensions.ManagedClient;
using CaseOnline.Azure.WebJobs.Extensions.Mqtt.Config;
using CaseOnline.Azure.WebJobs.Extensions.Mqtt.Bindings;
using MQTTnet.Client.Options;

namespace ExampleFunction.AdvancedConfig
{
    public class ExampleMqttConfigProvider : ICreateMqttConfig
    {
        public CustomMqttConfig Create(INameResolver nameResolver, ILogger logger)
        {
            var connectionString = new MqttConnectionString(nameResolver.Resolve("MqttConnection"), "CustomConfiguration");

            var options = new ManagedMqttClientOptionsBuilder()
                   .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                   .WithClientOptions(new MqttClientOptionsBuilder()
                        .WithClientId(connectionString.ClientId.ToString())
                        .WithTcpServer(connectionString.Server, connectionString.Port)
                        .WithCredentials(connectionString.Username, connectionString.Password)
                        .WithTls(new MqttClientOptionsBuilderTlsParameters { UseTls=connectionString.Tls })
                        .Build())
                   .Build();

            return new MqttConfigExample("CustomConnection", options);
        }
    }
}
