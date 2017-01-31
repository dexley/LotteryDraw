# LotteryDraw

install MongoDB https://www.mongodb.com/download-center#enterprise and runit (mongodb, mongo)

Fire up the service using visual studio cassini and it should work. You may have to change the repository controller to point to your running mongo instance. I would have used config but ran out of time trying to get a working application. 

Can test the service using the swagger end point
http://localhost:50162/swagger/ui/index

Wasn't able to finish the front end in a timely fashion and would have spent too much time on it, with light that only 2 hours should be spent.

TODO
****

Validation in the LotteryService should be via specification (specification pattern) and injected in.

Ideally there should be an entity that represents the domain rather than using the contract/dto through the various layers of the application.

More tests especially around the specified requirements.

I would prefer to have some jasmine tests around the controllers.

I would prefer to have some end to end integration smoke tests. 

Ideally hosting options as at the moment IIS hosting is the only posibility but OWIN would be a good option also.

Use configuration to define the angularjs service endpoints to point to the correct url.





