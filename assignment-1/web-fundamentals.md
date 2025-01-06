## Web Fundamentals 

How website works 

When you click on 'www.google.com' in url in browser

The request goes from ISP (Internet Service Provider) to DNS servers.

DNS is Domain Name Server.

which maps domains with their particular ip address.

Then the request goes to the server where the website is hosted. Because server only understands ip address but for humans it is difficult to remember ip address so we have domain names.

Then the server sends the response back to the browser.

The browser then renders the response and displays the website.

The website is made up of HTML, CSS, and JavaScript for frontend generally.

HTML is the structure of the website.

When you make a request to server communication is done through HTTP (Hyper Text Transfer Protocol).

Protocol is a set of rules that two sides agree to use when communicating.

https is secure version of http.

When you make a request to server it sends back a http response.In HTTP header it contains info like data type, data length, status code etc.

HTTP status codes are 3 digit numbers that tell us the status of the request.like 404 for not found, 200 for ok etc.

There is also Bearer token which is used for authentication.

When you login to a website it sends back a token which is stored in browser and sent back to server for every request.

This is how authentication is done.

There are 2 ways to store token in browser : Cookies and Local Storage. Cookies are sent with every request to server but local storage is not.

HTTP only cookies are more secure than local storage.


### REST API

There multiple ways to make a request to server like GET, POST, PUT, DELETE etc.

GET is for fetching data from server.
POST is for sending data to server.
PUT is for updating data on server.
DELETE is for deleting data on server.

There are also other methods like PATCH, OPTIONS, HEAD etc.

When you make a request to server it sends back a response.


REST stands for Representational State Transfer.
REST API is a set of rules that we follow when we create API.Rest API is used to communicate between client and server.

API is Application Programming Interface.

REST API uses JSON ( Javascript object Notation ) as data format.

REST APIS are stateless . It means every request is independent (doesn't have to do anything with previous request) and in request header every necessary info should be passed to it .

### SOAP API -

Simple Object Access Protocol - is more secured

Communincation is done in XML format.

Frotend API Code :

```js
const axios = require('axios');

const makeSoapCall = async () => {
  const url = 'http://localhost:3000/calculator';
  const xml = `
    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:cal="http://example.com/CalculatorService/">
       <soapenv:Header/>
       <soapenv:Body>
          <cal:Add>
             <a>5</a>
             <b>3</b>
          </cal:Add>
       </soapenv:Body>
    </soapenv:Envelope>
  `;

  try {
    const response = await axios.post(url, xml, {
      headers: {
        'Content-Type': 'text/xml',
      },
    });
    console.log(response.data);
  } catch (err) {
    console.error(err);
  }
};

makeSoapCall();
```

### GraphQL APIs -

GraphQL is query language

For GraphQL we need Apollo Client 


Frontend React code :

```js
import React from 'react';
import {
  ApolloClient,
  InMemoryCache,
  ApolloProvider,
  useQuery,
  gql,
} from '@apollo/client';

const client = new ApolloClient({
  uri: 'http://localhost:4000/graphql', // Replace with your GraphQL server URL
  cache: new InMemoryCache(),
});

const GET_USERS = gql`
  query GetUsers {
    users {
      id
      name
      email
    }
  }
`;

```

