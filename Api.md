## Create Project

### Create Project Request

```js
POST /projects
```

```json
{
	"name": "Project Name",
	"description": "My Awesome Description",
	"repositoryLink": "https://github.com/dotnet/aspnetcore",
	"startDateTime": "2023-01-16T08:00:00",
	"endDateTime": "2023-01-16T08:00:00".
	"status": "1",
	"type": "3"
	}
```

### Status Enum
```js
1. New
2. InProgress
3. OnHold
4. Completed
5. Cancelled
6. Closed
7. InReview
8. Testing
9. Deployment
```

### Create Project Response

```js
201 Created
```

```yml
Location: {{host}}/Projects/{{id}}
```

```json
{
	"id": "00000000-0000-0000-0000-000000000000"
	"name": "Project Name",
	"description": "My Awesome Description",
	"repositoryLink": "https://github.com/dotnet/aspnetcore",
	"startDateTime": "2023-01-16T08:00:00",
	"endDateTime": "2023-01-16T08:00:00".
	"status": "In Progress",
	"type": "Web Application"
	}
```
## Get Project

### Get Project Request
```js
GET /projects/{{id}}
```
### Get Project Response
```js
200 Ok
```
```json
{
	"id": "00000000-0000-0000-0000-000000000000"
	"name": "Project Name",
	"description": "My Awesome Description",
	"repositoryLink": "https://github.com/dotnet/aspnetcore",
	"startDateTime": "2023-01-16T08:00:00",
	"endDateTime": "2023-01-16T08:00:00".
	"status": "In Progress",
	"type": "Web Application"
}
```
## Update Project
### Update Project Request
```js
PUT /projects/{{id}}
```
```json
{
	"name": "Project Name",
	"description": "My Awesome Description",
	"repositoryLink": "https://github.com/dotnet/aspnetcore",
	"startDateTime": "2023-01-16T08:00:00",
	"endDateTime": "2023-01-16T08:00:00".
	"status": "In Progress",
	"type": "Web Application"
}
```
### Update Project Response
```js
204 No Content
```
or
```js
201 Created
```
```yml
Location: {{host}}/Projects/{{id}}
```

## Delete Project
### Delete Project Request
```js
DELETE /projects/{{id}}
```
### Delete Project Response
```js
204 No Content
```