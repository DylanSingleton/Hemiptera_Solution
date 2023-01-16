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

### Status Enumerators
1. <span style="color:orange">New</span> - This enumerator indicates that a task or project is in its initial stages and has not yet begun.
2. <span style="color:orange">InProgress</span> - This enumerator indicates that a task or project is currently being worked on and is not yet completed.
3. <span style="color:orange">OnHold</span> - This enumerator indicates that a task or project has been temporarily halted and will not be worked on until further notice.
4. <span style="color:orange">Completed</span> - This enumerator indicates that a task or project has been finished and is ready for review or implementation.
5. <span style="color:orange">Cancelled</span> - This enumerator indicates that a task or project has been cancelled and will not be completed.
6. <span style="color:orange">Closed</span> - This enumerator indicates that a task or project has been completed and is no longer active.
7. <span style="color:orange">InReview</span> - This enumerator indicates that a task or project is being reviewed by relevant parties.
8. <span style="color:orange">Testing</span> - This enumerator indicates that a task or project is being tested to ensure it meets the necessary requirements.
9. <span style="color:orange">Deployment</span> - This enumerator indicates that a task or project is being deployed for use in a live environment.

### Type Enumerators
Copy code
1. <span style="color:orange">WebApplication</span> - This enumerator indicates that the project is a web-based application.
2. <span style="color:orange">MobileApplication</span> - This enumerator indicates that the project is a mobile application.
3. <span style="color:orange">DesktopApplication</span> - This enumerator indicates that the project is a desktop application.
4. <span style="color:orange">SystemIntegration</span> - This enumerator indicates that the project is related to the integration of different systems.
5. <span style="color:orange">APIDevelopment</span> - This enumerator indicates that the project is about developing Application Programming Interface (API).
6. <span style="color:orange">CloudBased</span> - This enumerator indicates that the project is based on cloud computing.
7. <span style="color:orange">DataScience</span> - This enumerator indicates that the project is about data science and analysis.
8. <span style="color:orange">VirtualReality</span> - This enumerator indicates that the project is related to virtual reality technology.
9. <span style="color:orange">IoT</span> - This enumerator indicates that the project is related to the Internet of Things (IoT) technology.
10. <span style="color:orange">EmbeddedSystem</span> - This enumerator indicates that the project is related to embedded systems technology.

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