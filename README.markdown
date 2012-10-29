CQRS-Event-Sourcing
===================

A quick proof of concept created just to try and learn: how an event store works, and CQRS and event sourcing concepts.

To run do the following:  
 1) Create the tables by deploying the sql database projects  
 2) Verify your connection strings are correct  
    a) The write solution needs a connection to the event store database  
    b) The read solution needs a connection to the read database schema  
 3) Use Nuget to download the libraries for each solution  
    a) Presentation needs MassTransit.RabbitMQ and Ninject.MVC3 on the executing assembly  
    b) Write needs MassTransit.RabbitMQ, Ninject and Topshelf on the WriteService assembly  
    c) Write also needs Ninject and Json.Net on the Infrastructure assembly  
    d) Read needs Ninject.WCF on the Application assembly  
    e) Read needs MassTransit.RabbitMQ, Ninject and Topshelf on the ReadService assembly  
 4) Install Erlang and RabbitMQ (verify the service started)  
 5) Build and run  
    a) The write solution  
    b) The read solution  
    c) The presentation solution  
    
This solution is extremely minimal and was built from scratch to simply learn the concepts.
This solution still lacks a snapshotter so all aggregates will get loaded by their complete
event history instead of from the latest snapshot.  I may build this later on just to learn
about how it works.

In a production system you would probably want to use a framework for your event store like
Jonathan Oliver's or Greg Young's EventStore projects.