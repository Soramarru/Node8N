# Tasker

## how the tasker work

We will take a simple json for example

``` json
{
    "taskNodeName" : "UriAvailabilityChecker",
    "data" : {
        "uri" : "https://soramaru.fr"
    }
}
```
the "taskNodeName" must match exactly the name of the file in the nodes folder if its not found a error is thrown

in this example its only what is needed to run the test, in the server much more complex thing will occur ( eg a list of task with depending check for which one to do on which case )

the tasker will read the taskNodeName import it , send the data to check ,  throw a error if the data wasnt expected and if the data match will run the test and return what the test want to return (any)

## how to add your task

here is a js example of a task skeleton

```js
const UriChecker = 
{
    // this function receive task.parameter and must ensure that the received data is enough to run the check by returning true or false in the final obj
    async assertData(rawData)
    {
        // case fail 
        return { canRun : false }
        // case okay 
        return { canRun : true , formattedData : "data"}
    },

    // this will not be called at all if assertData does not return canRun : true
    async run(checkedData)
    {
        // if the data assertion worked the test is runned

        // if successfull
        return true;
        // if failed
        return false;

        // Note that if the "module" throw a crash the pipeline will completely stop
    },
}
export default UriChecker;
```

the assertData and the Run is two **needed** function the task will throw and stop the pipeline if one of these two dont exist
( you can add as many other function in it and import what is needed )

