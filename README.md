# Box GraphQL Wrapper
Wraps the Box API in a [GraphQL](http://graphql.org/) endpoint - `/api/graphql`

## Building the Project
This project requires several keys to authenticate with the Box API. In order to add the keys
to the application, create `authKeys.json` in the Box-GraphQL-Wrapper folder. The file should
look like: 
``` json
{
  "auth": {
    "clientId": {YourClientId},
    "clientSecret": {YourClientSecret},
    "developerToken": {YourDeveloperToken}
  }
}
```

The use of the developer token in `authKeys` is temporary. It will be eventually replaced by real
authentication using the `Authorization` header.

Client IDs, secrets, and developer tokens can be found by registering an app at https://developer.box.com/.

## Usage

All requests to the API should be POSTed to the `api/graphql` endpoint. Requests use the 
`application/graphQL` media type, and responses are in JSON.

#### Folder
```
id: String!
name: String!
items:  [FolderItem]
```

#### Folder Item
```
id: String!
name: String!
size: Int
description: String
tags: [String]
createdAt: Date
modifiedAt: Date
parent: Folder
ownedBy: User
createdBy: User
modifiedBy: User
```

#### User
```
id: String!
name: String!
isExemptFromLoginVerification: Boolean
isExemptFromDeviceLimits: Boolean
avatarUrl: String
address: String
phone: String
jobTitle: String
status: String
isSyncEnabled: Boolean
canSeeManagedUsers: Boolean
maxUploadSize: Int
spaceUsed: Int
spaceAmount: Int
language: String
role: String
modifiedAt: Date
createdAt: Date
login: String
```

### Sample Requests
All requests should be sent to the `/api/graphql` endpoint using the `POST` verb. The
content-type of the request must be `application/graphQL`.

These requests are just samples - fields can be added or removed from them as needed.
See the Usage section above for all available fields on each type.

For more information on GraphQL, visit [here](http://graphql.org/learn/).

To learn more about the Box API, visit [here](https://developer.box.com/docs)

#### Get a folder and its conents
```
query {
    folder(id: "123") {
        id
        name
        items {
            id
            name
            description
            tags
            size
            createdAt
        }
    }
}
```

#### Get a folder's contents
```
query {
    folderItems(folderId: "123") {
        id
        name
        description
        tags
        size
        modifiedAt
        parent {
            name
        }
        createdBy {
            id
            name
            login
            createdAt
            language
            status
        }
    }
}
```

#### Get a user
```
query {
    user(id: "123") {
        id
        name
        login
        createdAt
        language
        status
    }
}
```

## Roadmap
- [x] Basic object model
- [ ] Finish object model
- [ ] [GraphiQL support](https://github.com/graphql/graphiql)
- [ ] Mutations
- [ ] Subscriptions