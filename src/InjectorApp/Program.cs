using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using Confluent.Kafka;
using KafkaInjector;

namespace InjectorApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start to inject data");

            await InjectStringToKafka();
            await InjectStringListToKafka();
            await InjectSampleMessageObjectToKafka();
            
            Console.WriteLine("Inject data finished");
        }

        static async Task InjectStringToKafka()
        {
            const string topicName = "test-topic-1";
            const string message = "Hello Monica 2";
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka-broker:9092",
                Acks = Acks.None
            };
            
            IInjector<string> injector = new KafkaInjector<string>(config, topicName);
            await injector.Run(message);
        }

        static async Task InjectStringListToKafka()
        {
            const string topicName = "test-topic-1";
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka-broker:9092",
                Acks = Acks.None
            };
            var messageList = new List<string>()
            {
                "monica A",
                "monica B",
                "monica C"
            };
            
            IInjector<string> injector = new KafkaInjector<string>(config, topicName);
            await injector.Run(messageList);
        }

        static async Task InjectSampleMessageObjectToKafka()
        {
            const string topicName = "test-topic-1";
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka-broker:9092",
                Acks = Acks.None
            };
            var anotherMessage = new SampleMessage()
            {
                Id = new Random().Next(),
                Name = "monica"
            };
            
            IInjector<SampleMessage> injector2 = new KafkaInjector<SampleMessage>(config, topicName);
            await injector2.Run(anotherMessage);
        }
    }
}