# CONTACTS README

#### *PREPARED BY Carl Wessels*


## 1. SUMMARY
---
    This is a solution to the assignment to create a simple contacts application. The core is written with the mindset that this will be a consumer-agnostic approach, and not purely built for just the current web client. 
    The back-end is written in .net core, C#, with a REST API client. Initially the web would be done in ASP Razor pages, but after some time was spent on doing so and re-evaluation, it was changed to an Angular project. As you will see in the solution, the bulk of my skill lies in the back-end, and not the front. I have many years of “full stack” experiences, but the front end work of that is small compared to the back. 
    
    If I went with what I know, I would’ve gone with Asp Razor pages, but I thought I would rather take the “better” route, and go with Angular. I also focused more on the back-end, as the front-end can easily be replaced out, but the back-end can’t. I wanted the back-end to work 100%.

    Initially my concept was to have the option of having many PhoneBooks, but as time ran out I realized that this would not be doable with the time left, and I only implemented one single phonebook from the web project. This is the only “hack” that I did, and you can see the code-smell on the controllers where if a phonebookId is not supplied, I assume it’s the “default” phonebook. Not ideal. 
    
    Had I more time, I would have implemented this on the web as well,
    
    I wanted to do much more on this than came to light in the final product, but due to time constraints I could.

---
## 2. Back-end
---
### 2.1 Dependency Injection
---
    I utilized the DI that comes with .net core to its full extent. Every service is an implementation of an interface, and could be replaced with another implementation.
---
### 2.2 Contacts.Common
---
    Initially I would have done the front-end in ASP, and would have used this common project for common objects and interfaces. Now that the front-end is in Angular, this project is redundant. This could probably be removed, as projects like these creep up and create more points of maintenance. 
---
### 2.3 Contacts.Service
---
---        
#### 2.3.1 Database
---
    I decided to rather go with SQLite, as the database portion of the task seemed lightweight, and all the business logic would happen in the backend, as opposed to the database. I created a base dataobject class which does all the CRUD work, as opposed to having to write the CRUD operations in every model. I suppose with there only being two models this might be a bit of overengineering, but it’s a much cleaner, reusable and easily maintainable platform this way. Every model also has a database-specific datalayer. This means that if we ever decide to use another database, the models would not have to be changed to do the CRUD’ding, only the datalayers would have to be re-implemented.
---
#### 2.3.2 Security
---
    If time permitted, I would have implemented JWT authentication. This would also mean I would have to add some layer/mode of users as well. The JWT would only feature an EXP and UID (userId) claim
---
#### 2.3.3 Configuration
---
    One of the features that I love about .net core, is the configuration. The fact that there are built-in features for “layered runtime configuration” (environmental configuration, command line args, appsettings) makes any .net core app very easy to configure. I also like the use of the Options-pattern, so I can either inject the whole configuration root into a service, or just a subsection of this. This becomes especially powerful when you have multi-project solutions, where you have common configuration classes. 
---
#### 2.3.5 Logging
---
    For any project that I use, I use SeriLog. The power that it gives you, combined with the easy syntax of logging and easy configuration makes this product a breeze to work with. I couldn’t use it extensively during this project, as I would have created unnecessary over-engineering, just to prove a point. 
---
#### 2.3.6 Contacts.UnitTests
---
    For this project I’ve used XUnit as a framework. I’ve tried using Moq before, but I didn’t like the way that Moq does the testing. If I recall correctly my issue was that I couldn’t have dynamic expected results, only hard coded results. The only reason that I use XUnit versus the more popular NUnit, is that XUnit can run tests both synchronously and asynchronously. My tests are pretty simple and light-weight, due to the size of the project. 

    I do have some implementation in the unit testing project, and some would argue that this is a fault, but I feel that the implementation belongs there, as we are not testing, for example, the configuration/service scaffolding.

---
## 3 Front-end
---
### 3.1 Contacts web
---
    The decision to go with Angular was a difficult one to make. I could’ve probably delivered a better web page at the end of the day, but it would’ve been in the wrong framework. The project was done with vanilla Angular experience, and in a couple of hours I managed to create something from a lot of googling and StackOverflowing. There is room for improvement on various levels, and currently routing to an Element is not functional. I can see that the wrong Id is being passed through, but I was unable to determine what the cause of this is. I am fairly certain that there is a very simple reason for this, but I just don’t know where to look. Again, time was my enemy here.

    The same for the unit tests. At a glance Angular's unit tests look easy to implement, and very convenient as they live right next to their implementations. Unfortunately I am unable to get these to work, as you will see, and unable to get the injection working properly. With more time, I would have loved to get these test to work as they seem really powerful.

    As mentioned earlier, this implementation only caters for a single PhoneBook. My plan was to get the one working properly, and then implementing the solution to work with multiple. Unfortunately, I never managed to get that far.