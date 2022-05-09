# url-shortener-service

Web API developed with .NET 5 for shortening URL's in two ways : Custom or Random.
User can pick a custom URL and transform the long URL's to it.
Also user can let service shorten the URL randomly to 6-digit hash portion.

For storing the shortened URL's, LiteDB (flexible NoSQL embedded database) is used. 

Project can be run locally, or in a Docker container (Docker file is prepared). More easier, project is deployed to Heroku, it can be used with the following endpoints.

API endpoints (http://url-shortener-mercedes.herokuapp.com) : 

| HTTP method   | POST            |
| :---          | :---            | 
| URI           | /shorten        |
| Request Body  | Example:{ "url": "https://medium.com/@hjhuney/implementing-a-random-forest-classification-model-in-python-583891c99652", "customUrl" : "forest" }|
| Response Type  | application/json|
| Response  | Example: { "url": "http://url-shortener-mercedes.herokuapp.com/forest", "customUrl" : "forest" }|

| HTTP method   | GET           |
| :---          | :---            | 
| URI           | /{urlChunk}      |
| Response Type  | application/json|
| Response  | Example:{ "url": "https://medium.com/@hjhuney/implementing-a-random-forest-classification-model-in-python-583891c99652", "customUrl" : "forest" }|

Note : If you leave the "customUrl" field while using POST method, service will assign a random value to the URL chunk (6-digit).

| HTTP method   | POST            |
| :---          | :---            | 
| URI           | /shorten        |
| Request Body  | Example:{ "url": "https://medium.com/@hjhuney/implementing-a-random-forest-classification-model-in-python-583891c99652 ", "customUrl" : "" }|
| Response Type  | application/json|
| Response  | Example: { "url": "http://url-shortener-mercedes.herokuapp.com/BAAAAA", "customUrl" : "BAAAAA" }|

# Examples : 
# Create a custom URL : 
![POST custom shorten](https://user-images.githubusercontent.com/43525350/167320407-2b6b71d8-a902-42bc-8eb3-3fe0e36ef0f5.png)
&nbsp;
![GET custom shorten](https://user-images.githubusercontent.com/43525350/167320412-9cf04955-1d5f-4d90-bbc5-56c1c14f29da.png)
&nbsp;&nbsp;
# Create a random URL : 
![POST random shorten](https://user-images.githubusercontent.com/43525350/167320447-6705fc4e-1321-4002-b769-b236af80205b.png)
&nbsp;
![GET random shorten](https://user-images.githubusercontent.com/43525350/167320451-15ad1e0d-a53c-41c3-8e3d-b2ec0e5f2d4a.png)
&nbsp;&nbsp;
# Overwrite a URL with custom URL : 
![POST custom shorten twice](https://user-images.githubusercontent.com/43525350/167320467-14dbed1b-c071-420e-8df1-38f696933652.png)
&nbsp;
![GET custom shorten twice](https://user-images.githubusercontent.com/43525350/167320475-5c9090e6-f320-4b80-a478-d609541db407.png)



