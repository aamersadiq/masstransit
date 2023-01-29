Introduction
---------------
- Uses masstransit to sent/receive message from queue
- Uses masstransit to publish/subscribe to topics
- Switches between Azure Service and RabbitMQ based on the connection string
- Queues/Topics/Subscription and automatically created in ASB and RabbitMQ

Running the solution
---------------------
Use Producer /senttoqueue API to sent to queue 
Use Producer /publishtotopic API to publish to topic
Swicth between azure service bus and rabbitmq by changing service bus connection string

Using RabbitMq
----------------
Run the rabbitmq: docker run --rm -it --hostname my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
Use http://localhost:15672/ to log onto RabbitMq admin console using guest/guest