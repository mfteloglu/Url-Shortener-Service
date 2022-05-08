# url-shortener-service

API endpoints : 

| HTTP method   | POST            |
| :---          | :---            | 
| URI           | /shorten        |
| Request Body  | Example:{ "url": "https://medium.com/@hjhuney/implementing-a-random-forest-classification-model-in-python-583891c99652", "customUrl" : "forest" }|
| Reponse Type  | application/json|
| Response  | Example: { "url": "http://url-shortener-mercedes.herokuapp.com/forest", "customUrl" : "forest" }|

| HTTP method   | GET           |
| :---          | :---            | 
| URI           | /{hash}      |
| Reponse Type  | application/json|
| Response  | Example:{ "url": "https://medium.com/@hjhuney/implementing-a-random-forest-classification-model-in-python-583891c99652", "customUrl" : "forest" }|

Note : If you leave the "customUrl" field while using POST method, service will assign a random value to the URL chunk (6-digit).

| HTTP method   | POST            |
| :---          | :---            | 
| URI           | /shorten        |
| Request Body  | Example:{ "url": “https://medium.com/@hjhuney/implementing-a-random-forest-classification-model-in-python-583891c99652 “, "customUrl" : "" }|
| Reponse Type  | application/json|
| Response  | Example: { "url": “http://url-shortener-mercedes.herokuapp.com/BAAAAA“, "customUrl" : "BAAAAA" }|

Examples : 
![POST custom shorten](https://user-images.githubusercontent.com/43525350/167319820-68e56b8b-b4b3-44a4-814b-10f5dbdc3041.png)
![GET custom shorten](https://user-images.githubusercontent.com/43525350/167319826-edd560cc-a608-4183-88bb-a5486c67b152.png)
![POST random shorten](https://user-images.githubusercontent.com/43525350/167319831-cc88146d-e411-4883-8e95-7ee61ec57184.png)
![GET random shorten](https://user-images.githubusercontent.com/43525350/167319834-7bf2918f-02b2-46d3-a768-9f57213d737d.png)
