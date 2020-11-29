using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace KafkaInjector
{
    public class KafkaInjector<T> : IInjector<T>
    {
        private readonly string _topicName;
        private readonly ProducerBuilder<string, byte[]> _builder;
    
        public KafkaInjector(ProducerConfig config, string topicName)
        {
            _topicName = topicName;
            _builder = new ProducerBuilder<string, byte[]>(config)
                .SetErrorHandler(OnError)
                .SetLogHandler(OnLog);
        }

        public async Task Run(T item)
        {
            Guard.ArgumentNotNull(item, nameof(item));
          
            Message<string, byte[]> message = new Message<string, byte[]>()
            {
                Key = Guid.NewGuid().ToString(),
                Value = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(item))
            };
            
            using (var producer = _builder.Build())
            {
                var result = await producer.ProduceAsync(_topicName, message);
                Console.WriteLine($"Message [key={result.Key ?? "null"},value={item}] delivered to {result.Topic} [{result.Partition}] @ {result.Offset}");
                producer.Flush(TimeSpan.FromMilliseconds(100));
            }
        }

        public Task Run(IEnumerable<T> items)
        {
            Guard.ArgumentNotNull(items, nameof(items));
            return Task.WhenAll(items.Select(this.Run));
        }
        
        private void OnError(IProducer<string, byte[]> producer, Error error)
        {
            Console.WriteLine($"\n[ERROR] { error.Reason }");
        }
        
        private void OnLog(IProducer<string, byte[]> producer, LogMessage logMessage)
        {
            Console.WriteLine($"[Log] Producer Name: {producer.Name}; Message: {logMessage}");
        }
    }
}