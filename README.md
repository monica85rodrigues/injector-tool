# Injector tool

A project to inject data in different destination sources

Supported recipients:

- [X] Kafka
- [ ] File
- [ ] SQL Server
- [ ] Mongo DB
- [ ] Cassandra

## Improvements to add

- [ ] Add IMessage interface in KafkaInjector with Key and Value. The client needs to have the ability to define the key.
- [ ] Add support for more serializers (the client can choose the serializer). For now only Json serializer is supported.