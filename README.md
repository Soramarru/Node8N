# Node8N


## How to launch the project
Install Docker (any platform), go into this folder and run:
```shell
docker compose up
```

## Launch the C# client
It is located in the "n8nClient" folder. Open the .slnx file.

## How to run the tests
A Node.js version is required here (Node 22 recommended).
Run the command in the same directory as the README:
```shell
npm i
npm test -- server/test.js
```

## Going further
More "technical" information is in the tasker folder, to understand how to maintain and add tasks to the API and the tasker.
