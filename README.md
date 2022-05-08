# url-shortener-service

API endpoints : 

| HTTP method   | POST            |
| :---          | :---            | 
| URI           | /shorten        |
| Request Body  | Example:{ "url": “https://medium.com/@hjhuney/implementing-a-random-forest-classification-model-in-python-583891c99652 “, "customUrl" : "forest" }|
| Reponse Type  | application/json|
| Response  | Example: { "url": “http://url-shortener-mercedes.herokuapp.com/forest“, "customUrl" : "forest" }|

| HTTP method   | GET           |
| :---          | :---            | 
| URI           | /{hash}      |
| Reponse Type  | application/json|
| Response  | Example:{ url: “https://medium.com/@hjhuney/implementing-a-random-forest-classification-model-in-python-583891c99652 “ }|
