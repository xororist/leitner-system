# Leitner System
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=xororist_leitner-system&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=xororist_leitner-system)
## Summary
Studies project to implement the leitner system for learning, following Clean Code & Hexagonal Architecture principles.

This project has been developed with:
- .NET 8 / C#
- Nuxt3 / Typescript
- Docker
- MongoDB
- Github Actions

To run the app use:

```
git clone https://github.com/xororist/leitner-system.git
cd leitner-system
docker compose up --build
```

The Dockerfile for the frontend application is running nuxt3 in dev mode to be able to use NuxtUI Pro without licence.

The url frontend application is:
```
http://localhost:3000/
```

The url for the backend application that handle the endpoints for API calls is:
```
http://localhost:8080/cards
```

## Github Actions

The pipeline build the .net 8 application, then execute the test and finally build and push two separate images for frontend and backend to dockerhub.

## API

### Availble endpoints:

To get all cards
```
GET http://localhost:4321/cards
```

To get all cards with a specific tag
```
POST http://localhost:8080/cards?tags=Architecture
```

To create a card
```
POST http://localhost:8080/cards
```

To update a question/answer/tag of a card
```
PATCH http://localhost:8080/cards
```

To answer a card
```
POST http://localhost:8080/cards/answer/{cardId}
```

To delete a card
```
DELETE http://localhost:8080/cards/delete/{cardId}
```

To get all cards for review today
```
GET http://localhost:8080/cards/quizz
```

To force the validation of a card
```
PATCH http://localhost:8080/cards/{cardId}/answer
```
