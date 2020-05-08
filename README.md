# Url Shortener

Simple url shortener built with [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1) and [MongoDB](https://www.mongodb.com/)

## Routes

-   Shorten url

    -   URL: `/urls`
    -   Method: POST

    -   Request body:

        ```json
        {
            "url": "https://github.com"
        }
        ```

    -   Response:
        -   Code: 201 Created
        -   Headers:
            -   Location `https://localhost:5001/14b533`
        -   Body:
            ```json
            {
                "shortUrl": "https://localhost:5001/14b533",
                "expireAt": "2020-05-20T17:12:15.4789353Z"
            }
            ```

-   Shorten url

    -   URL: `/:id`
    -   Method: GET
    -   Params:
        -   id: shorten url id

    Example:
    GET `https://localhost:5001/14b533`

    -   Response:
        -   Code: 302 Found
        -   Headers:
            -   Location `https://github.com`
        -   Body:
            None
    -   Response if fail:
        -   Code: 404 Not Found
        -   Body:
            ```json
            {
                "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                "title": "Not Found",
                "status": 404,
                "traceId": "|1cfccbe9-486bb72a18c8f720."
            }
            ```

## Getting started

First, restore dependecies

```
dotnet restore
```

To run place a connection string to MongoDB in appsettings then run

```
dotnet run --project src/UrlShortener.WebApi
```

## Run with docker

Build docker image

```
docker build src/UrlShortner.WebApi  -t url-shortener
```

Run the app container

```
docker run -it -p 8080:80 -e ConnectionStrings__MongoConnection="CONNECTION_STRING" --name url-shortener-app url-shortener
```

The app will be availible in `http://localhost:8080`
