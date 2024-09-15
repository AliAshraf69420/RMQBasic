
- ### What’s Message Queueing ?
	- Basically where a publisher sends a message to a queue and a consumer (someone subscribed to the queue) receives the message

---
- ### What’s Exchange / Queue ?
	Queue:
		_A queue_ is the name for the post box in RabbitMQ. Although messages flow through RabbitMQ and your applications, they can only be stored inside a _queue_. A _queue_ is only bound by the host's memory & disk limits, it's essentially a large message buffer.
	Exchange:
		an exchange is a component that routes messages from producers to message queues based on routing rules. It acts as an intermediary, determining how and where to send messages. Exchanges are crucial for the flow of messages in RabbitMQ, as they dictate how messages are distributed to the appropriate queues.
---
- ### Launch RabbitMQ server on Docker
	`Docker pull image`
	`Docker run container`
		`$ docker run -d --hostname my-rabbit --name some-rabbit -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management`
		
		$ docker run -d --hostname my-rabbit --name some-rabbit -p 8080:15672
		rabbitmq:3-management
		
---
- ### Write 3 console apps: (Declarative + Publish + Consume)
	[how declarative clustering works](https://www.rabbitmq.com/docs/clustering)
	
---
- ### Run Multiple consumers = scalability
	Achieves high availability as well
---
- ### With SAC -> high availability but not scalability 
	used to keep consistency for the messages 
---
- ### Different Exchanges (Direct – Fanout – Routing – Topic)
	1. Direct (from what I read it's the same as routing but I'm not sure)
		basically using a routing key and if the routing key matches then a message is send to that receiver
	2. Fanout
		using an exchange to broadcast a message, practically sending it to every consumer
			for sender:
			 `string exchangeName = "logs";`
             `channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");`
             `channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");`
	           `channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);` 
		    for receiver:
	        `string exchangeName = "logs";`
            `channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");
			
            // Declare a queue and bind it to the exchange
            string queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: exchangeName,
            routingKey: "");
	3. Topic
		-  binding key / routing key "multicasting" = Topic basically it's like direct exchange but instead of a specific routing key it looks for patterns there's also a headers exchange type that looks up a key value pair to send specific messages to specific queues 
		- In case you run into this exception (in different file, couldn't figure out how to add it to this one)
		
---
- ### Message durability
		 When RabbitMQ quits or crashes it will forget the queues and messages unless you tell it not to. Two things are required to make sure that messages aren't lost: we need to mark both the queue and messages as durable.
	First, we need to make sure that the queue will survive a RabbitMQ node restart. In order to do so, we need to declare it as _durable_: 
	`durable: true, // in queue declaration` 
---
### some sources I used or found and thought was worth checking out :
[Start out with this](https://www.rabbitmq.com/tutorials#queue-tutorials) \n
[Docker Image for RMQ](https://hub.docker.com/_/rabbitmq) \n
[Good blog about SAC but it's written in java](https://www.rabbitmq.com/blog/2022/07/05/rabbitmq-3-11-feature-preview-single-active-consumer-for-streams) \n
[Talks about Exchange types](https://www.cogin.com/articles/rabbitmq/rabbitmq-exchanges-guide.php) \n
[Routing Exchange](https://www.rabbitmq.com/tutorials/tutorial-four-dotnet) \n
[Message Durability](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet#message-durability) \n
[outdated .NET 6 yet insightful video on RMQ](https://www.youtube.com/watch?v=eEipVEq8F1k) \n
[Protocol that is supported by RMQ](https://www.rabbitmq.com/tutorials/amqp-concepts#exchange-fanout) \n
[Acks (Basically the consumer letting the publisher know that the message was successfully received) ](https://www.rabbitmq.com/docs/confirms) \n




