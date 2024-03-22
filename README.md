# Leitner System

Studies project to implement the leitner system for learning, following Clean Code & Hexagonal Architecture principles.

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
http://localhost:4321/cards
```

## API

### Availble endpoints:

To get all cards
```
GET http://localhost:4321/cards
```

To get all cards with a specific tag
```
POST http://localhost:4321/cards?tags=Architecture
```

To create a card
```
POST http://localhost:4321/cards
```

To update a question/answer/tag of a card
```
PATCH http://localhost:4321/cards
```

To answer a card
```
POST http://localhost:4321/cards/answer/{cardId}
```

To delete a card
```
DELETE http://localhost:4321/cards/delete/{cardId}
```

To get all cards for review today
```
GET http://localhost:4321/cards/quizz
```

To force the validation of a card
```
PATCH http://localhost:4321/cards/{cardId}/answer
```
